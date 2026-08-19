using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
using Microsoft.Xna.Framework.Input;


namespace CrabUI
{
  public partial class CUIVisualComponent
  {
    public void Click()
    {
      Events.MouseDown.Raise(
        this,
        new CUIMouseDownEvent(CUIMouseButton.LeftButton, CUICore.Input.Mouse)
      );
    }

    public void PressKey(Keys key)
    {
      FocusHandle.KeyPressed.Raise(
        new CUIKeyPressedEvent(key, CUICore.Input.Keyboard)
      );
    }

    public Action<CUIMouseDownEvent> OnMouseDown { set { MouseDown += value; } }
    public event Action<CUIMouseDownEvent> MouseDown
    {
      add => this.Events.MouseDown.Add(value);
      remove => this.Events.MouseDown.Remove(value);
    }

    public Action<CUIMouseUpEvent> OnMouseUp { set { MouseUp += value; } }
    public event Action<CUIMouseUpEvent> MouseUp
    {
      add => this.Events.MouseUp.Add(value);
      remove => this.Events.MouseUp.Remove(value);
    }

    public event Action<CUIMouseClickEvent> MouseClick
    {
      add => this.Events.MouseClick.Add(value);
      remove => this.Events.MouseClick.Remove(value);
    }

    public event Action<CUIMouseDoubleClickEvent> MouseDoubleClick
    {
      add => this.Events.MouseDoubleClick.Add(value);
      remove => this.Events.MouseDoubleClick.Remove(value);
    }

    public event Action<CUIMouseMovedEvent> MouseMoved
    {
      add => this.Events.MouseMoved.Add(value);
      remove => this.Events.MouseMoved.Remove(value);
    }

    public event Action<CUIMouseEnterEvent> MouseEnter
    {
      add => this.Events.MouseEnter.Add(value);
      remove => this.Events.MouseEnter.Remove(value);
    }

    public event Action<CUIMouseLeaveEvent> MouseLeave
    {
      add => this.Events.MouseLeave.Add(value);
      remove => this.Events.MouseLeave.Remove(value);
    }

    public event Action<CUIMouseOnEvent> MouseOn
    {
      add => this.Events.MouseOn.Add(value);
      remove => this.Events.MouseOn.Remove(value);
    }

    public event Action<CUIMouseOffEvent> MouseOff
    {
      add => this.Events.MouseOff.Add(value);
      remove => this.Events.MouseOff.Remove(value);
    }

    public Action<CUIMouseScrollEvent> OnMouseScroll { set { MouseScroll += value; } }
    public event Action<CUIMouseScrollEvent> MouseScroll
    {
      add => this.Events.MouseScroll.Add(value);
      remove => this.Events.MouseScroll.Remove(value);
    }

    public Action<Vector2> OnDragStarted { set { DragStarted += value; } }
    public event Action<Vector2> DragStarted
    {
      add => this.Events.DragStarted.Add(value);
      remove => this.Events.DragStarted.Remove(value);
    }

    public Action<Vector2> OnDragged { set { Dragged += value; } }
    public event Action<Vector2> Dragged
    {
      add => this.Events.Dragged.Add(value);
      remove => this.Events.Dragged.Remove(value);
    }

    public Action<Vector2> OnDragEnded { set { DragEnded += value; } }
    public event Action<Vector2> DragEnded
    {
      add => this.Events.DragEnded.Add(value);
      remove => this.Events.DragEnded.Remove(value);
    }

    public Action<CUIRect> OnResized { set { Resized += value; } }
    public event Action<CUIRect> Resized
    {
      add => this.Events.Resized.Add(value);
      remove => this.Events.Resized.Remove(value);
    }

    public Action<CUIRect> OnRectSet { set { RectSet += value; } }
    public event Action<CUIRect> RectSet
    {
      add => this.Events.RectSet.Add(value);
      remove => this.Events.RectSet.Remove(value);
    }
  }
}