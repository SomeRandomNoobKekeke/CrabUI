using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugGateDict
  {
    private Dictionary<string, DebugGate> Switches { get; } = new();

    public DebugGate this[string type]
    {
      get => Get(type);
    }

    public DebugGate Get(string type)
    {
      if (!Switches.ContainsKey(type)) Switches[type] = new();
      return Switches[type];
    }
  }
}