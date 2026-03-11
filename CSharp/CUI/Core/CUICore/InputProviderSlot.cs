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
  public class InputProviderSlot : IInputProvider
  {
    public IInputProvider InputProvider { get; set; }

    public MouseState ScanMouse() => InputProvider?.ScanMouse() ?? default;
    public KeyboardState ScanKeyboard() => InputProvider?.ScanKeyboard() ?? default;
  }
}