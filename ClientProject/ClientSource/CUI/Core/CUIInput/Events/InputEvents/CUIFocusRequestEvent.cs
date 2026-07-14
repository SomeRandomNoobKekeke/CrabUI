using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIFocusRequestEvent : InputEvent
  {
    public void Accept(IFocusable c)
    {

    }
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IFocusRequestEventConsumer FEConsumer)
      {
        FEConsumer.FocusRequested.Raise(this);
      }
    }
  }
}