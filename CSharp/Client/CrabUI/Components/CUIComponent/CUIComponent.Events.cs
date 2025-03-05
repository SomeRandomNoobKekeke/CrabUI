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
    public event Action<double> OnUpdate; internal void InvokeOnUpdate(double totalTime) => OnUpdate?.Invoke(totalTime);
    public Action<double> AddOnUpdate { set { OnUpdate += value; } }
    public event Action<CUIInput> OnMouseLeave; internal void InvokeOnMouseLeave(CUIInput e) => OnMouseLeave?.Invoke(e);
    public Action<CUIInput> AddOnMouseLeave { set { OnMouseLeave += value; } }
    public event Action<CUIInput> OnMouseEnter; internal void InvokeOnMouseEnter(CUIInput e) => OnMouseEnter?.Invoke(e);
    public Action<CUIInput> AddOnMouseEnter { set { OnMouseEnter += value; } }
    public event Action<CUIInput> OnMouseDown; internal void InvokeOnMouseDown(CUIInput e) => OnMouseDown?.Invoke(e);
    public Action<CUIInput> AddOnMouseDown { set { OnMouseDown += value; } }
    public event Action<CUIInput> OnMouseUp; internal void InvokeOnMouseUp(CUIInput e) => OnMouseUp?.Invoke(e);
    public Action<CUIInput> AddOnMouseUp { set { OnMouseUp += value; } }
    public event Action<CUIInput> OnMouseMove; internal void InvokeOnMouseMove(CUIInput e) => OnMouseMove?.Invoke(e);
    public Action<CUIInput> AddOnMouseMove { set { OnMouseMove += value; } }
    public event Action<CUIInput> OnMouseOn; internal void InvokeOnMouseOn(CUIInput e) => OnMouseOn?.Invoke(e);
    public Action<CUIInput> AddOnMouseOn { set { OnMouseOn += value; } }
    public event Action<CUIInput> OnMouseOff; internal void InvokeOnMouseOff(CUIInput e) => OnMouseOff?.Invoke(e);
    public Action<CUIInput> AddOnMouseOff { set { OnMouseOff += value; } }
    public event Action<CUIInput> OnClick; internal void InvokeOnClick(CUIInput e) => OnClick?.Invoke(e);
    public Action<CUIInput> AddOnClick { set { OnClick += value; } }
    public event Action<CUIInput> OnDClick; internal void InvokeOnDClick(CUIInput e) => OnDClick?.Invoke(e);
    public Action<CUIInput> AddOnDClick { set { OnDClick += value; } }
    public event Action<CUIInput> OnScroll; internal void InvokeOnScroll(CUIInput e) => OnScroll?.Invoke(e);
    public Action<CUIInput> AddOnScroll { set { OnScroll += value; } }
    public event Action<float, float> OnDrag; internal void InvokeOnDrag(float x, float y) => OnDrag?.Invoke(x, y);
    public Action<float, float> AddOnDrag { set { OnDrag += value; } }
    public event Action<float, float> OnSwipe; internal void InvokeOnSwipe(float x, float y) => OnSwipe?.Invoke(x, y);
    public Action<float, float> AddOnSwipe { set { OnSwipe += value; } }
    public event Action<CUIInput> OnKeyDown; internal void InvokeOnKeyDown(CUIInput e) => OnKeyDown?.Invoke(e);
    public Action<CUIInput> AddOnKeyDown { set { OnKeyDown += value; } }
    public event Action<CUIInput> OnKeyUp; internal void InvokeOnKeyUp(CUIInput e) => OnKeyUp?.Invoke(e);
    public Action<CUIInput> AddOnKeyUp { set { OnKeyUp += value; } }
    public event Action<CUIInput> OnTextInput; internal void InvokeOnTextInput(CUIInput e) => OnTextInput?.Invoke(e);
    public Action<CUIInput> AddOnTextInput { set { OnTextInput += value; } }
    public event Action OnFocus; internal void InvokeOnFocus() => OnFocus?.Invoke();
    public Action AddOnFocus { set { OnFocus += value; } }
    public event Action OnFocusLost; internal void InvokeOnFocusLost() => OnFocusLost?.Invoke();
    public Action AddOnFocusLost { set { OnFocusLost += value; } }

    /// <summary>
    /// Simulates Click
    /// </summary>
    public void Click()
    { OnMouseDown?.Invoke(CUI.Input); }

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