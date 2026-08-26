using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public class CUIMouseMovedEvent : CUIMouseEvent
  {
    public Vector2 PosDiff => Mouse.PosDiff;
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseMoved.Raise(this);
      }
    }

    public CUIMouseMovedEvent(CUIInput.MouseInput mouse) : base(mouse) { }

    public override string ToString() => $"Mouse Moved {Pos}";
  }
}