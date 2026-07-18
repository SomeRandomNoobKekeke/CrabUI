using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIMouseDoubleClickEvent : CUIMouseButtonEvent
  {
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseDoubleClick.Raise(this);
        Consumed = MEConsumer.ConsumeMouseEvents || Consumed;
      }
    }

    public CUIMouseDoubleClickEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(button, mouse) { }

    public override string ToString() => $"{Button} Double Click";
  }
}