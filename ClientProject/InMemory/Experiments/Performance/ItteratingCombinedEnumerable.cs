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
  /// itterating combined ienumerables is 10 times slower that itterating two lists
  /// but itterating combined lits is fast
  /// </summary>
  public class ItteratingCombinedEnumerable : Experiment
  {
    public List<int> list1 = Enumerable.Range(0, 10).ToList();
    public List<int> list2 = Enumerable.Range(0, 10).ToList();
    public IEnumerable<int> Combined => list1.Concat(list2);

    public int repeats = 1000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      int value = 0;

      IEnumerable<int> preCombined = list1.Concat(list2);

      List<int> preCombinedlist = new List<int>(preCombined);

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (int v in list1)
        {
          value = v;
        }

        foreach (int v in list2)
        {
          value = v;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"itterating lists: [{sw.ElapsedMilliseconds}]");

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (int v in Combined)
        {
          value = v;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"itterating combined ienumerable: [{sw.ElapsedMilliseconds}]");

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (int v in preCombined)
        {
          value = v;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"itterating precombined ienumerable: [{sw.ElapsedMilliseconds}]");

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (int v in preCombinedlist)
        {
          value = v;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"itterating precombined list: [{sw.ElapsedMilliseconds}]");
    }
  }
}