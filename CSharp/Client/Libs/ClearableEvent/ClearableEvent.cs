using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class ClearableEvent
  {
    private event Action Event;
    public bool Empty => Event == null;

    public event Action<Action> OnSubscribed;
    public event Action<Action> OnUnSubscribed;

    public EventSubscription Add(Action callback)
    {
      Event += callback;
      OnSubscribed?.Invoke(callback);
      return new EventSubscription(() =>
      {
        Event -= callback;
        OnUnSubscribed?.Invoke(callback);
      });
    }
    public void Remove(Action callback)
    {
      Event -= callback;
      OnUnSubscribed?.Invoke(callback);
    }
    public void Raise() => Event?.Invoke();
    public void Clear()
    {
      if (Event is null) return;

      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= (Action)callback;
      }
    }
  }
}