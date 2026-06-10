using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIMainComponent
  {
    public GlobalEvents_Part GlobalEvents { get; } = new();
    public class GlobalEvents_Part : Part, IMouseEventConsumer
    {
      public bool MouseOver { get; set; } // BRUH
      public bool MousePressed { get; set; }

      public bool ConsumeMouseClicks { get; set; } // BRUH

      public ClearableEvent<CUIMouseDownEvent> MouseDown { get; } = new();
      public ClearableEvent<CUIMouseUpEvent> MouseUp { get; } = new();
      public ClearableEvent<CUIMouseClickEvent> MouseClick { get; } = new();
      public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
      public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; } = new();
      public ClearableEvent<CUIMouseEnterEvent> MouseEnter { get; } = new(); // BRUH
      public ClearableEvent<CUIMouseLeaveEvent> MouseLeave { get; } = new(); // BRUH
      public ClearableEvent<CUIMouseOnEvent> MouseOn { get; } = new(); // BRUH
      public ClearableEvent<CUIMouseOffEvent> MouseOff { get; } = new(); // BRUH
      public ClearableEvent<CUIMouseScrollEvent> MouseScroll { get; } = new();
    }
  }
}