using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CUILibs;
using HarmonyLib;
using System.Threading.Tasks;

namespace CursedUIUser
{
  public static class Utils
  {
    public static void RunWithDelay(Action action, int delay = 100)
    {
      Task.Delay(delay).ContinueWith((t) => action());
    }

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