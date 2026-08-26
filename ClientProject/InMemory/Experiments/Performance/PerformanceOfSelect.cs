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
  /// Select is 12 times slower
  /// </summary>
  public class PerformanceOfSelect : Experiment
  {
    public class Wrapper
    {
      public int Value { get; set; }

      public Wrapper(int i) => Value = i;
    }



    public int repeats = 30000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      List<Wrapper> list = new List<Wrapper>(Enumerable.Range(0, 1000).Select(i => new Wrapper(i)));

      int value = 0;

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        foreach (Wrapper wrapper in list)
        {
          value = wrapper.Value;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"prop access: [{sw.ElapsedMilliseconds}]");


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (int v in list.Select(wrapper => wrapper.Value))
        {
          value = v;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"select: [{sw.ElapsedMilliseconds}]");


    }
  }
}