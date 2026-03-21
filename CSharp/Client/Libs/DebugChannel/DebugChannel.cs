using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugChannel<T1> : DebugChannelBase
  {
    public Func<T1, string> ToText { get; set; }
    public Func<T1, bool> Condition { get; set; }

    public Type[] ArgTypes => [
      typeof(T1),
    ];

    public ReusableTuple<T1> Last { get; private set; } = new();

    public override string Msg => ToText?.Invoke(
      Last.Item1
    ) ?? Last.ToString();
    public override object[] Args => Last.ToArray();

    public ClearableEvent<T1> Event { get; } = new();

    public void Send(T1 arg1)
    {
      if (Open && (Condition == null || Condition.Invoke(arg1)))
      {
        Last.Update(arg1);
        Event.Raise(arg1);
        OnMsg.Raise(this);
      }
    }

    public override void Dispose()
    {
      Event.Clear();
      ToText = null;
      Condition = null;
      Last = null;
      base.Dispose();
    }
  }

  public class DebugChannel<T1, T2> : DebugChannelBase
  {
    public Func<T1, T2, string> ToText { get; set; }
    public Func<T1, T2, bool> Condition { get; set; }

    public Type[] ArgTypes => [
      typeof(T1),
      typeof(T2),
    ];

    public ReusableTuple<T1, T2> Last { get; private set; } = new();

    public override string Msg => ToText?.Invoke(
      Last.Item1,
      Last.Item2
    ) ?? Last.ToString();
    public override object[] Args => Last.ToArray();

    public ClearableEvent<T1, T2> Event { get; } = new();

    public void Send(T1 arg1, T2 arg2)
    {
      if (Open && (Condition == null || Condition.Invoke(arg1, arg2)))
      {
        Last.Update(arg1, arg2);
        Event.Raise(arg1, arg2);
        OnMsg.Raise(this);
      }
    }

    public override void Dispose()
    {
      Event.Clear();
      ToText = null;
      Condition = null;
      Last = null;
      base.Dispose();
    }
  }

  public class DebugChannel<T1, T2, T3> : DebugChannelBase
  {
    public Func<T1, T2, T3, string> ToText { get; set; }
    public Func<T1, T2, T3, bool> Condition { get; set; }

    public Type[] ArgTypes => [
      typeof(T1),
      typeof(T2),
      typeof(T3),
    ];

    public ReusableTuple<T1, T2, T3> Last { get; private set; } = new();

    public override string Msg => ToText?.Invoke(
      Last.Item1,
      Last.Item2,
      Last.Item3
    ) ?? Last.ToString();
    public override object[] Args => Last.ToArray();

    public ClearableEvent<T1, T2, T3> Event { get; } = new();

    public void Send(T1 arg1, T2 arg2, T3 arg3)
    {
      if (Open && (Condition == null || Condition.Invoke(arg1, arg2, arg3)))
      {
        Last.Update(arg1, arg2, arg3);
        Event.Raise(arg1, arg2, arg3);
        OnMsg.Raise(this);
      }
    }

    public override void Dispose()
    {
      Event.Clear();
      ToText = null;
      Condition = null;
      Last = null;
      base.Dispose();
    }
  }

  public class DebugChannel<T1, T2, T3, T4> : DebugChannelBase
  {
    public Func<T1, T2, T3, T4, string> ToText { get; set; }
    public Func<T1, T2, T3, T4, bool> Condition { get; set; }

    public Type[] ArgTypes => [
      typeof(T1),
      typeof(T2),
      typeof(T3),
      typeof(T4),
    ];

    public ReusableTuple<T1, T2, T3, T4> Last { get; private set; } = new();

    public override string Msg => ToText?.Invoke(
      Last.Item1,
      Last.Item2,
      Last.Item3,
      Last.Item4
    ) ?? Last.ToString();
    public override object[] Args => Last.ToArray();

    public ClearableEvent<T1, T2, T3, T4> Event { get; } = new();

    public void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      if (Open && (Condition == null || Condition.Invoke(arg1, arg2, arg3, arg4)))
      {
        Last.Update(arg1, arg2, arg3, arg4);
        Event.Raise(arg1, arg2, arg3, arg4);
        OnMsg.Raise(this);
      }
    }

    public override void Dispose()
    {
      Event.Clear();
      ToText = null;
      Condition = null;
      Last = null;
      base.Dispose();
    }
  }
}