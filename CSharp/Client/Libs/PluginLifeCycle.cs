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
using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaroJunk
{

  //TODO restore, there's some il errors that i'm too lazy to debug rn
  public class PluginLifeCycle
  {
    static PluginLifeCycle()
    {
      GameMain.LuaCs.Hook.Add("stop", $"[{ModInfo.AssemblyName}] PluginLifeCycle.Stop", (object[] args) =>
      {
        Stop?.Invoke();
        foreach (Delegate callback in Stop.GetInvocationList())
        {
          Stop -= (Action)callback;
        }
        return null;
      });
    }

    public static event Action Stop;

  }
}
