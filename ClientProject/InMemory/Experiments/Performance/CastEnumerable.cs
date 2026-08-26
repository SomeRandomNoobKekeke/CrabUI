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
  /// Itterating IA.list is 20 times slower than List<Prop2> list
  /// Itterating list.Cast<Prop1>() is exactly the same as IA.list
  /// </summary>
  public class CastEnumerable : Experiment
  {

    public class Prop1 { }
    public class Prop2 : Prop1 { }

    public interface IA
    {
      public IEnumerable<Prop1> list { get; }
    }


    public class A : IA
    {

      public List<Prop2> list { get; } = Enumerable.Range(0, 10).Select((i) => new Prop2()).ToList();

      public IEnumerable<Prop1> listOfProp1 => list.Cast<Prop1>();

      IEnumerable<Prop1> IA.list => list;
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
      Mod.Logger.Log($"itterating list: [{sw.ElapsedMilliseconds}]");


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (Prop1 prop in (a as IA).list)
        {
          value = prop;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"interface IEnumerable: [{sw.ElapsedMilliseconds}]");

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (Prop1 prop in a.listOfProp1)
        {
          value = prop;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"class IEnumerable: [{sw.ElapsedMilliseconds}]");
    }
  }
}