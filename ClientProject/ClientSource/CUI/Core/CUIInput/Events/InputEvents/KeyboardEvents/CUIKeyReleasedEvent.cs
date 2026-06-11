using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public class CUIKeyReleasedEvent : CUIKeyboardEvent
  {
    public Keys Key { get; }

    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IKeyboardEventConsumer KEConsumer)
      {
        KEConsumer.KeyReleased.Raise(this);
      }
    }

    public CUIKeyReleasedEvent(Keys key, CUIInput.KeyboardInput keyboard) : base(keyboard) => Key = key;

    public override string ToString() => $"{Key} Key Released";
  }
}