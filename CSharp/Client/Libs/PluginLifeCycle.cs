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

  public static class PluginLifeCycle
  {
    static PluginLifeCycle()
    {
      GameMain.LuaCs.Hook.Add("stop", $"[{ModInfo.AssemblyName}] PluginLifeCycle.End", (object[] args) =>
      {
        End?.Invoke();
        return null;
      });
    }
    public static event Action End;
  }
}
