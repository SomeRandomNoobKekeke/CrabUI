using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public abstract class DebugNode
  {
    public static int MaxID { get; private set; } = 0;
    public int ID { get; }

    public string Name { get; set; }

    public ClearableEvent<DebugEvent> Pin { get; } = new();

    public EventSubscription Route(DebugNode prev, Delegate callback) => prev.Map(this, callback);
    public abstract EventSubscription Map(DebugNode next, Delegate callback);

    public abstract void Unmap(DebugNode node);
    public void Unroute(DebugNode node) => node.Unmap(this);

    public DebugNode()
    {
      ID = MaxID++;
    }

    public override int GetHashCode() => ID;

    public override bool Equals(object obj)
    {
      if (obj is not DebugNode other) return false;
      return other.ID == ID;
    }
  }
}