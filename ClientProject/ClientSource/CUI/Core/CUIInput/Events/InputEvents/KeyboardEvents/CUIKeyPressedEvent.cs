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
  public class CUIKeyPressedEvent : CUIKeyboardEvent
  {
    public Keys Key { get; }

    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IKeyboardEventConsumer KEConsumer)
      {
        KEConsumer.KeyPressed.Raise(this);
      }
    }

    public CUIKeyPressedEvent(Keys key, CUIInput.KeyboardInput keyboard) : base(keyboard) => Key = key;

    public override string ToString() => $"{Key} Key Pressed";
  }
}