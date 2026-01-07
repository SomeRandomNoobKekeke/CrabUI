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
    public partial class MouseInput
    {
      public MouseState State;
      public MouseState PrevState; // not used?

      public Vector2 Pos;
      public Vector2 PosDiff;
      public bool Moved;
      public bool SomethingHappened;
      public MouseButtonInput M1 { get; }
      public MouseButtonInput M2 { get; }


      private Vector2 PrevPos;

      public void Update(double totalTime, MouseState newState)
      {
        PrevState = State;
        State = newState;

        M1.Update(totalTime, State.LeftButton);
        M2.Update(totalTime, State.RightButton);

        PrevPos = Pos;
        Pos = new Vector2(State.X, State.Y);
        PosDiff = Pos - PrevPos;
        Moved = PosDiff != Vector2.Zero;

        SomethingHappened = M1.Changed || M2.Changed || Moved;

        //TODO Scroll
      }

      public MouseInput(InputSettings settings)
      {
        M1 = new MouseButtonInput(settings);
        M2 = new MouseButtonInput(settings);
      }
    }
  }
}