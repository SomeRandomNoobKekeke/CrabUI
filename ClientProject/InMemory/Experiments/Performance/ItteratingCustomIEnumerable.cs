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
  /// Custom IEnumerable is 10 times slower
  /// Custom IEnumerable without a nested loop is just as slow
  /// </summary>
  public class ItteratingCustomIEnumerable : Experiment
  {
    public class SomeClass
    {
      public List<int> Values = new(){
        1,2,3,4,5
      };

      public IEnumerable<int> GetValues()
      {
        for (int i = 0; i < Values.Count; i++)
        {
          yield return Values[i];
        }
        yield break;
      }

      public IEnumerable<int> GetFunnyValues()
      {
        yield return Values[0];
        yield return Values[1];
        yield return Values[2];
        yield return Values[3];
        yield return Values[4];
        yield break;
      }
    }

    public int repeats = 100000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      SomeClass o = new();

      int value = 0;

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        foreach (int v in o.Values)
        {
          value = v;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"measure 1: [{sw.ElapsedMilliseconds}]");


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (int v in o.GetFunnyValues())
        {
          value = v;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"measure 3: [{sw.ElapsedMilliseconds}]");

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (int v in o.GetValues())
        {
          value = v;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"measure 2: [{sw.ElapsedMilliseconds}]");



    }
  }
}