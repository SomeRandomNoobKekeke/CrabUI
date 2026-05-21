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
  public interface IMouseEventConsumer : IEventConsumer
  {
    public bool MouseOver { get; set; }
    public bool MousePressed { get; set; }

    public ClearableEvent<CUIMouseDownEvent> MouseDown { get; }
    public ClearableEvent<CUIMouseUpEvent> MouseUp { get; }
    public ClearableEvent<CUIMouseClickEvent> MouseClick { get; }
    public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; }
    public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; }
    public ClearableEvent<CUIMouseEnterEvent> MouseEnter { get; }
    public ClearableEvent<CUIMouseLeaveEvent> MouseLeave { get; }
    public ClearableEvent<CUIMouseOnEvent> MouseOn { get; }
    public ClearableEvent<CUIMouseOffEvent> MouseOff { get; }
    public ClearableEvent<CUIMouseScrollEvent> MouseScroll { get; }
  }

  public static class IMouseEventConsumer_Extensions
  {
    public static void Map(this IMouseEventConsumer self, IMouseEventConsumer target)
    {
      self.MouseDown.Map(target.MouseDown);
      self.MouseUp.Map(target.MouseUp);
      self.MouseClick.Map(target.MouseClick);
      self.MouseDoubleClick.Map(target.MouseDoubleClick);
      self.MouseMoved.Map(target.MouseMoved);
      self.MouseEnter.Map(target.MouseEnter);
      self.MouseLeave.Map(target.MouseLeave);
      self.MouseOn.Map(target.MouseOn);
      self.MouseOff.Map(target.MouseOff);
      self.MouseScroll.Map(target.MouseScroll);
    }

    public static void Unmap(this IMouseEventConsumer self, IMouseEventConsumer target)
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

    public static void Route(this IMouseEventConsumer self, IMouseEventConsumer source) => source.Map(self);
    public static void Unroute(this IMouseEventConsumer self, IMouseEventConsumer source) => source.Unmap(self);
  }
}