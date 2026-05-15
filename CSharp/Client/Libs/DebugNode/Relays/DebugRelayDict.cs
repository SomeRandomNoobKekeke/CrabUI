using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugRelayDict : Dictionary<string, DebugRelay>
  {

  }

  public static class DebugRelayDict_Exetnsions
  {
    public static void Map(this Dictionary<string, DebugRelay> self, Dictionary<string, DebugRelay> next)
    {
      foreach (string key in next.Keys)
      {
        if (self.ContainsKey(key))
        {
          next[key].Route(self[key]);
        }
      }
    }

    public static void Route(this Dictionary<string, DebugRelay> self, Dictionary<string, DebugRelay> prev)
    {
      foreach (string key in self.Keys)
      {
        if (prev.ContainsKey(key))
        {
          self[key].Route(prev[key]);
        }
      }
    }

    public static void Map(this Dictionary<string, DebugRelay> self, DebugRelayBase next)
    {
      foreach (DebugRelay node in self.Values)
      {
        next.Route(node);
      }
    }

    public static void Open(this Dictionary<string, DebugRelay> self)
    {
      foreach (DebugRelay relay in self.Values) { relay.Open(); }
    }
    public static void Open(this Dictionary<string, DebugRelay> self, string type)
    {
      foreach (DebugRelay relay in self.Values) { relay.Open(type); }
    }
    public static void Close(this Dictionary<string, DebugRelay> self)
    {
      foreach (DebugRelay relay in self.Values) { relay.Close(); }
    }
    public static void Close(this Dictionary<string, DebugRelay> self, string type)
    {
      foreach (DebugRelay relay in self.Values) { relay.Close(type); }
    }
    public static void Toggle(this Dictionary<string, DebugRelay> self)
    {
      foreach (DebugRelay relay in self.Values) { relay.Toggle(); }
    }
    public static void Toggle(this Dictionary<string, DebugRelay> self, string type)
    {
      foreach (DebugRelay relay in self.Values) { relay.Toggle(type); }
    }


    public static IEnumerable<DebugNodeBase> GetNodes(this Dictionary<string, DebugRelay> self, string type)
    {
      foreach (DebugRelay relay in self.Values)
      {
        foreach (DebugNodeBase node in relay.GetNodes(type))
        {
          yield return node;
        }
      }
    }

    public static IEnumerable<DebugNodeBase> GetNodes(this Dictionary<string, DebugRelay> self)
    {
      foreach (DebugRelay relay in self.Values)
      {
        foreach (DebugNodeBase node in relay.GetNodes())
        {
          yield return node;
        }
      }
    }
  }

}