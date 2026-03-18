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
    protected GlobalEvents_Part GlobalEvents { get; } = new();
    public class GlobalEvents_Part : Part, IMouseEventConsumer
    {
      public CUIEvent<CUIMouseDownEvent> MouseDown { get; set; } = new();
      public CUIEvent<CUIMouseUpEvent> MouseUp { get; set; } = new();
      public CUIEvent<CUIMouseClickEvent> MouseClick { get; set; } = new();
      public CUIEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; set; } = new();
      public CUIEvent<CUIMouseMovedEvent> MouseMoved { get; set; } = new();
    }
  }
}