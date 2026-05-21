using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    protected Events_Part Events { get; } = new();
    public class Events_Part : Part, IModule, IMouseEventConsumer
    {
      public void Init()
      {
        this.Route(Self.Background);
      }

      public bool MouseOver
      {
        get => Self.Background.MouseOver;
        set => Self.Background.MouseOver = value;
      }
      public bool MousePressed
      {
        get => Self.Background.MousePressed;
        set => Self.Background.MousePressed = value;
      }


      public ClearableEvent<CUIMouseDownEvent> MouseDown { get; } = new();
      public ClearableEvent<CUIMouseUpEvent> MouseUp { get; } = new();
      public ClearableEvent<CUIMouseClickEvent> MouseClick { get; } = new();
      public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
      public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; } = new();
      public ClearableEvent<CUIMouseEnterEvent> MouseEnter { get; } = new();
      public ClearableEvent<CUIMouseLeaveEvent> MouseLeave { get; } = new();
      public ClearableEvent<CUIMouseOnEvent> MouseOn { get; } = new();
      public ClearableEvent<CUIMouseOffEvent> MouseOff { get; } = new();
      public ClearableEvent<CUIMouseScrollEvent> MouseScroll { get; } = new();
    }
  }
}