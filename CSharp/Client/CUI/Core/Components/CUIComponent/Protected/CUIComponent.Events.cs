using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    protected Events_Part Events { get; } = new();
    public class Events_Part : Part, IModule, IMouseEventConsumer
    {
      public void Init()
      {
        this.Route(Self.Visual.Background);
      }

      public ClearableEvent<CUIMouseDownEvent> MouseDown { get; } = new();
      public ClearableEvent<CUIMouseUpEvent> MouseUp { get; } = new();
      public ClearableEvent<CUIMouseClickEvent> MouseClick { get; } = new();
      public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
      public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; } = new();
    }
  }
}