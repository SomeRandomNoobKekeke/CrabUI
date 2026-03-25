using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public class DebugChannelsDict : Dictionary<string, DebugNode>
  {
    public void Map(DebugChannelsDict next)
    {
      foreach (string key in Keys)
      {
        if (next.ContainsKey(key)) this[key].Map(next[key]);
      }
    }

    public void Unmap(DebugChannelsDict next)
    {
      foreach (string key in Keys)
      {
        if (next.ContainsKey(key)) this[key].Unmap(next[key]);
      }
    }

    public void Route(DebugChannelsDict prev) => prev.Map(this);
    public void Unroute(DebugChannelsDict prev) => prev.Unmap(this);
  }
}