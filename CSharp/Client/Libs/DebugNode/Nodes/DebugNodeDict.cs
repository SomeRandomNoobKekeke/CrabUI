using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugNodeDict : Dictionary<string, DebugNodeBase>
  {

  }

  public static class DebugNodeDict_Exetnsions
  {
    public static void Map(this Dictionary<string, DebugNodeBase> self, Dictionary<string, DebugRelay> next)
    {
      foreach (string key in next.Keys)
      {
        if (self.ContainsKey(key))
        {
          next[key].Route(self[key]);
        }
      }
    }

    public static void Map(this Dictionary<string, DebugNodeBase> self, DebugRelayBase next)
    {
      foreach (DebugNodeBase node in self.Values)
      {
        next.Route(node);
      }
    }


    public static void Unmap(this Dictionary<string, DebugNodeBase> self, Dictionary<string, DebugRelay> next)
    {
      foreach (string key in next.Keys)
      {
        if (self.ContainsKey(key))
        {
          next[key].Unroute(self[key]);
        }
      }
    }

    public static void Unmap(this Dictionary<string, DebugNodeBase> self, DebugRelayBase next)
    {
      foreach (DebugNodeBase node in self.Values)
      {
        next.Unroute(node);
      }
    }
  }

}