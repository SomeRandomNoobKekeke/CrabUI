using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;

using System.Diagnostics;

namespace CursedUIUser
{

  /// <summary>
  /// delegate is 8 times slower
  /// </summary>
  public class DelegateVsDirectAccess : Experiment
  {
    public class SomeClass
    {
      public int Value { get; set; } = 123;
    }

    public int repeats = 100000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      SomeClass o = new();
      Func<int> getter = () => o.Value;

      int value = 0;

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        value = o.Value;
      }
      sw.Stop();
      Mod.Logger.Log($"measure 1: [{sw.ElapsedMilliseconds}]");

      value = getter();

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        value = getter();
      }
      sw.Stop();
      Mod.Logger.Log($"measure 2: [{sw.ElapsedMilliseconds}]");
    }
  }
}