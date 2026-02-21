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
      public MouseState State { get; private set; }
      public MouseState PrevState { get; private set; } // not used?

      public Vector2 Pos { get; private set; }
      public Vector2 PosDiff { get; private set; }
      public bool Moved { get; private set; }
      public bool SomethingHappened { get; private set; }
      public MouseButtonInput M1 { get; }
      public MouseButtonInput M2 { get; }
      public List<MouseButtonInput> Buttons { get; }


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
        M1 = new MouseButtonInput(settings, CUIMouseButton.LeftButton);
        M2 = new MouseButtonInput(settings, CUIMouseButton.RightButton);
        Buttons = new List<MouseButtonInput>(){
          M1,M2
        };
      }
    }
  }
}