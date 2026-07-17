using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;
using CrabUI;
using Barotrauma;

namespace CrabUIUser
{

  /// <summary>
  /// it's 0, delegate is cached
  /// </summary>
  public class LambdaMemoryUsage : Experiment
  {
    public class Prop
    {
      public InfoChannel<string> Log = new();
    }


    public override void Run()
    {
      GC.Collect();
      int count = 10000000;
      float mem1 = LuaCsPerformanceCounter.MemoryUsage;


      List<Prop> props = Enumerable.Range(0, count).Select(i => new Prop()).ToList();

      foreach (Prop prop in props)
      {
        prop.Log.OnSend = (msg) => Mod.Logger.Log(msg);
      }
      float mem2 = LuaCsPerformanceCounter.MemoryUsage;
      Mod.Logger.Log($"count: {count} mem: {mem2 - mem1}MB unit: {Math.Round((mem2 - mem1) * 1024 * 1024 / count)}B");

      props = null;
      GC.Collect();

    }

  }
}