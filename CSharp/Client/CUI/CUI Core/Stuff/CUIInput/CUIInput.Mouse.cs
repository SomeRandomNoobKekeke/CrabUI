using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public partial class CUIInput
  {
    public MouseState Current;
    public MouseState Previous;

    public Vector2 MousePosition;

    public bool M1Down;


    public void UpdateMouse()
    {
      Previous = Current;
      Current = Environment.InputScanner.ScanMouse();

      MousePosition = new Vector2(Current.Position.X, Current.Position.Y);

      M1Down = Previous.LeftButton == ButtonState.Released && Current.LeftButton == ButtonState.Pressed;
    }
  }
}