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
    public TextInputEventPack ScanTextInput();
  }
}