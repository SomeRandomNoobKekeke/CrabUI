using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public interface IMouseEventConsumer : IEventConsumer
  {
    public CUIEvent<CUIMouseDownEvent> MouseDown { get; }
    public CUIEvent<CUIMouseUpEvent> MouseUp { get; }
    public CUIEvent<CUIMouseClickEvent> MouseClick { get; }
    public CUIEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; }
    public CUIEvent<CUIMouseMovedEvent> MouseMoved { get; }
  }
}