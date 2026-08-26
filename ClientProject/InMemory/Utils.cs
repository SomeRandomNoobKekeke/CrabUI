using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using Barotrauma.LuaCs;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;
using HarmonyLib;

namespace CursedUIUser
{
  public static class Utils
  {
    public static void PrintAllHarmonyPatches()
    {
      foreach (MethodBase mb in Harmony.GetAllPatchedMethods())
      {
        Patches patches = Harmony.GetPatchInfo(mb);

        if (patches.Prefixes.Count() > 0 || patches.Postfixes.Count() > 0)
        {
          Logger.Default.Log($"{mb.DeclaringType}.{mb.Name}:");
          if (patches.Prefixes.Count() > 0)
          {
            Logger.Default.Log($"    Prefixes:");
            foreach (Patch patch in patches.Prefixes) { Logger.Default.Log($"        {patch.owner}"); }
          }

          if (patches.Postfixes.Count() > 0)
          {
            Logger.Default.Log($"    Postfixes:");
            foreach (Patch patch in patches.Postfixes) { Logger.Default.Log($"        {patch.owner}"); }
          }
        }
      }
    }
  }

}