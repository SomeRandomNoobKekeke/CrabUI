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
  /// Accessing dict is 45 times slower that accessing prop
  /// accessing field = accessing prop
  /// </summary>
  public class PropAccessSpeedExperiment : Experiment
  {
    public class Bebebe
    {

      public int propA = 123;
      public int propB = 12341;
      public int propC = 43214;

      public int PropA
      {
        get => propA;
        set => propA = value;
      }
      public int PropB
      {
        get => propB;
        set => propB = value;
      }
      public int PropC
      {
        get => propC;
        set => propC = value;
      }


      public int fieldA = 123;
      public int fieldB = 12341;
      public int fieldC = 43214;
    }

    public Dictionary<int, int> Props = new Dictionary<int, int>()
    {
      [0] = 123,
      [1] = 12341,
      [2] = 43214,
    };

    public Bebebe bubu = new();

    public int repeats = 100000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      int value = 0;

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        value = bubu.PropA;
        value = bubu.PropB;
        value = bubu.PropC;
      }
      sw.Stop();
      Mod.Logger.Log($"measure 1: [{sw.ElapsedMilliseconds}]");


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        value = Props[0];
        value = Props[1];
        value = Props[2];
      }
      sw.Stop();
      Mod.Logger.Log($"measure 2: [{sw.ElapsedMilliseconds}]");

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        value = bubu.fieldA;
        value = bubu.fieldB;
        value = bubu.fieldC;
      }
      sw.Stop();
      Mod.Logger.Log($"measure 3: [{sw.ElapsedMilliseconds}]");
    }
  }
}