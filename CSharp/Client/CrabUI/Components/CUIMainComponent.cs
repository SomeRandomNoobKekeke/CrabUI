#define SHOWPERF

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace CrabUI
{
  /// <summary>
  /// Orchestrating drawing and updating of it's children  
  /// Also a CUIComponent, but it's draw and update methods
  /// Attached directly to games life cycle
  /// </summary>
  public class CUIMainComponent : CUIComponent
  {
    /// <summary>
    /// Wrapper for global events
    /// </summary>
    public class CUIGlobalEvents
    {
      public Action<CUIInput> OnMouseDown; public void InvokeOnMouseDown(CUIInput e) => OnMouseDown?.Invoke(e);
      public Action<CUIInput> OnMouseUp; public void InvokeOnMouseUp(CUIInput e) => OnMouseUp?.Invoke(e);
      public Action<CUIInput> OnMouseMoved; public void InvokeOnMouseMoved(CUIInput e) => OnMouseMoved?.Invoke(e);
      public Action<CUIInput> OnClick; public void InvokeOnClick(CUIInput e) => OnClick?.Invoke(e);
      public Action<CUIInput> OnKeyDown; public void InvokeOnKeyDown(CUIInput e) => OnKeyDown?.Invoke(e);
      public Action<CUIInput> OnKeyUp; public void InvokeOnKeyUp(CUIInput e) => OnKeyUp?.Invoke(e);
    }

    /// <summary>
    /// Frozen window doesn't update
    /// </summary>
    public bool Frozen { get; set; }
    public double UpdateInterval = 1.0 / 300.0;
    /// <summary>
    /// If true will update layout until it settles to prevent blinking
    /// </summary>
    public bool CalculateUntilResolved = true;
    /// <summary>
    /// If your GUI needs more than this steps of layout update
    /// you will get a warning
    /// </summary>
    public int MaxLayoutRecalcLoopsPerUpdate = 10;
    public event Action OnTreeChanged;
    public Action AddOnTreeChanged { set { OnTreeChanged += value; } }

    public CUIDragHandle GrabbedDragHandle;
    public CUIResizeHandle GrabbedResizeHandle;
    public CUISwipeHandle GrabbedSwipeHandle;
    public CUIComponent MouseOn;
    public CUIComponent FocusedComponent
    {
      get => CUI.FocusedComponent;
      set => CUI.FocusedComponent = value;
    }
    /// <summary>
    /// Container for true global events 
    /// CUIMainComponent itself can react to events and you can listen for those,
    /// but e.g. mouse events may be consumed before they reach Main
    /// </summary>
    public CUIGlobalEvents Global = new CUIGlobalEvents();

    private Stopwatch sw = new Stopwatch();

    internal List<CUIComponent> Flat = new List<CUIComponent>();
    internal List<CUIComponent> Leaves = new List<CUIComponent>();
    internal SortedList<int, List<CUIComponent>> Layers = new SortedList<int, List<CUIComponent>>();
    private List<CUIComponent> MouseOnList = new List<CUIComponent>();
    private Vector2 GrabbedOffset;

    private void RunStraigth(Action<CUIComponent> a) { for (int i = 0; i < Flat.Count; i++) a(Flat[i]); }
    private void RunReverse(Action<CUIComponent> a) { for (int i = Flat.Count - 1; i >= 0; i--) a(Flat[i]); }




    private void FlattenTree()
    {
      int retries = 0;
      bool done = false;
      do
      {
        retries++;
        if (retries > 10) break;
        try
        {
          Flat.Clear();
          Layers.Clear();

          int globalIndex = 0;
          void CalcZIndexRec(CUIComponent component, int added = 0)
          {
            component.positionalZIndex = globalIndex;
            globalIndex += 1;
            component.addedZIndex = added;
            if (component.ZIndex.HasValue) component.addedZIndex += component.ZIndex.Value;

            foreach (CUIComponent child in component.Children)
            {
              CalcZIndexRec(child, component.addedZIndex);
            }
          }

          CalcZIndexRec(this, 0);
          RunRecursiveOn(this, (c) =>
          {
            int i = c.positionalZIndex + c.addedZIndex;
            if (!Layers.ContainsKey(i)) Layers[i] = new List<CUIComponent>();
            Layers[i].Add(c);
          });

          foreach (var layer in Layers)
          {
            Flat.AddRange(layer.Value);
          }

          done = true;
        }
        catch (Exception e)
        {
          CUI.Warning($"Couldn't Flatten component tree: {e.Message}");
        }
      } while (!done);
    }

    #region Update

    internal bool GlobalLayoutChanged;
    internal void LayoutChanged() => GlobalLayoutChanged = true;
    private double LastUpdateTime;
    private int UpdateLoopCount = 0;
    /// <summary>
    /// Forses 1 layout update step, even when Frozen
    /// </summary>
    public void Step()
    {
      Update(LastUpdateTime + UpdateInterval, true, true);
    }
    public void Update(double totalTime, bool force = false, bool noInput = false)
    {
      if (!force)
      {
        if (Frozen) return;
        if (totalTime - LastUpdateTime <= UpdateInterval) return;
      }

      CUIDebug.Flush();

      if (TreeChanged)
      {
        OnTreeChanged?.Invoke();

        FlattenTree();
        TreeChanged = false;
      }

      if (!noInput) HandleInput(totalTime);

      RunStraigth(c => c.InvokeOnUpdate(totalTime));


      if (CalculateUntilResolved)
      {
        UpdateLoopCount = 0;
        do
        {
          GlobalLayoutChanged = false;

          if (TreeChanged)
          {
            OnTreeChanged?.Invoke();

            FlattenTree();
            TreeChanged = false;
          }

          RunReverse(c =>
          {
            c.Layout.ResizeToContent();
          });

          RunStraigth(c =>
          {
            c.Layout.Update();
            c.Layout.UpdateDecor();
          });

          UpdateLoopCount++;
          if (UpdateLoopCount >= MaxLayoutRecalcLoopsPerUpdate)
          {
            PrintRecalLimitWarning();
            break;
          }
        }
        while (GlobalLayoutChanged);
        //CUI.Log($"UpdateLoopCount: {UpdateLoopCount}");
      }
      else
      {
        RunReverse(c =>
        {
          c.Layout.ResizeToContent();
        });

        RunStraigth(c =>
        {
          c.Layout.Update();
          c.Layout.UpdateDecor();
        });
      }

      //TODO do i need 2 updates?
      //RunStraigth(c => c.InvokeOnUpdate(totalTime));

      LastUpdateTime = totalTime;
    }

    #endregion
    #region Draw

    private void StopStart(SpriteBatch spriteBatch, Rectangle SRect, SamplerState? samplerState = null)
    {
      samplerState ??= GUI.SamplerState;
      spriteBatch.End();
      spriteBatch.GraphicsDevice.ScissorRectangle = SRect;
      spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: samplerState, rasterizerState: GameMain.ScissorTestEnable);
    }

    public new void Draw(SpriteBatch spriteBatch)
    {
      sw.Restart();

      Rectangle OriginalSRect = spriteBatch.GraphicsDevice.ScissorRectangle;
      Rectangle SRect = OriginalSRect;

      try
      {
        RunStraigth(c =>
        {
          if (!c.Visible || c.CulledOut) return;
          if (c.Parent != null && c.Parent.ScissorRect.HasValue && SRect != c.Parent.ScissorRect.Value)
          {
            SRect = c.Parent.ScissorRect.Value;
            StopStart(spriteBatch, SRect, c.SamplerState);
          }
          c.Draw(spriteBatch);
        });
      }
      finally
      {
        if (spriteBatch.GraphicsDevice.ScissorRectangle != OriginalSRect) StopStart(spriteBatch, OriginalSRect);
      }

      RunStraigth(c =>
      {
        if (!c.Visible || c.CulledOut) return;
        c.DrawFront(spriteBatch);
      });

      sw.Stop();
      // CUIDebug.EnsureCategory();
      // CUIDebug.CaptureTicks(sw.ElapsedTicks, "CUI.Draw");
    }
    #endregion
    // https://youtu.be/xuFgUmYCS8E?feature=shared&t=72
    #region HandleInput Start 

    public void OnDragEnd(CUIDragHandle h) { if (h == GrabbedDragHandle) GrabbedDragHandle = null; }
    public void OnResizeEnd(CUIResizeHandle h) { if (h == GrabbedResizeHandle) GrabbedResizeHandle = null; }
    public void OnSwipeEnd(CUISwipeHandle h) { if (h == GrabbedSwipeHandle) GrabbedSwipeHandle = null; }


    private void HandleInput(double totalTime)
    {
      HandleGlobal(totalTime);
      HandleMouse(totalTime);
      HandleKeyboard(totalTime);
    }

    private void HandleGlobal(double totalTime)
    {
      if (CUI.Input.MouseDown) Global.InvokeOnMouseDown(CUI.Input);
      if (CUI.Input.MouseUp)
      {
        Global.InvokeOnMouseUp(CUI.Input);
        Global.InvokeOnClick(CUI.Input);
      }
      if (CUI.Input.MouseMoved) Global.InvokeOnMouseMoved(CUI.Input);
      if (CUI.Input.SomeKeyPressed) Global.InvokeOnKeyDown(CUI.Input);
      if (CUI.Input.SomeKeyUnpressed) Global.InvokeOnKeyUp(CUI.Input);
    }

    private void HandleKeyboard(double totalTime)
    {
      if (FocusedComponent == null) FocusedComponent = this;
      if (CUI.Input.PressedKeys.Contains(Keys.Escape)) FocusedComponent = this;
      if (CUI.Input.SomeKeyPressed) FocusedComponent.InvokeOnKeyDown(CUI.Input);
      if (CUI.Input.SomeKeyUnpressed) FocusedComponent.InvokeOnKeyUp(CUI.Input);
      if (CUI.Input.SomeWindowEvents) FocusedComponent.InvokeOnTextInput(CUI.Input);
    }

    private void HandleMouse(double totalTime)
    {
      if (!CUI.Input.SomethingHappened) return;

      if (!CUI.Input.MouseHeld)
      {
        GrabbedDragHandle?.EndDrag();
        GrabbedResizeHandle?.EndResize();
        GrabbedSwipeHandle?.EndSwipe();
      }

      if (CUI.Input.MouseMoved)
      {
        GrabbedDragHandle?.DragTo(CUI.Input.MousePosition);
        GrabbedResizeHandle?.Resize(CUI.Input.MousePosition);
        GrabbedSwipeHandle?.Swipe(CUI.Input);
      }

      if (CUI.Input.MouseInputHandled) return;

      //HACK
      //if (CUI.Input.ClickConsumed) return;

      //TODO think where should i put it?
      if (GrabbedResizeHandle != null || GrabbedDragHandle != null || GrabbedSwipeHandle != null) return;

      List<CUIComponent> prevMouseOnList = new List<CUIComponent>(MouseOnList);

      CUIComponent CurrentMouseOn = null;
      MouseOnList.Clear();



      // form MouseOnList
      // Note: including main component
      if (
        GUI.MouseOn == null || (GUI.MouseOn is GUIButton btn && btn.Text == "DUMMY")
        || (this == CUI.TopMain) //TODO guh
      )
      {
        RunStraigth(c =>
        {
          bool ok = !c.IgnoreEvents && c.Real.Contains(CUI.Input.MousePosition) && c.ShouldInvoke(CUI.Input);

          if (c.Parent != null && c.Parent.ScissorRect.HasValue &&
              !c.Parent.ScissorRect.Value.Contains(CUI.Input.Mouse.Position))
          {
            ok = false;
          }

          if (ok) MouseOnList.Add(c);
        });
      }

      MouseOn = MouseOnList.LastOrDefault();

      //HACK
      if (MouseOn != this)
      {
        CUI.Input.MouseInputHandled = true;
        CUIMultiModResolver.MarkOtherInputsAsHandled();
      }

      //if (CurrentMouseOn != null) GUI.MouseOn = dummyComponent;


      foreach (CUIComponent c in prevMouseOnList)
      {
        c.MousePressed = false;
        c.MouseOver = false;
        c.InvokeOnMouseOff(CUI.Input);
      }

      foreach (CUIComponent c in MouseOnList)
      {
        c.MousePressed = CUI.Input.MouseHeld;
        c.MouseOver = true;
        c.InvokeOnMouseOn(CUI.Input);
      }

      // Mouse enter / leave
      foreach (CUIComponent c in prevMouseOnList.Except(MouseOnList)) c.InvokeOnMouseLeave(CUI.Input);
      foreach (CUIComponent c in MouseOnList.Except(prevMouseOnList)) c.InvokeOnMouseEnter(CUI.Input);


      // focus
      if (CUI.Input.MouseDown)
      {
        CUIComponent newFocused = this;
        for (int i = MouseOnList.Count - 1; i >= 0; i--)
        {
          if (MouseOnList[i].FocusHandle.ShouldStart(CUI.Input))
          {
            newFocused = MouseOnList[i];
            break;
          }
        }
        FocusedComponent = newFocused;
      }

      // Resize
      for (int i = MouseOnList.Count - 1; i >= 0; i--)
      {
        if (MouseOnList[i].RightResizeHandle.ShouldStart(CUI.Input))
        {
          GrabbedResizeHandle = MouseOnList[i].RightResizeHandle;
          GrabbedResizeHandle.BeginResize(CUI.Input.MousePosition);
          break;
        }

        if (MouseOnList[i].LeftResizeHandle.ShouldStart(CUI.Input))
        {
          GrabbedResizeHandle = MouseOnList[i].LeftResizeHandle;
          GrabbedResizeHandle.BeginResize(CUI.Input.MousePosition);
          break;
        }
      }
      if (GrabbedResizeHandle != null) return;

      //Scroll
      for (int i = MouseOnList.Count - 1; i >= 0; i--)
      {
        if (CUI.Input.Scrolled) MouseOnList[i].InvokeOnScroll(CUI.Input);

        if (MouseOnList[i].ConsumeMouseScroll) break;
      }

      //Move
      if (CUI.Input.MouseMoved)
      {
        for (int i = MouseOnList.Count - 1; i >= 0; i--)
        {
          MouseOnList[i].InvokeOnMouseMove(CUI.Input);
        }
      }



      //Clicks
      for (int i = MouseOnList.Count - 1; i >= 0; i--)
      {
        if (CUI.Input.MouseDown) MouseOnList[i].InvokeOnMouseDown(CUI.Input);
        if (CUI.Input.MouseUp)
        {
          MouseOnList[i].InvokeOnMouseUp(CUI.Input);
          MouseOnList[i].InvokeOnClick(CUI.Input);
        }
        if (CUI.Input.DoubleClick) MouseOnList[i].InvokeOnDClick(CUI.Input);

        if (MouseOnList[i].ConsumeMouseClicks || CUI.Input.ClickConsumed) break;
      }
      if (CUI.Input.ClickConsumed) return;

      // Swipe
      for (int i = MouseOnList.Count - 1; i >= 0; i--)
      {
        if (MouseOnList[i].SwipeHandle.ShouldStart(CUI.Input))
        {
          GrabbedSwipeHandle = MouseOnList[i].SwipeHandle;
          GrabbedSwipeHandle.BeginSwipe(CUI.Input.MousePosition);
          break;
        }

        if (MouseOnList[i].ConsumeSwipe) break;
      }
      if (GrabbedSwipeHandle != null) return;

      // Drag
      for (int i = MouseOnList.Count - 1; i >= 0; i--)
      {
        if (MouseOnList[i].DragHandle.ShouldStart(CUI.Input))
        {
          GrabbedDragHandle = MouseOnList[i].DragHandle;
          GrabbedDragHandle.BeginDrag(CUI.Input.MousePosition);
          break;
        }

        if (MouseOnList[i].ConsumeDragAndDrop) break;
      }
      if (GrabbedDragHandle != null) return;
    }
    #endregion
    #region HandleInput End
    #endregion

    /// <summary>
    /// Obsolete function  
    /// Will run generator func with this
    /// </summary>
    /// <param name="initFunc"> Generator function that adds components to passed Main </param>
    public void Load(Action<CUIMainComponent> initFunc)
    {
      RemoveAllChildren();
      initFunc(this);
    }

    public CUIMainComponent() : base()
    {
      CullChildren = true;
      Real = new CUIRect(0, 0, GameMain.GraphicsWidth, GameMain.GraphicsHeight);
      Visible = false;
      //IgnoreEvents = true;
      ShouldPassPropsToChildren = false;


      Debug = true;
      ChildrenBoundaries = CUIBoundaries.Box;
    }

    public void PrintRecalLimitWarning()
    {
      CUI.Log($"Warning: Your GUI code requires {MaxLayoutRecalcLoopsPerUpdate} layout update loops to fully resolve (which is cringe). Optimize it!", Color.Orange);
    }
  }
}