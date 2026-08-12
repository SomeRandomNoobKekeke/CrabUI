using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public class EventConstructor : IModule
  {
    public List<InputEvent> Events { get; } = new();
    public List<InputEvent> KeyboardEvents { get; } = new();

    public CUIMouseLeaveEvent MouseLeaveEvent { get; private set; }
    public CUIMouseEnterEvent MouseEnterEvent { get; private set; }
    public CUIMouseOnEvent MouseOnEvent { get; private set; }
    public CUIMouseOffEvent MouseOffEvent { get; private set; }

    public void Construct(CUIInput input)
    {
      Events.Clear();
      KeyboardEvents.Clear();

      MouseLeaveEvent = new(input.Mouse);
      MouseEnterEvent = new(input.Mouse);
      MouseOnEvent = new(input.Mouse);
      MouseOffEvent = new(input.Mouse);

      foreach (var button in input.Mouse.Buttons)
      {
        if (!button.Changed) continue;
        if (button.Down) Events.Add(new CUIMouseDownEvent(button.Type, input.Mouse));
        if (button.Up) Events.Add(new CUIMouseUpEvent(button.Type, input.Mouse));
        if (button.Click) Events.Add(new CUIMouseClickEvent(button.Type, input.Mouse));
        if (button.DoubleClick) Events.Add(new CUIMouseDoubleClickEvent(button.Type, input.Mouse));
      }

      if (input.Mouse.Moved)
      {
        Events.Add(new CUIMouseMovedEvent(input.Mouse));
      }

      if (input.Mouse.Scrolled)
      {
        Events.Add(new CUIMouseScrollEvent(input.Mouse));
      }


      foreach (Keys key in input.Keyboard.PressedKeys)
      {
        KeyboardEvents.Add(new CUIKeyPressedEvent(key, input.Keyboard));
      }

      foreach (Keys key in input.Keyboard.ReleasedKeys)
      {
        KeyboardEvents.Add(new CUIKeyReleasedEvent(key, input.Keyboard));
      }

      foreach (TextInputEventArgs args in input.Keyboard.TextInputEvents)
      {
        KeyboardEvents.Add(new CUITextInputEvent(args, input.Keyboard));
      }

      foreach (TextInputEventArgs args in input.Keyboard.KeyDownEvents)
      {
        KeyboardEvents.Add(new CUIKeyDownInputEvent(args, input.Keyboard));
      }

      // if (Events.Count != 0) Debug_Events.Send(Events);
    }
  }
}