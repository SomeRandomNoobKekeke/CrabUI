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
  public class RoutableCommandNode
  {
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


    public void ListenFor(string name, Action<object> action)
    {
      Listeners.Add(name, action);
    }

    public void Execute(RoutableCommand command)
    {
      if (Listeners.ContainsKey(command.name))
      {
        foreach (Action<object> action in Listeners[command.name])
        {
          action(command.data);
        }
      }
    }
    public void SendDown(RoutableCommand command)
    {
      if (Children.Count == 0) return;

      for (int i = Children.Count - 1; i >= 0; i--)
      {
        Children[i].Execute(command);
        Children[i].SendDown(command);
      }
    }

    public void SendUp(RoutableCommand command)
    {
      Parent?.Execute(command);
      Parent?.SendUp(command);
    }


  }
}