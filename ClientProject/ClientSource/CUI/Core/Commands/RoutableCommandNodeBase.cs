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
  public abstract class RoutableCommandNodeBase
  {
    public RoutableCommandNodeBase Parent { get; set; }
    public List<RoutableCommandNodeBase> Children { get; } = new();

    public void AddChild(RoutableCommandNodeBase child)
    {
      child.Parent = this;
      Children.Add(child);
    }

    public void RemoveChild(RoutableCommandNodeBase child)
    {
      child.Parent = null;
      Children.Remove(child);
    }

    public void RemoveSelf() => Parent?.RemoveChild(this);


    public abstract void Execute(RoutableCommand command);
    public virtual void SendDown(RoutableCommand command)
    {
      if (Children.Count == 0) return;

      for (int i = Children.Count - 1; i >= 0; i--)
      {
        RoutableCommandNodeBase node = Children[i];
        node.Execute(command);
        node.SendDown(command);
      }
    }

    public virtual void SendUp(RoutableCommand command)
    {
      Parent?.Execute(command);
      Parent?.SendUp(command);
    }
  }
}