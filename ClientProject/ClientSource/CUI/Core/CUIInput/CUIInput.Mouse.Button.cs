using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CursedUI
{
  public partial class CUIInput
  {
    public partial class MouseInput
    {
      public class MouseButtonInput
      {
        private InputSettings Settings;
        public CUIMouseButton Type { get; }

        public ButtonState State { get; private set; }
        public ButtonState PrevState { get; private set; }

        public bool Down { get; private set; }
        public bool Up { get; private set; }
        public bool Pressed { get; private set; }
        public bool Click { get; private set; }
        public bool DoubleClick { get; private set; }
        public bool Changed { get; private set; }


        private double lastDownTime;
        private double lastClickTime;

        public void Update(double totalTime, ButtonState newState)
        {
          PrevState = State;
          State = newState;

          Changed = PrevState != State;

          Down = PrevState == ButtonState.Released && State == ButtonState.Pressed;
          Up = PrevState == ButtonState.Pressed && State == ButtonState.Released;
          Pressed = State == ButtonState.Pressed;

          Click = Up && totalTime - lastDownTime < Settings.ClickInterval;
          DoubleClick = Click && totalTime - lastClickTime < Settings.DoubleClickInterval;

          if (Down) lastDownTime = totalTime;
          if (Click) lastClickTime = totalTime;
        }

        public MouseButtonInput(InputSettings settings, CUIMouseButton type)
        {
          Settings = settings;
          Type = type;
        }
      }
    }
  }
}