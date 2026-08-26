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
  /// They are equaly fast lol
  /// </summary>
  public class CheckingInterfaceVsBitFlag : Experiment
  {
    public class SomeClass
    {
      public bool HasProp { get; set; }
      public int RealValue = 123;
    }

    public class SomeDefivedClass : SomeClass
    {
      public int Value => RealValue;
    }

    public int repeats = 1000000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      SomeClass o = new SomeDefivedClass();

      int value = 0;

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        if (o.HasProp)
        {
          value = o.RealValue;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"measure 1: [{sw.ElapsedMilliseconds}]");


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        if (o is SomeDefivedClass derived)
        {
          value = derived.Value;
        }
      }
      sw.Stop();
      Mod.Logger.Log($"measure 2: [{sw.ElapsedMilliseconds}]");


    }
  }
}