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
  public interface IFocusRequestEventConsumer : IEventConsumer
  {
    public bool ConsumeFocus { get; }
    public ClearableEvent<CUIFocusRequestEvent> FocusProbed { get; }
  }
}