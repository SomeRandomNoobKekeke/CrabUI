using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public abstract class CUIKeyboardEvent : InputEvent
  {
    public CUIInput.KeyboardInput Keyboard { get; }

    public CUIKeyboardEvent(CUIInput.KeyboardInput keyboard) => Keyboard = keyboard;
  }
}