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
    public ClearableEvent<CUIMouseDownEvent> MouseDown { get; }
    public ClearableEvent<CUIMouseUpEvent> MouseUp { get; }
    public ClearableEvent<CUIMouseClickEvent> MouseClick { get; }
    public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; }
    public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; }
    public ClearableEvent<CUIMouseEnterEvent> MouseEnter { get; }
    public ClearableEvent<CUIMouseLeaveEvent> MouseLeave { get; }

    // public CUIEvent<CUIInput> OnMouseLeave = new();
    // public CUIEvent<CUIInput> OnMouseEnter = new() { ShouldRise = ShouldInvoke };
    // public CUIEvent<CUIInput> OnMouseOn = new() { ShouldRise = ShouldInvoke };
    // public CUIEvent<CUIInput> OnMouseOff = new() { ShouldRise = ShouldInvoke };
    // public CUIEvent<CUIInput> OnClick = new() { ShouldRise = ShouldInvoke };
    // public CUIEvent<CUIInput> OnDClick = new() { ShouldRise = ShouldInvoke };
    // public CUIEvent<CUIInput> OnScroll = new() { ShouldRise = ShouldInvoke };
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
    }

    public static void Unmap(this IMouseEventConsumer self, IMouseEventConsumer target)
    {
      self.MouseDown.Unmap(target.MouseDown);
      self.MouseUp.Unmap(target.MouseUp);
      self.MouseClick.Unmap(target.MouseClick);
      self.MouseDoubleClick.Unmap(target.MouseDoubleClick);
      self.MouseMoved.Unmap(target.MouseMoved);
    }

    public static void Route(this IMouseEventConsumer self, IMouseEventConsumer source) => source.Map(self);
    public static void Unroute(this IMouseEventConsumer self, IMouseEventConsumer source) => source.Unmap(self);
  }
}