using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class ClearableEvent<T1, T2, T3, T4> : ClearableEventBase
  {
    private event Action<T1, T2, T3, T4> Event;
    public bool Empty => Event == null;
    public event Action<Action<T1, T2, T3, T4>> OnSubscribed;
    public event Action<Action<T1, T2, T3, T4>> OnUnSubscribed;
    public EventSubscription Add(Action<T1, T2, T3, T4> callback)
    {
      ArgumentNullException.ThrowIfNull(callback);
      Event += callback;
      OnSubscribed?.Invoke(callback);
      return new EventSubscription(() =>
      {
        Event -= callback;
        OnUnSubscribed?.Invoke(callback);
      });
    }
    public void Remove(Action<T1, T2, T3, T4> callback)
    {
      Event -= callback;
      OnUnSubscribed?.Invoke(callback);
    }

    public override void Raise(object arg1, object arg2, object arg3, object arg4)
      => Raise((T1)arg1, (T2)arg2, (T3)arg3, (T4)arg4);
    public void Raise(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => Event?.Invoke(arg1, arg2, arg3, arg4);
    public void Clear()
    {
      if (Event is null) return;

      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= (Action<T1, T2, T3, T4>)callback;
      }
    }

    public override EventSubscription Add(Delegate callback) => Add((Action<T1, T2, T3, T4>)callback);
    protected override Delegate DefaultMapping(IClearableEvent next) => DefaultMapping((ClearableEvent<T1, T2, T3, T4>)next);
    private Action<T1, T2, T3, T4> DefaultMapping(ClearableEvent<T1, T2, T3, T4> next)
      => (T1 arg1, T2 arg2, T3 arg3, T4 arg4) => next.Raise(arg1, arg2, arg3, arg4);
  }
}