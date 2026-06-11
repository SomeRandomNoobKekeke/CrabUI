using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
namespace CrabUI
{
  public interface IKeyboardEventConsumer : IEventConsumer
  {
    public ClearableEvent<CUIKeyPressedEvent> KeyPressed { get; }
    public ClearableEvent<CUIKeyReleasedEvent> KeyReleased { get; }
  }
}