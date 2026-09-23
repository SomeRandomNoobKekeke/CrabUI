using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class Debug2GateDict
  {
    private Dictionary<string, Debug2Gate> Switches { get; } = new();

    public Debug2Gate this[string type]
    {
      get => Get(type);
    }

    public Debug2Gate Get(string type)
    {
      if (!Switches.ContainsKey(type)) Switches[type] = new();
      return Switches[type];
    }

    public IEnumerable<string> Names => Switches.Keys;
  }
}