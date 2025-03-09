using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  public partial class CUIComponent
  {
    #region Events --------------------------------------------------------

    [CUISerializable] public bool ConsumeMouseClicks { get; set; }
    [CUISerializable] public bool ConsumeDragAndDrop { get; set; }
    [CUISerializable] public bool ConsumeSwipe { get; set; }
    [CUISerializable] public bool ConsumeMouseScroll { get; set; }

    //HACK no one will ever find it, hehehe
    public void CascadeRefresh()
    {
      if (this is IRefreshable refreshable) refreshable.Refresh();
      Children.ForEach(c => c.CascadeRefresh());
    }

    public event Action OnTreeChanged;
    public event Action<double> OnUpdate;
    public event Action<CUIInput> OnMouseLeave;
    public event Action<CUIInput> OnMouseEnter;
    public event Action<CUIInput> OnMouseDown;
    public event Action<CUIInput> OnMouseUp;
    public event Action<CUIInput> OnMouseMove;
    public event Action<CUIInput> OnMouseOn;
    public event Action<CUIInput> OnMouseOff;
    public event Action<CUIInput> OnClick;
    public event Action<CUIInput> OnDClick;
    public event Action<CUIInput> OnScroll;
    public event Action<float, float> OnDrag;
    public event Action<float, float> OnSwipe;
    public event Action<CUIInput> OnKeyDown;
    public event Action<CUIInput> OnKeyUp;
    public event Action<CUIInput> OnTextInput;
    public event Action OnFocus;
    public event Action OnFocusLost;


    public Action<double> AddOnUpdate { set { OnUpdate += value; } }
    public Action<CUIInput> AddOnMouseLeave { set { OnMouseLeave += value; } }
    public Action<CUIInput> AddOnMouseEnter { set { OnMouseEnter += value; } }
    public Action<CUIInput> AddOnMouseDown { set { OnMouseDown += value; } }
    public Action<CUIInput> AddOnMouseUp { set { OnMouseUp += value; } }
    public Action<CUIInput> AddOnMouseMove { set { OnMouseMove += value; } }
    public Action<CUIInput> AddOnMouseOn { set { OnMouseOn += value; } }
    public Action<CUIInput> AddOnMouseOff { set { OnMouseOff += value; } }
    public Action<CUIInput> AddOnClick { set { OnClick += value; } }
    public Action<CUIInput> AddOnDClick { set { OnDClick += value; } }
    public Action<CUIInput> AddOnScroll { set { OnScroll += value; } }
    public Action<float, float> AddOnDrag { set { OnDrag += value; } }
    public Action<float, float> AddOnSwipe { set { OnSwipe += value; } }
    public Action<CUIInput> AddOnKeyDown { set { OnKeyDown += value; } }
    public Action<CUIInput> AddOnKeyUp { set { OnKeyUp += value; } }
    public Action<CUIInput> AddOnTextInput { set { OnTextInput += value; } }
    public Action AddOnFocus { set { OnFocus += value; } }
    public Action AddOnFocusLost { set { OnFocusLost += value; } }

    //TODO add more CUISpriteDrawModes
    public virtual bool IsPointOnTransparentPixel(Vector2 point)
    {
      if (BackgroundSprite.DrawMode != CUISpriteDrawMode.Resize) return true;

      //TODO hangle case where offset != sprite.origin
      Vector2 RotationCenter = new Vector2(
        BackgroundSprite.Offset.X * Real.Width,
        BackgroundSprite.Offset.Y * Real.Height
      );

      Vector2 v = (point - Real.Position - RotationCenter).Rotate(-BackgroundSprite.Rotation) + RotationCenter;

      float x = v.X / Real.Width;
      float y = v.Y / Real.Height;

      Rectangle bounds = BackgroundSprite.Texture.Bounds;
      Rectangle SourceRect = BackgroundSprite.SourceRect;

      int textureX = (int)Math.Round(SourceRect.X + x * SourceRect.Width);
      int textureY = (int)Math.Round(SourceRect.Y + y * SourceRect.Height);

      if (textureX < SourceRect.X || (SourceRect.X + SourceRect.Width - 1) < textureX) return true;
      if (textureY < SourceRect.Y || (SourceRect.Y + SourceRect.Height - 1) < textureY) return true;

      Color cl = TextureData[textureY * bounds.Width + textureX];

      return cl.A == 0;
    }


    public virtual bool ShouldInvoke(CUIInput e)
    {
      if (IgnoreTransparent)
      {
        return !IsPointOnTransparentPixel(e.MousePosition);
      }

      return true;
    }

    internal void InvokeOnUpdate(double totalTime) => OnUpdate?.Invoke(totalTime);
    internal void InvokeOnMouseLeave(CUIInput e) { OnMouseLeave?.Invoke(e); }
    internal void InvokeOnMouseEnter(CUIInput e) { if (ShouldInvoke(e)) OnMouseEnter?.Invoke(e); }
    internal void InvokeOnMouseDown(CUIInput e) { if (ShouldInvoke(e)) OnMouseDown?.Invoke(e); }
    internal void InvokeOnMouseUp(CUIInput e) { if (ShouldInvoke(e)) OnMouseUp?.Invoke(e); }
    internal void InvokeOnMouseMove(CUIInput e) { if (ShouldInvoke(e)) OnMouseMove?.Invoke(e); }
    internal void InvokeOnMouseOn(CUIInput e) { if (ShouldInvoke(e)) OnMouseOn?.Invoke(e); }
    internal void InvokeOnMouseOff(CUIInput e) { if (ShouldInvoke(e)) OnMouseOff?.Invoke(e); }
    internal void InvokeOnClick(CUIInput e) { if (ShouldInvoke(e)) OnClick?.Invoke(e); }
    internal void InvokeOnDClick(CUIInput e) { if (ShouldInvoke(e)) OnDClick?.Invoke(e); }
    internal void InvokeOnScroll(CUIInput e) { if (ShouldInvoke(e)) OnScroll?.Invoke(e); }
    internal void InvokeOnDrag(float x, float y) => OnDrag?.Invoke(x, y);
    internal void InvokeOnSwipe(float x, float y) => OnSwipe?.Invoke(x, y);
    internal void InvokeOnKeyDown(CUIInput e) { if (ShouldInvoke(e)) OnKeyDown?.Invoke(e); }
    internal void InvokeOnKeyUp(CUIInput e) { if (ShouldInvoke(e)) OnKeyUp?.Invoke(e); }
    internal void InvokeOnTextInput(CUIInput e) { if (ShouldInvoke(e)) OnTextInput?.Invoke(e); }
    internal void InvokeOnFocus() => OnFocus?.Invoke();
    internal void InvokeOnFocusLost() => OnFocusLost?.Invoke();

    #endregion
    #region Handles --------------------------------------------------------

    internal CUIDragHandle DragHandle = new CUIDragHandle();
    [CUISerializable]
    public bool Draggable
    {
      get => DragHandle.Draggable;
      set => DragHandle.Draggable = value;
    }
    //HACK Do i really need this?
    internal CUIFocusHandle FocusHandle = new CUIFocusHandle();
    [CUISerializable]
    public bool Focusable
    {
      get => FocusHandle.Focusable;
      set => FocusHandle.Focusable = value;
    }
    public CUIResizeHandle LeftResizeHandle = new CUIResizeHandle(new Vector2(0, 1), new CUIBool2(false, false));
    public CUIResizeHandle RightResizeHandle = new CUIResizeHandle(new Vector2(1, 1), new CUIBool2(true, false));
    public bool Resizible
    {
      get => ResizibleLeft || ResizibleRight;
      set { ResizibleLeft = value; ResizibleRight = value; }
    }

    [CUISerializable]
    public bool ResizibleLeft
    {
      get => LeftResizeHandle.Visible;
      set => LeftResizeHandle.Visible = value;
    }

    [CUISerializable]
    public bool ResizibleRight
    {
      get => RightResizeHandle.Visible;
      set => RightResizeHandle.Visible = value;
    }

    [CUISerializable]
    public CUIBool2 ResizeDirection
    {
      get => RightResizeHandle.Direction;
      set
      {
        LeftResizeHandle.Direction = value;
        RightResizeHandle.Direction = value;
      }
    }

    internal CUISwipeHandle SwipeHandle = new CUISwipeHandle();
    [CUISerializable]
    public bool Swipeable
    {
      get => SwipeHandle.Swipeable;
      set => SwipeHandle.Swipeable = value;
    }

    #endregion
  }
}