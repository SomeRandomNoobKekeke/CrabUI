using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;

using System.Diagnostics;

namespace CrabUIUser
{
  /// <summary>
  /// foreach is a bit slower, but not significantly
  /// </summary>
  public class ForVsForeach : Experiment
  {

    public List<int> list = Enumerable.Range(0, 10).ToList();

    public int repeats = 100000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      int value = 0;

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        for (int j = 0; j < list.Count; j++)
        {
          value = list[j];
        }
      }
      sw.Stop();
      Mod.Logger.Log($"measure 1: [{sw.ElapsedMilliseconds}]");


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (int j in list)
        {
          value = j;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"measure 2: [{sw.ElapsedMilliseconds}]");
    }
  }
}