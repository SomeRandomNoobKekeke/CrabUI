using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
namespace CrabUI
{
  //WTF is this? why are there 2 EventConsumers, do i need to maintain them both? Are they synched?
  //So, i added it when i made all events accept component as firs arg, i guess not every IMouseEventConsumer is actually attached to component
  //Still need somehow make sure that they are synched
  public interface IMouseEventConsumingComponent
  {
    public CUIComponent Component { get; } // CRINGE

    public bool MouseOver { get; set; }
    public bool MousePressed { get; set; }

    public ClearableEvent<CUIComponent, CUIMouseDownEvent> MouseDown { get; }
    public ClearableEvent<CUIComponent, CUIMouseUpEvent> MouseUp { get; }
    public ClearableEvent<CUIComponent, CUIMouseClickEvent> MouseClick { get; }
    public ClearableEvent<CUIComponent, CUIMouseDoubleClickEvent> MouseDoubleClick { get; }
    public ClearableEvent<CUIComponent, CUIMouseMovedEvent> MouseMoved { get; }
    public ClearableEvent<CUIComponent, CUIMouseEnterEvent> MouseEnter { get; }
    public ClearableEvent<CUIComponent, CUIMouseLeaveEvent> MouseLeave { get; }
    public ClearableEvent<CUIComponent, CUIMouseOnEvent> MouseOn { get; }
    public ClearableEvent<CUIComponent, CUIMouseOffEvent> MouseOff { get; }
    public ClearableEvent<CUIComponent, CUIMouseScrollEvent> MouseScroll { get; }
  }

  public static class IMouseEventConsumingComponent_Extensions
  {
    public static void Map(this IMouseEventConsumer self, IMouseEventConsumingComponent target)
    {
      self.MouseDown.Map(target.MouseDown, (CUIMouseDownEvent e) => target.MouseDown.Raise(target.Component, e));
      self.MouseUp.Map(target.MouseUp, (CUIMouseUpEvent e) => target.MouseUp.Raise(target.Component, e));
      self.MouseClick.Map(target.MouseClick, (CUIMouseClickEvent e) => target.MouseClick.Raise(target.Component, e));
      self.MouseDoubleClick.Map(target.MouseDoubleClick, (CUIMouseDoubleClickEvent e) => target.MouseDoubleClick.Raise(target.Component, e));
      self.MouseMoved.Map(target.MouseMoved, (CUIMouseMovedEvent e) => target.MouseMoved.Raise(target.Component, e));
      self.MouseEnter.Map(target.MouseEnter, (CUIMouseEnterEvent e) => target.MouseEnter.Raise(target.Component, e));
      self.MouseLeave.Map(target.MouseLeave, (CUIMouseLeaveEvent e) => target.MouseLeave.Raise(target.Component, e));
      self.MouseOn.Map(target.MouseOn, (CUIMouseOnEvent e) => target.MouseOn.Raise(target.Component, e));
      self.MouseOff.Map(target.MouseOff, (CUIMouseOffEvent e) => target.MouseOff.Raise(target.Component, e));
      self.MouseScroll.Map(target.MouseScroll, (CUIMouseScrollEvent e) => target.MouseScroll.Raise(target.Component, e));
    }

    public static void Unmap(this IMouseEventConsumer self, IMouseEventConsumingComponent target)
    {
      self.MouseDown.Unmap(target.MouseDown);
      self.MouseUp.Unmap(target.MouseUp);
      self.MouseClick.Unmap(target.MouseClick);
      self.MouseDoubleClick.Unmap(target.MouseDoubleClick);
      self.MouseMoved.Unmap(target.MouseMoved);
      self.MouseEnter.Unmap(target.MouseEnter);
      self.MouseLeave.Unmap(target.MouseLeave);
      self.MouseOn.Unmap(target.MouseOn);
      self.MouseOff.Unmap(target.MouseOff);
      self.MouseScroll.Unmap(target.MouseScroll);
    }

    public static void Route(this IMouseEventConsumingComponent self, IMouseEventConsumer source) => source.Map(self);
    public static void Unroute(this IMouseEventConsumingComponent self, IMouseEventConsumer source) => source.Unmap(self);
  }
}