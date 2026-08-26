using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public class CUIMouseScrollEvent : CUIMouseEvent
  {
    public float Scroll => Mouse.Scroll;

    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseScroll.Raise(this);
      }
    }

    public CUIMouseScrollEvent(CUIInput.MouseInput mouse) : base(mouse) { }
    public override string ToString() => $"Scrolled {Scroll}";
  }
}