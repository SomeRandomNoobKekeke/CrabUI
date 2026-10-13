using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CUILibs;

using HarmonyLib;

namespace CursedUI
{
  public static partial class Utils
  {
    public static string Lorem => "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aut quam facilis doloribus in optio qui, explicabo sunt ratione excepturi totam dolorum natus delectus provident minus consequatur corrupti odio veniam praesentium?";

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