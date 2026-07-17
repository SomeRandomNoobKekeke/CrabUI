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
  /// Doesn't matter
  /// </summary>
  public class DeepPropVsDirectProp : Experiment
  {
    public class SomeClass
    {
      public SomeClass1 A { get; set; } = new();
      public int Value { get; set; } = 123;
    }
    public class SomeClass1
    {
      public SomeClass2 A { get; set; } = new();
    }
    public class SomeClass2
    {
      public SomeClass3 A { get; set; } = new();
    }
    public class SomeClass3
    {
      public SomeClass4 A { get; set; } = new();
    }
    public class SomeClass4
    {
      public SomeClass5 A { get; set; } = new();
    }
    public class SomeClass5
    {
      public int Value { get; set; } = 123;
    }



    public int repeats = 1000000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      SomeClass o = new SomeClass();

      int value = 0;

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        value = o.Value;
      }
      sw.Stop();
      Mod.Logger.Log($"measure 1: [{sw.ElapsedMilliseconds}]");


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        value = o.A.A.A.A.A.Value;
      }
      sw.Stop();
      Mod.Logger.Log($"measure 2: [{sw.ElapsedMilliseconds}]");


    }
  }
}