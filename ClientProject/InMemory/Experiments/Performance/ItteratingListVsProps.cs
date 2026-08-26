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
  /// itterating list or array is almost 4 times slower that getting props
  /// </summary>
  public class ItteratingListVsProps : Experiment
  {

    public List<int> list = new(){
      1,2,3
    };

    public List<int> array = new(){
      1,2,3
    };

    public int Prop1 { get; set; } = 1;
    public int Prop2 { get; set; } = 1;
    public int Prop3 { get; set; } = 1;


    public int repeats = 1000000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      int value = 0;

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        value = Prop1;
        value = Prop2;
        value = Prop3;
      }
      sw.Stop();
      Mod.Logger.Log($"measure 1: [{sw.ElapsedMilliseconds}]");


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        value = list[0];
        value = list[1];
        value = list[2];
      }
      sw.Stop();
      Mod.Logger.Log($"measure 2: [{sw.ElapsedMilliseconds}]");

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        value = array[0];
        value = array[1];
        value = array[2];
      }
      sw.Stop();
      Mod.Logger.Log($"measure 3: [{sw.ElapsedMilliseconds}]");

    }
  }
}