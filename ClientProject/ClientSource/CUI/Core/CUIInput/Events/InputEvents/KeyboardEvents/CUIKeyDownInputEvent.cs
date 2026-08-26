using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CursedUI
{
  public class CUIKeyDownInputEvent : CUIKeyboardEvent
  {
    public TextInputEventArgs Args { get; }

    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IKeyboardEventConsumer KEConsumer)
      {
        KEConsumer.KeyDownInput.Raise(this);
      }
    }

    public CUIKeyDownInputEvent(TextInputEventArgs args, CUIInput.KeyboardInput keyboard) : base(keyboard) => Args = args;

    public override string ToString() => $"{Args.Key} Key Down Input";
  }
}