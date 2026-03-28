using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class ClearableEvent<T1> : ClearableEventBase
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


    public Dictionary<ClearableEvent<T1>, EventSubscription> Subscriptions = new();

    public void Map(ClearableEvent<T1> e)
    {
      Subscriptions[e] = this.Add((arg1) => e.Raise(arg1));
    }

    public void Unmap(ClearableEvent<T1> e)
    {
      Subscriptions[e].Cancel();
      Subscriptions.Remove(e);
    }

    public void Route(ClearableEvent<T1> source) => source.Map(this);
    public void Unroute(ClearableEvent<T1> source) => source.Map(this);

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