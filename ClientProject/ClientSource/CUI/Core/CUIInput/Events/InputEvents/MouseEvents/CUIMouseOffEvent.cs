using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIMouseOffEvent : CUIMouseEvent
  {
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseOver = false;
        MEConsumer.MousePressed = false;
        MEConsumer.MouseOff.Raise(this);

        Consumed = MEConsumer.ConsumeMouseEvents || Consumed;
      }
    }

    public CUIMouseOffEvent(CUIInput.MouseInput mouse) : base(mouse) { }
    public override string ToString() => $"Mouse Enter";
  }
}