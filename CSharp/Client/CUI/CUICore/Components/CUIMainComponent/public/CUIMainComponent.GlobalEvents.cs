using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUIMainComponent
  {
    public GlobalEvents_Part GlobalEvents { get; } = new();
    public class GlobalEvents_Part : Part, IMouseEventConsumer
    {
      public ClearableEvent<CUIMouseDownEvent> MouseDown { get; } = new();
      public ClearableEvent<CUIMouseUpEvent> MouseUp { get; } = new();
      public ClearableEvent<CUIMouseClickEvent> MouseClick { get; } = new();
      public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
      public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; } = new();
    }
  }
}