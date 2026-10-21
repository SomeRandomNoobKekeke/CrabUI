using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
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

    //https://stackoverflow.com/a/29491927
    public static Action<T> Debounce<T>(this Action<T> func, int milliseconds = 100)
    {
      CancellationTokenSource? cancelTokenSource = null;

      return arg =>
      {
        cancelTokenSource?.Cancel();
        cancelTokenSource = new CancellationTokenSource();

        Task.Delay(milliseconds, cancelTokenSource.Token)
          .ContinueWith(t =>
          {
            if (t.IsCompletedSuccessfully)
            {
              func(arg);
            }
          }, TaskScheduler.Default);
      };
    }

    public static Action Debounce(this Action func, int milliseconds = 100)
    {
      CancellationTokenSource? cancelTokenSource = null;

      return () =>
      {
        cancelTokenSource?.Cancel();
        cancelTokenSource = new CancellationTokenSource();

        Task.Delay(milliseconds, cancelTokenSource.Token)
          .ContinueWith(t =>
          {
            if (t.IsCompletedSuccessfully)
            {
              func();
            }
          }, TaskScheduler.Default);
      };
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