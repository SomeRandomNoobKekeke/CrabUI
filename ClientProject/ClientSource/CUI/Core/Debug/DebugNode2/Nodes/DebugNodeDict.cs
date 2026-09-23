using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class Debug2NodeDict : Dictionary<string, Debug2NodeBase>
  {

  }

  public static class Debug2NodeDict_Exetnsions
  {
    public static void Map(this Dictionary<string, Debug2NodeBase> self, Dictionary<string, Debug2Relay> next)
    {
      foreach (string key in self.Keys)
      {
        if (next.ContainsKey(key))
        {
          next[key].Route(self[key]);
        }
        else
        {
          Debug2NodeSetup.Logger.Warning($"Debug2NodeDict can't be fully mapped, next doesn't have [{key}]");
        }
      }
    }

    public static void Map(this Dictionary<string, Debug2NodeBase> self, Debug2Relay next)
    {
      foreach (Debug2NodeBase node in self.Values)
      {
        next.Route(node);
      }
    }


    public static void Unmap(this Dictionary<string, Debug2NodeBase> self, Dictionary<string, Debug2Relay> next)
    {
      foreach (string key in next.Keys)
      {
        if (self.ContainsKey(key))
        {
          next[key].Unroute(self[key]);
        }
      }
    }

    public static void Unmap(this Dictionary<string, Debug2NodeBase> self, Debug2Relay next)
    {
      foreach (Debug2NodeBase node in self.Values)
      {
        next.Unroute(node);
      }
    }
  }

}