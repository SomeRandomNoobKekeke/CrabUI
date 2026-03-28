using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{

  public class MapableEvent<T1> : ClearableEvent<T1>
  {
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

    public void ClearMappings()
    {
      foreach (EventSubscription subscription in Subscriptions.Values)
      {
        subscription.Cancel();
      }
      Subscriptions.Clear();
    }
  }

  public static partial class ClearableEvent_Extensions
  {
    public static void Route<T1>(this ClearableEvent<T1> target, MapableEvent<T1> source) => source.Map(target);
    public static void Unroute<T1>(this ClearableEvent<T1> target, MapableEvent<T1> source) => source.Map(target);
  }
}