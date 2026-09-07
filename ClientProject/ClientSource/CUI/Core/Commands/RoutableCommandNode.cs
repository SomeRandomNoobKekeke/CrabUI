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
  //not used
  public class RoutableCommandNode
  {
    public RoutableCommandContract? CommandsContract { get; set; }
    public DictOfLists<string, Action<object>> Listeners { get; } = new();

    public RoutableCommandNode Parent { get; set; }
    public List<RoutableCommandNode> Children { get; } = new();

    public void AddChild(RoutableCommandNode child)
    {
      child.Parent = this;
      Children.Add(child);
    }

    public void RemoveChild(RoutableCommandNode child)
    {
      child.Parent = null;
      Children.Remove(child);
    }

    public void RemoveSelf() => Parent?.RemoveChild(this);


    public bool ListenFor(string name, Action<object> action)
    {
      if (CommandsContract?.CanConsume(name) == false) return false;
      Listeners.Add(name, action);
      return true;
    }

    public bool Execute(RoutableCommand command)
    {
      if (Listeners.ContainsKey(command.name))
      {
        if (CommandsContract?.CanConsume(command.name) == false) return false;

        foreach (Action<object> action in Listeners[command.name])
        {
          action(command.data);
        }
      }
      return true;
    }

    public bool SendDown(RoutableCommand command)
    {
      if (CommandsContract?.CanSendDown(command.name) == false) return false;
      if (Children.Count == 0) return true;

      for (int i = Children.Count - 1; i >= 0; i--)
      {
        Children[i].Execute(command);
        Children[i].SendDown(command);
      }
      return true;
    }

    public bool SendUp(RoutableCommand command)
    {
      if (CommandsContract?.CanSendUp(command.name) == false) return false;
      Parent?.Execute(command);
      Parent?.SendUp(command);
      return true;
    }


  }
}