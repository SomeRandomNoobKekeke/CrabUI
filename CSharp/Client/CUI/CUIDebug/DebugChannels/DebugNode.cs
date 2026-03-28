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
    public class FakeEvent
    {
      public DebugNode Self { get; set; }

      public EventSubscription Add(Delegate callback) => Self.AddToEvent(callback);
    }

    //CRINGE it's implemented in a most stupid way possible, make Clearable event accept delegate, juggle interfaces idk
    public FakeEvent Event { get; } = new();

    public static int MaxID { get; private set; } = 0;
    public int ID { get; }

    public string Name { get; set; }

    public ClearableEvent<DebugEvent> Pin { get; } = new();

    protected abstract EventSubscription AddToEvent(Delegate callback);
    public EventSubscription Route(DebugNode prev, Delegate callback) => prev.Map(this, callback);
    public abstract EventSubscription Map(DebugNode next, Delegate callback);
    public abstract EventSubscription Map(DebugNode next);

    public abstract EventSubscription Map(DebugNode<object> next);
    public abstract EventSubscription Map(DebugNode<object, object> next);
    public abstract EventSubscription Map(DebugNode<object, object, object> next);

    public abstract void Unmap(DebugNode node);
    public void Unroute(DebugNode node) => node.Unmap(this);

    public DebugNode()
    {
      ID = MaxID++;
      Event.Self = this;
    }

    public override int GetHashCode() => ID;

    public override bool Equals(object obj)
    {
      if (obj is not DebugNode other) return false;
      return other.ID == ID;
    }
  }
}