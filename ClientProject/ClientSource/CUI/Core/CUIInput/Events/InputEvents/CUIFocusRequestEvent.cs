using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIFocusRequestEvent(CUIInput input) : InputEvent
  {
    public CUIInput Input { get; } = input;

    public IFocusable Acceptor { get; private set; }

    public void Accept(IFocusable acceptor)
    {
      Acceptor = acceptor;
    }
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IFocusRequestEventConsumer FEConsumer)
      {
        FEConsumer.FocusProbed.Raise(this);
        Consumed = FEConsumer.ConsumeFocus || Consumed;
      }
    }
  }
}