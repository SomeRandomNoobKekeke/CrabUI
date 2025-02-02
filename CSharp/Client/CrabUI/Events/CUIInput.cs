using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Containing a snapshot of current mouse and keyboard state
  /// </summary>
  public class CUIInput
  {
    public static double DoubleClickInterval = 0.2;
    public static float ScrollSpeed = 0.6f;



    public MouseState Mouse;
    public bool MouseDown;
    public bool DoubleClick;
    public bool MouseUp;
    public bool MouseHeld;
    public float Scroll;
    public bool Scrolled;
    public Vector2 MousePosition;
    public Vector2 MousePositionDif;
    public bool MouseMoved;
    //TODO split into sh mouse and sh keyboard
    public bool SomethingHappened;

    //HACK rethink, this is too hacky
    public bool ClickConsumed;

    public KeyboardState Keyboard;
    public Keys[] HeldKeys = new Keys[0];
    public Keys[] PressedKeys = new Keys[0];
    public Keys[] UnpressedKeys = new Keys[0];
    public bool SomeKeyHeld;
    public bool SomeKeyPressed;
    public bool SomeKeyUnpressed;
    public TextInputEventArgs[] WindowTextInputEvents;
    public TextInputEventArgs[] WindowKeyDownEvents;
    public bool SomeWindowEvents;


    //-------------- private stuff
    private double PrevMouseDownTiming;
    private int PrevScrollWheelValue;
    private MouseState PrevMouseState;
    private Vector2 PrevMousePosition;
    private Keys[] PrevHeldKeys = new Keys[0];
    private Queue<TextInputEventArgs> WindowTextInputQueue = new Queue<TextInputEventArgs>(10);
    private Queue<TextInputEventArgs> WindowKeyDownQueue = new Queue<TextInputEventArgs>(10);

    //HACK super hacky solution to block input from one CUIMainComponent to another
    public bool MouseInputHandled { get; set; }

    public void Scan(double totalTime)
    {
      MouseInputHandled = false;
      ScanMouse(totalTime);
      ScanKeyboard(totalTime);
    }

    private void ScanMouse(double totalTime)
    {
      ClickConsumed = false;

      Mouse = Microsoft.Xna.Framework.Input.Mouse.GetState();

      MouseDown = PrevMouseState.LeftButton == ButtonState.Released && Mouse.LeftButton == ButtonState.Pressed;
      MouseUp = PrevMouseState.LeftButton == ButtonState.Pressed && Mouse.LeftButton == ButtonState.Released;
      MouseHeld = Mouse.LeftButton == ButtonState.Pressed;

      PrevMousePosition = MousePosition;
      MousePosition = new Vector2(Mouse.Position.X, Mouse.Position.Y);
      MousePositionDif = MousePosition - PrevMousePosition;
      MouseMoved = MousePositionDif != Vector2.Zero;

      Scroll = (Mouse.ScrollWheelValue - PrevScrollWheelValue) * ScrollSpeed;
      PrevScrollWheelValue = Mouse.ScrollWheelValue;
      Scrolled = Scroll != 0;

      DoubleClick = false;

      if (MouseDown)
      {
        if (totalTime - PrevMouseDownTiming < DoubleClickInterval)
        {
          DoubleClick = true;
        }

        PrevMouseDownTiming = totalTime;
      }

      SomethingHappened = MouseHeld || MouseUp || MouseDown || MouseMoved || Scrolled;

      PrevMouseState = Mouse;
    }

    private void ScanKeyboard(double totalTime)
    {
      Keyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();
      HeldKeys = Keyboard.GetPressedKeys();
      SomeKeyHeld = HeldKeys.Length > 0;

      PressedKeys = HeldKeys.Except(PrevHeldKeys).ToArray();
      UnpressedKeys = PrevHeldKeys.Except(HeldKeys).ToArray();

      SomeKeyPressed = PressedKeys.Length > 0;
      SomeKeyUnpressed = UnpressedKeys.Length > 0;

      PrevHeldKeys = HeldKeys;

      WindowTextInputEvents = WindowTextInputQueue.ToArray();
      WindowTextInputQueue.Clear();

      WindowKeyDownEvents = WindowKeyDownQueue.ToArray();
      WindowKeyDownQueue.Clear();


      SomeWindowEvents = WindowTextInputEvents.Length > 0 || WindowKeyDownEvents.Length > 0;
    }

    public CUIInput()
    {
      CUI.OnWindowKeyDown += (e) => WindowKeyDownQueue.Enqueue(e);
      CUI.OnWindowTextInput += (e) => WindowTextInputQueue.Enqueue(e);
    }

  }
}