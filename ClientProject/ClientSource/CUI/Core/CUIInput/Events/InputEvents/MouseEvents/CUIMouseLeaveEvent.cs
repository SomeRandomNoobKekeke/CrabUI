using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public class CUIMouseLeaveEvent : CUIMouseEvent
  {
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseLeave.Raise(this);
      }
    }

    public CUIMouseLeaveEvent(CUIInput.MouseInput mouse) : base(mouse) { }
    public override string ToString() => $"Mouse Enter";
  }
}