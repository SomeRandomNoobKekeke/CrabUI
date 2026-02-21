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
  public interface IInputProvider
  {
    public MouseState ScanMouse();
    public KeyboardState ScanKeyboard();
  }

  public class InputScanner : IInputProvider
  {
    public IInputProvider InputProvider;

    public MouseState ScanMouse() => InputProvider?.ScanMouse() ?? default;
    public KeyboardState ScanKeyboard() => InputProvider?.ScanKeyboard() ?? default;
  }
}