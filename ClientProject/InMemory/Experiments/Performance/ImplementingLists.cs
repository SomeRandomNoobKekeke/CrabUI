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
  /// ConvertAll is 30 times slower than direct access
  /// list.Cast<Prop1>().ToList() is 60 times slower
  /// </summary>
  public class ImplementingLists : Experiment
  {

    public class Prop1 { }
    public class Prop2 : Prop1 { }

    public interface IA
    {
      public List<Prop1> list { get; }
    }


    public class A : IA
    {

      public List<Prop2> list { get; } = Enumerable.Range(0, 10).Select((i) => new Prop2()).ToList();

      List<Prop1> IA.list => list.ConvertAll((prop) => (Prop1)prop);
      // List<Prop1> IA.list => list.Cast<Prop1>().ToList();
    }

    public int repeats = 3000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      A a = new A();
      Prop1 value = null;

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        foreach (Prop2 prop in a.list)
        {
          value = prop;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"direct: [{sw.ElapsedMilliseconds}]");


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (Prop1 prop in (a as IA).list)
        {
          value = prop;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"with ConvertAll: [{sw.ElapsedMilliseconds}]");
    }
  }
}