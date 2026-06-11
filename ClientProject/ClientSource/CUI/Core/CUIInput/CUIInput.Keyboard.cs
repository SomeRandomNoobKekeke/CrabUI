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
    public partial class KeyboardInput
    {
      public KeyboardState State { get; private set; }
      public KeyboardState PrevState { get; private set; }
      public bool SomethingHappened { get; private set; }

      private Keys[] PrevPressedKeys = [];

      public Keys[] PressedKeys = [];
      public Keys[] ReleasedKeys = [];

      public bool IsKeyPressed(Keys key) => PressedKeys.Contains(key);
      public bool IsKeyReleased(Keys key) => ReleasedKeys.Contains(key);
      public bool IsKeyDown(Keys key) => State.IsKeyDown(key);
      public bool IsKeyUp(Keys key) => State.IsKeyUp(key);

      public void Update(double totalTime, KeyboardState newState)
      {
        PrevState = State;
        State = newState;

        Keys[] pressedKeys = State.GetPressedKeys();

        PressedKeys = pressedKeys.Except(PrevPressedKeys).ToArray();
        ReleasedKeys = PrevPressedKeys.Except(pressedKeys).ToArray();

        PrevPressedKeys = pressedKeys;

        SomethingHappened = PressedKeys.Length != 0 || ReleasedKeys.Length != 0;
      }
    }
  }
}