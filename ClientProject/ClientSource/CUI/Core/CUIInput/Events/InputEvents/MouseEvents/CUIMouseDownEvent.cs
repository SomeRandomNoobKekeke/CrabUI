using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIMouseDownEvent : CUIMouseButtonEvent
  {
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseDown.Raise(this);
        Consumed = MEConsumer.ConsumeMouseEvents || Consumed;
      }
    }

    public CUIMouseDownEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(button, mouse) { }
    public override string ToString() => $"{Button} Down";
  }
}