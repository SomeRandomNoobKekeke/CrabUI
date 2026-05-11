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
  public class DebugNodeDict : Dictionary<string, IDebugNode> { }

  public static class DebugNodeDict_Extensions
  {
    public static void Map(this Dictionary<string, IDebugNode> self, Dictionary<string, IDebugNode> next)
    {
      foreach (string key in self.Keys)
      {
        if (next.ContainsKey(key)) self[key].Map(next[key]);
      }
    }

    public static void Unmap(this Dictionary<string, IDebugNode> self, Dictionary<string, IDebugNode> next)
    {
      foreach (string key in self.Keys)
      {
        if (next.ContainsKey(key)) self[key].Unmap(next[key]);
      }
    }

    public static void Route(this Dictionary<string, IDebugNode> self, Dictionary<string, IDebugNode> prev) => prev.Map(self);
    public static void Unroute(this Dictionary<string, IDebugNode> self, Dictionary<string, IDebugNode> prev) => prev.Unmap(self);


    public static NodeT Get<NodeT>(this Dictionary<string, IDebugNode> self, string key) where NodeT : IDebugNode
      => (NodeT)self[key];
  }
}