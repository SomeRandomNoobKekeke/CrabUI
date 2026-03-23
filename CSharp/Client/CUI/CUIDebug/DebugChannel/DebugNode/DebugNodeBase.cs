using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;


namespace CrabUI
{
  public abstract class DebugNodeBase
  {
    public static int MaxID { get; private set; } = 0;
    public int ID { get; }

    public string Name { get; set; }

    public ClearableEvent<DebugEvent> Pin { get; } = new();
    public abstract EventSubscription Map(Delegate callback);

    public DebugNodeBase()
    {
      ID = MaxID++;
    }

    public override int GetHashCode() => ID;

    public override bool Equals(object obj)
    {
      if (obj is not DebugNodeBase other) return false;
      return other.ID == ID;
    }
  }
}