using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public class TextInputEventPackBuilder
  {
    public List<TextInputEventArgs> TextInputEvents { get; } = new();
    public List<TextInputEventArgs> KeyDownEvents { get; } = new();

    private bool SomethingFocusedElsewhere;

    //It doesn't belong here but i don't want to create 100 wrappers for some bool flags
    public void StealFocus() => SomethingFocusedElsewhere = true;

    public TextInputEventPack Build()
    {
      TextInputEventPack pack = new TextInputEventPack(
        TextInputEvents.ToArray(),
        KeyDownEvents.ToArray(),
        SomethingFocusedElsewhere
      );

      TextInputEvents.Clear();
      KeyDownEvents.Clear();
      SomethingFocusedElsewhere = false;

      return pack;
    }
  }
}