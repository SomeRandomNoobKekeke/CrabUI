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
  /// Difference is negligible
  /// </summary>
  public class ItteratingOverProps : Experiment
  {

    public class A
    {

    }
    public class B : A
    {

    }
    public class C : A
    {

    }

    public class D : C
    {

    }

    public class Settings
    {
      public A A1 { get; set; } = new();
      public A A2 { get; set; } = new();
      public A A3 { get; set; } = new();
      public B B1 { get; set; } = new();
      public B B2 { get; set; } = new();
      public B B3 { get; set; } = new();
      public C C1 { get; set; } = new();
      public C C2 { get; set; } = new();
      public C C3 { get; set; } = new();
      public D D1 { get; set; } = new();
      public D D2 { get; set; } = new();
      public D D3 { get; set; } = new();
    }

    public int repeats = 1000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      object slot1 = null;
      object slot2 = null;

      Settings settings = new();

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (PropertyInfo pi in typeof(Settings).GetProperties())
        {
          if (pi.PropertyType.IsAssignableTo(typeof(A)))
          {
            slot1 = pi.GetValue(settings);
          }

          if (pi.PropertyType.IsAssignableTo(typeof(C)))
          {
            slot2 = pi.GetValue(settings);
          }
        }
      }
      sw.Stop();
      Mod.Logger.Log($"together: [{sw.ElapsedMilliseconds}]");

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        foreach (PropertyInfo pi in typeof(Settings).GetProperties())
        {
          if (pi.PropertyType.IsAssignableTo(typeof(A)))
          {
            slot1 = pi.GetValue(settings);
          }
        }

        foreach (PropertyInfo pi in typeof(Settings).GetProperties())
        {
          if (pi.PropertyType.IsAssignableTo(typeof(C)))
          {
            slot2 = pi.GetValue(settings);
          }
        }
      }
      sw.Stop();
      Mod.Logger.Log($"separate: [{sw.ElapsedMilliseconds}]");
    }
  }
}