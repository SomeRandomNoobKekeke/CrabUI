using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class ClearableEvent : ClearableEventBase
  {
    protected event Action Event;
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


    public Dictionary<ClearableEvent, EventSubscription> Subscriptions = new();
    public void Map(ClearableEvent e)
    {
      Subscriptions[e] = this.Add(() => e.Raise());
    }

    public void Unmap(ClearableEvent e)
    {
      Subscriptions[e].Cancel();
      Subscriptions.Remove(e);
    }

    public void Route(ClearableEvent source) => source.Map(this);
    public void Unroute(ClearableEvent source) => source.Map(this);

    public void ClearMappings()
    {
      foreach (EventSubscription subscription in Subscriptions.Values)
      {
        subscription.Cancel();
      }
      Subscriptions.Clear();
    }
  }
}