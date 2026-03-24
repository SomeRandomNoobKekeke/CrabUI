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
    public event Action<Action<T1>> OnSubscribed;
    public event Action<Action<T1>> OnUnSubscribed;
    public EventSubscription Add(Action<T1> callback)
    {
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
    public void Raise(T1 arg1) => Event?.Invoke(arg1);
    public void Clear()
    {
      if (Event is null) return;

      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= (Action<T1>)callback;
      }
    }

    public EventBridge<T1> CreateBridge(Action<T1> action)
    {
      return new EventBridge<T1>(this)
      {
        Action = action
      };
    }
  }
}