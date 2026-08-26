using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public class CUIMouseUpEvent : CUIMouseButtonEvent
  {
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseUp.Raise(this);
      }
    }

    public CUIMouseUpEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(button, mouse) { }
    public override string ToString() => $"{Button} Up";
  }
}