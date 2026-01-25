using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIMainComponent
  {
    public class GlobalEventsWrapper : IMouseEventConsumer
    {
      public CUIEvent<CUIMouseDownEvent> MouseDown { get; set; } = new();
      public CUIEvent<CUIMouseUpEvent> MouseUp { get; set; } = new();
      public CUIEvent<CUIMouseClickEvent> MouseClick { get; set; } = new();
      public CUIEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; set; } = new();
      public CUIEvent<CUIMouseMovedEvent> MouseMoved { get; set; } = new();
    }
  }
}