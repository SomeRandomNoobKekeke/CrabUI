using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using BaroJunk;

namespace CrabUI
{
  public class RoutableCommandNode : RoutableCommandNodeBase
  {
    public Dictionary<string, Action<object>> Listeners { get; } = new();

    public override void Process(RoutableCommand command)
    {
      if (Listeners.ContainsKey(command.name))
      {
        Listeners[command.name](command.data);
      }
    }
  }
}