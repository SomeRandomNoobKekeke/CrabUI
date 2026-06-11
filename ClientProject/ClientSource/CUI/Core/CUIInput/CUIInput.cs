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
    //TODO idk, are InputSettings part of CUIInput? Should they be created here or passed from CUICore?
    public InputSettings InputSettings { get; } = new();

    public KeyboardInput Keyboard { get; }
    public MouseInput Mouse { get; }
    public bool SomethingHappened => Mouse.SomethingHappened || Keyboard.SomethingHappened;

    public void Update(double totalTime, MouseState mouse, KeyboardState keyboard, TextInputEventPack textInput)
    {
      Mouse.Update(totalTime, mouse);
      Keyboard.Update(totalTime, keyboard, textInput);
    }

    public CUIInput()
    {
      Mouse = new MouseInput(InputSettings);
      Keyboard = new KeyboardInput();
    }
  }
}