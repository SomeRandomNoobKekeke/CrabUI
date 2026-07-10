using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    public Action<CUIComponent, CUIMouseDownEvent> OnMouseDown { set { MouseDown += value; } }
    public event Action<CUIComponent, CUIMouseDownEvent> MouseDown
    {
      add => this.Events.MouseDown.Add(value);
      remove => this.Events.MouseDown.Remove(value);
    }

    public Action<CUIComponent, CUIMouseUpEvent> OnMouseUp { set { MouseUp += value; } }
    public event Action<CUIComponent, CUIMouseUpEvent> MouseUp
    {
      add => this.Events.MouseUp.Add(value);
      remove => this.Events.MouseUp.Remove(value);
    }

    public event Action<CUIComponent, CUIMouseClickEvent> MouseClick
    {
      add => this.Events.MouseClick.Add(value);
      remove => this.Events.MouseClick.Remove(value);
    }

    public event Action<CUIComponent, CUIMouseDoubleClickEvent> MouseDoubleClick
    {
      add => this.Events.MouseDoubleClick.Add(value);
      remove => this.Events.MouseDoubleClick.Remove(value);
    }

    public event Action<CUIComponent, CUIMouseMovedEvent> MouseMoved
    {
      add => this.Events.MouseMoved.Add(value);
      remove => this.Events.MouseMoved.Remove(value);
    }

    public event Action<CUIComponent, CUIMouseEnterEvent> MouseEnter
    {
      add => this.Events.MouseEnter.Add(value);
      remove => this.Events.MouseEnter.Remove(value);
    }

    public event Action<CUIComponent, CUIMouseLeaveEvent> MouseLeave
    {
      add => this.Events.MouseLeave.Add(value);
      remove => this.Events.MouseLeave.Remove(value);
    }

    public event Action<CUIComponent, CUIMouseOnEvent> MouseOn
    {
      add => this.Events.MouseOn.Add(value);
      remove => this.Events.MouseOn.Remove(value);
    }

    public event Action<CUIComponent, CUIMouseOffEvent> MouseOff
    {
      add => this.Events.MouseOff.Add(value);
      remove => this.Events.MouseOff.Remove(value);
    }

    public Action<CUIComponent, CUIMouseScrollEvent> OnMouseScroll { set { MouseScroll += value; } }
    public event Action<CUIComponent, CUIMouseScrollEvent> MouseScroll
    {
      add => this.Events.MouseScroll.Add(value);
      remove => this.Events.MouseScroll.Remove(value);
    }




  }
}