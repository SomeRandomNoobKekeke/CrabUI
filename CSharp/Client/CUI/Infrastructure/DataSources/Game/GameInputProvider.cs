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
  public class GameInputProvider : IInputProvider
  {
    public MouseState ScanMouse() => Mouse.GetState();
    public KeyboardState ScanKeyboard() => Keyboard.GetState();
  }
}