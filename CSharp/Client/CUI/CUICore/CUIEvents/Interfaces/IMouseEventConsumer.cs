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
  }
}