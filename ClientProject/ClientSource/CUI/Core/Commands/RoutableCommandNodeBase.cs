using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;

namespace CrabUI
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


    public abstract void Process(RoutableCommand command);
    public virtual void SendDown(RoutableCommand command)
    {
      for (int i = Children.Count - 1; i <= 0; i--)
      {
        RoutableCommandNodeBase node = Children[i];
        node.Process(command);
        node.SendDown(command);
      }
    }

    public virtual void SendUp(RoutableCommand command)
    {
      Parent?.Process(command);
      Parent?.SendUp(command);
    }
  }
}