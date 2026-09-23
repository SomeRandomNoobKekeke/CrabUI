using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class Debug2RelayDict : Dictionary<string, Debug2Relay>
  {

  }

  public static class Debug2RelayDict_Exetnsions
  {
    public static void Map(this Dictionary<string, Debug2Relay> self, Dictionary<string, Debug2Relay> next)
    {
      foreach (string key in self.Keys)
      {
        if (next.ContainsKey(key))
        {
          next[key].Route(self[key]);
        }
        else
        {
          Debug2NodeSetup.Logger.Warning($"Debug2RelayDict can't be fully mapped, next doesn't have [{key}]");
        }
      }
    }

    public static void Route(this Dictionary<string, Debug2Relay> self, Dictionary<string, Debug2Relay> prev)
    {
      foreach (string key in self.Keys)
      {
        if (prev.ContainsKey(key))
        {
          self[key].Route(prev[key]);
        }
      }
    }

    public static void Map(this Dictionary<string, Debug2Relay> self, Debug2RelayBase next)
    {
      foreach (Debug2Relay node in self.Values)
      {
        next.Route(node);
      }
    }


    public static void Unmap(this Dictionary<string, Debug2Relay> self, Dictionary<string, Debug2Relay> next)
    {
      foreach (string key in next.Keys)
      {
        if (self.ContainsKey(key))
        {
          next[key].Unroute(self[key]);
        }
      }
    }

    public static void Unroute(this Dictionary<string, Debug2Relay> self, Dictionary<string, Debug2Relay> prev)
    {
      foreach (string key in self.Keys)
      {
        if (prev.ContainsKey(key))
        {
          self[key].Unroute(prev[key]);
        }
      }
    }

    public static void Unmap(this Dictionary<string, Debug2Relay> self, Debug2RelayBase next)
    {
      foreach (Debug2Relay node in self.Values)
      {
        next.Unroute(node);
      }
    }


    public static void Open(this Dictionary<string, Debug2Relay> self)
    {
      foreach (Debug2Relay relay in self.Values) { relay.Open(); }
    }
    public static void Open(this Dictionary<string, Debug2Relay> self, string type)
    {
      foreach (Debug2Relay relay in self.Values) { relay.Open(type); }
    }
    public static void Close(this Dictionary<string, Debug2Relay> self)
    {
      foreach (Debug2Relay relay in self.Values) { relay.Close(); }
    }
    public static void Close(this Dictionary<string, Debug2Relay> self, string type)
    {
      foreach (Debug2Relay relay in self.Values) { relay.Close(type); }
    }
    public static void Toggle(this Dictionary<string, Debug2Relay> self)
    {
      foreach (Debug2Relay relay in self.Values) { relay.Toggle(); }
    }
    public static void Toggle(this Dictionary<string, Debug2Relay> self, string type)
    {
      foreach (Debug2Relay relay in self.Values) { relay.Toggle(type); }
    }


    public static IEnumerable<Debug2NodeBase> GetNodes(this Dictionary<string, Debug2Relay> self, string type)
    {
      foreach (Debug2Relay relay in self.Values)
      {
        foreach (Debug2NodeBase node in relay.GetNodes(type))
        {
          yield return node;
        }
      }
    }

    public static IEnumerable<Debug2NodeBase> GetNodes(this Dictionary<string, Debug2Relay> self)
    {
      foreach (Debug2Relay relay in self.Values)
      {
        foreach (Debug2NodeBase node in relay.GetNodes())
        {
          yield return node;
        }
      }
    }
  }

}