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
    #region public
    #endregion
    public bool MouseOver => Events.MouseOver;
    public bool MousePressed => Events.MousePressed;

    #region protected
    #endregion
    protected Events_Part Events { get; } = new();
    public class Events_Part : Part, IModule, IMouseEventConsumingComponent
    {
      public void Init()
      {
        this.Route(Self.Background);
        //TODO route borders?
      }

      public CUIComponent Component => Self;

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


      public ClearableEvent<CUIComponent, CUIMouseDownEvent> MouseDown { get; } = new();
      public ClearableEvent<CUIComponent, CUIMouseUpEvent> MouseUp { get; } = new();
      public ClearableEvent<CUIComponent, CUIMouseClickEvent> MouseClick { get; } = new();
      public ClearableEvent<CUIComponent, CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
      public ClearableEvent<CUIComponent, CUIMouseMovedEvent> MouseMoved { get; } = new();
      public ClearableEvent<CUIComponent, CUIMouseEnterEvent> MouseEnter { get; } = new();
      public ClearableEvent<CUIComponent, CUIMouseLeaveEvent> MouseLeave { get; } = new();
      public ClearableEvent<CUIComponent, CUIMouseOnEvent> MouseOn { get; } = new();
      public ClearableEvent<CUIComponent, CUIMouseOffEvent> MouseOff { get; } = new();
      public ClearableEvent<CUIComponent, CUIMouseScrollEvent> MouseScroll { get; } = new();

      public ClearableEvent<CUIComponent, Vector2> Dragged { get; } = new();
      public ClearableEvent<CUIComponent, CUIRect> RectSet { get; } = new();


    }
  }
}