using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  /// <summary>
  /// Just to show that this is not a random dict, it's special
  /// </summary>
  public class ClearableEventDict : Dictionary<string, ClearableEventBase> { }

  public static class ClearableEventDict_Extensions
  {
    public static void Map(this Dictionary<string, ClearableEventBase> self, Dictionary<string, ClearableEventBase> next)
    {
      foreach (string key in self.Keys)
      {
        if (next.ContainsKey(key)) self[key].Map(next[key]);
      }
    }

    public static void Unmap(this Dictionary<string, ClearableEventBase> self, Dictionary<string, ClearableEventBase> next)
    {
      foreach (string key in self.Keys)
      {
        if (next.ContainsKey(key)) self[key].Unmap(next[key]);
      }
    }

    public static void Route(this Dictionary<string, ClearableEventBase> self, Dictionary<string, ClearableEventBase> prev) => prev.Map(self);
    public static void Unroute(this Dictionary<string, ClearableEventBase> self, Dictionary<string, ClearableEventBase> prev) => prev.Unmap(self);


    public static EventT Get<EventT>(this Dictionary<string, ClearableEventBase> self, string key) where EventT : ClearableEventBase
      => (EventT)self[key];
  }
}