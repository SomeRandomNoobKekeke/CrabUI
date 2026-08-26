using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;

namespace CursedUI
{
  public class RoutableCommandNode : RoutableCommandNodeBase
  {
    public DictOfLists<string, Action<object>> Listeners { get; } = new();

    public override void Execute(RoutableCommand command)
    {
      if (Listeners.ContainsKey(command.name))
      {
        foreach (Action<object> action in Listeners[command.name])
        {
          action(command.data);
        }
      }
    }
  }
}