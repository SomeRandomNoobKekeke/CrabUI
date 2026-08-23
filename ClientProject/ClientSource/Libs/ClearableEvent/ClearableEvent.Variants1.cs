using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class ClearableEvent<T1> : ClearableEventBase
  {
    private event Action<T1> Event;
    public bool Empty => Event == null;
    public event Action<Action<T1>> OnSubscribed;
    public event Action<Action<T1>> OnUnSubscribed;
    public EventSubscription Add(Action<T1> callback)
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
    public void Remove(Action<T1> callback)
    {
      Event -= callback;
      OnUnSubscribed?.Invoke(callback);
    }

    public override void Raise(object arg1) => Raise((T1)arg1);
    public void Raise(T1 arg1) => Event?.Invoke(arg1);
    public void Clear()
    {
      if (Event is null) return;

      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= (Action<T1>)callback;
      }
    }

    public override EventSubscription Add(Delegate callback) => Add((Action<T1>)callback);
    protected override Delegate DefaultMapping(IClearableEvent next) => DefaultMapping((ClearableEvent<T1>)next);
    private Action<T1> DefaultMapping(ClearableEvent<T1> next) => next.Raise;
  }
}