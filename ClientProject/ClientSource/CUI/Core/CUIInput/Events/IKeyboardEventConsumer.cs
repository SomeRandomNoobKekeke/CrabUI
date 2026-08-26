using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
namespace CursedUI
{
  public interface IKeyboardEventConsumer : IEventConsumer
  {
    public ClearableEvent<CUIKeyPressedEvent> KeyPressed { get; }
    public ClearableEvent<CUIKeyReleasedEvent> KeyReleased { get; }
    public ClearableEvent<CUITextInputEvent> TextInput { get; }
    public ClearableEvent<CUIKeyDownInputEvent> KeyDownInput { get; }
  }
}