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
  public class DefaultInputProvider : IInputProvider
  {
    public MouseState ScanMouse() => Microsoft.Xna.Framework.Input.Mouse.GetState();
    public KeyboardState ScanKeyboard() => Microsoft.Xna.Framework.Input.Keyboard.GetState();
  }
}