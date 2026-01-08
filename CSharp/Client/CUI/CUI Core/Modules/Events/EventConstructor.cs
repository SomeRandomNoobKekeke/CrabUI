using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class EventConstructor : IModule
  {
    public IEnumerable<CUIEvent> ConstructEvents(CUIInput input)
    {
      foreach (var button in input.Mouse.Buttons)
      {
        if (!button.Changed) continue;
        if (button.Down) yield return new CUIMouseDownEvent(button.Type, input.Mouse);
        if (button.Up) yield return new CUIMouseUpEvent(button.Type, input.Mouse);
        if (button.Click) yield return new CUIMouseClickEvent(button.Type, input.Mouse);
        if (button.DoubleClick) yield return new CUIMouseDoubleClickEvent(button.Type, input.Mouse);
      }

      if (input.Mouse.Moved)
      {
        yield return new CUIMouseMovedEvent(input.Mouse);
      }
    }
  }
}