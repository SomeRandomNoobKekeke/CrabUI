using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class ClearableEvent<T1>
  {
    private event Action<T1> Event;
    public bool Empty => Event == null;
    public EventSubscription Add(Action<T1> callback)
    {
      Event += callback;
      return new EventSubscription(() => Event -= callback);
    }
    public void Remove(Action<T1> callback) => Event -= callback;
    public void Raise(T1 arg1) => Event?.Invoke(arg1);
    public void Clear()
    {
      if (Event is null) return;

      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= (Action<T1>)callback;
      }
    }
  }

  public class ClearableEvent<T1, T2>
  {
    private event Action<T1, T2> Event;
    public bool Empty => Event == null;
    public EventSubscription Add(Action<T1, T2> callback)
    {
      Event += callback;
      return new EventSubscription(() => Event -= callback);
    }
    public void Remove(Action<T1, T2> callback) => Event -= callback;
    public void Raise(T1 arg1, T2 arg2) => Event?.Invoke(arg1, arg2);
    public void Clear()
    {
      if (Event is null) return;

      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= (Action<T1, T2>)callback;
      }
    }
  }

  public class ClearableEvent<T1, T2, T3>
  {
    private event Action<T1, T2, T3> Event;
    public bool Empty => Event == null;
    public EventSubscription Add(Action<T1, T2, T3> callback)
    {
      Event += callback;
      return new EventSubscription(() => Event -= callback);
    }
    public void Remove(Action<T1, T2, T3> callback) => Event -= callback;
    public void Raise(T1 arg1, T2 arg2, T3 arg3) => Event?.Invoke(arg1, arg2, arg3);
    public void Clear()
    {
      if (Event is null) return;

      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= (Action<T1, T2, T3>)callback;
      }
    }
  }
}