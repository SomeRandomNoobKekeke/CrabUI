using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CursedUI
{
  public class TextInputEventPackBuilder
  {
    public List<TextInputEventArgs> TextInputEvents { get; } = new();
    public List<TextInputEventArgs> KeyDownEvents { get; } = new();

    public TextInputEventPack Build()
    {
      TextInputEventPack pack = new TextInputEventPack(
        TextInputEvents.ToArray(),
        KeyDownEvents.ToArray()
      );

      TextInputEvents.Clear();
      KeyDownEvents.Clear();

      return pack;
    }
  }
}