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
      public class MouseButtonInput
      {
        private InputSettings Settings;
        public CUIMouseButton Type { get; }

        public ButtonState State;
        public ButtonState PrevState;

        public bool Down;
        public bool Up;
        public bool Pressed;
        public bool Click;
        public bool DoubleClick;
        public bool Changed;


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