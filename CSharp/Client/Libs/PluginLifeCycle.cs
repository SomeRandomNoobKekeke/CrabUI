using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using System.IO;
using System.Text;

namespace BaroJunk
{

  public class PluginLifeCycle
  {
    private static PluginLifeCycle Instance;

    static PluginLifeCycle()
    {
      Instance = new PluginLifeCycle();

      GameMain.LuaCs.Hook.Add("stop", $"[{ModInfo.AssemblyName}] PluginLifeCycle.End", (object[] args) =>
      {
        Instance?.end?.Invoke();
        Instance = null;
        return null;
      });
    }
    private event Action end;

    public static event Action End
    {
      add { if (Instance is not null) Instance.end += value; }
      remove { if (Instance is not null) Instance.end -= value; }
    }
  }
}
