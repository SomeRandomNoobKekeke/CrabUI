using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public class CUIMouseOnEvent : CUIMouseEvent
  {
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseOver = true;
        MEConsumer.MousePressed = Mouse.Pressed;
        MEConsumer.MouseOn.Raise(this);

        Consumed = MEConsumer.ConsumeMouseEvents || Consumed;
      }
    }

    public CUIMouseOnEvent(CUIInput.MouseInput mouse) : base(mouse) { }
    public override string ToString() => $"Mouse Enter";
  }
}