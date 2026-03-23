using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;


namespace CrabUI
{
  public abstract class DebugNode<T1> : DebugNodeBase
  {
    public ClearableEvent<T1> Event { get; } = new();

    public Func<T1, string> ToText { get; set; }

    public void Send(T1 arg1)
    {
      Event.Raise(arg1);

      if (!Pin.Empty)
      {
        Pin.Raise(new DebugEvent(new object[] { arg1 }, Name, ToText?.Invoke(arg1)));
      }
    }

    public override EventSubscription Map(Delegate callback) => Map((Action<T1>)callback);
    public EventSubscription Map(Action<T1> callback) => Event.Add(callback);
    public EventSubscription Map(DebugNode<T1> target)
    {
      return Event.Add((T1 arg1) => target.Event.Raise(arg1));
    }

    public void Clear() => Event.Clear();
  }

  public abstract class DebugNode<T1, T2> : DebugNodeBase
  {
    public ClearableEvent<T1, T2> Event { get; } = new();
    public Func<T1, T2, string> ToText { get; set; }

    public void Send(T1 arg1, T2 arg2)
    {
      Call(arg1, arg2);
    }

    private void Call(T1 arg1, T2 arg2)
    {
      Event.Raise(arg1, arg2);
      Pin.Raise(new DebugEvent(new object[] { arg1, arg2 }, Name, ToText?.Invoke(arg1, arg2)));
    }

    public override EventSubscription Map(Delegate callback)
    {
      return Map((Action<T1, T2>)callback);
    }
    public EventSubscription Map(Action<T1, T2> callback)
    {
      return Event.Add(callback);
    }
    public EventSubscription Map(DebugNode<T1, T2> target)
    {
      return Event.Add((T1 arg1, T2 arg2) => target.Call(arg1, arg2));
    }

    public void Clear() => Event.Clear();
  }
}