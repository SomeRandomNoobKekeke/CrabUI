using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIMouseEnterEvent : CUIMouseEvent
  {
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseEnter.Raise(this);
      }
    }

    public CUIMouseEnterEvent(CUIInput.MouseInput mouse) : base(mouse) { }
    public override string ToString() => $"Mouse Enter";
  }
}