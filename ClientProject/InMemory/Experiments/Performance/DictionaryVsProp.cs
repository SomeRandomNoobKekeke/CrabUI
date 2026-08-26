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
  /// Accessing dict by int key is 16 times slower than prop
  /// Accessing dict by str key is at least 48 times slower than prop for 1 char string
  /// And even slower for longer strings
  /// </summary>
  public class DictionaryVsProp : Experiment
  {
    public static string key = "stupidly long string for a dict key";

    public Dictionary<string, int> Values = new()
    {
      [key] = 123
    };

    public Dictionary<int, int> IntValues = new()
    {
      [123] = 123
    };
    public int Value { get; set; } = 321;

    public int repeats = 100000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();
      int value = 0;



      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        value = Values[key];
      }
      sw.Stop();
      Mod.Logger.Log($"access string dict: [{sw.ElapsedMilliseconds}]");

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        value = IntValues[123];
      }
      sw.Stop();
      Mod.Logger.Log($"access int dict: [{sw.ElapsedMilliseconds}]");


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        value = Value;
      }
      sw.Stop();
      Mod.Logger.Log($"access prop: [{sw.ElapsedMilliseconds}]");


    }
  }
}