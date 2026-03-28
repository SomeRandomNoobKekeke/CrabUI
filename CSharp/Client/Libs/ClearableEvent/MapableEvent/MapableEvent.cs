using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  //TODO rework debug channels with this mb, 
  // i already implemented this mapping functionality in debug channels 
  // And now it turns out that i need it in other places too and i have to duplicate code
  public class MapableEvent : ClearableEvent
  {
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

    public void ClearMappings()
    {
      foreach (EventSubscription subscription in Subscriptions.Values)
      {
        subscription.Cancel();
      }
      Subscriptions.Clear();
    }
  }

  //THINK should all this just be part of ClearableEvent?
  public static partial class ClearableEvent_Extensions
  {
    public static void Route(this ClearableEvent target, MapableEvent source) => source.Map(target);
    public static void Unroute(this ClearableEvent target, MapableEvent source) => source.Map(target);
  }
}