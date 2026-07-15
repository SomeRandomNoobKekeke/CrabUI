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
  /// Using object via interface doesn't affect performance at all
  /// o is IModuleBHost takes some ticks, but it's still balls
  /// </summary>
  public class InterfaceVsDirectProp : Experiment
  {
    public interface IModuleBHost
    {
      public ModuleB ModuleB { get; set; }
    }


    public class Host : IModuleBHost
    {
      public ModuleB ModuleB { get; set; } = new ModuleB();
      public ModuleB DirectModuleB { get; set; } = new ModuleB();
    }

    public class ModuleA
    {
      public int UseObject(object o)
      {
        if (o is IModuleBHost host)
        {
          return host.ModuleB.Value;
        }
        else
        {
          return 0;
        }
      }
      public int UseB(IModuleBHost host)
      {
        return host.ModuleB.Value;
      }

      public int UseHost(Host host)
      {
        return host.DirectModuleB.Value;
      }
    }

    public class ModuleB
    {
      public int Value { get; set; } = 123;
    }

    public int repeats = 1000000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();
      int value = 0;

      Host host = new Host();
      ModuleA moduleA = new ModuleA();

      sw.Start();
      for (int i = 0; i < repeats; i++)
      {
        value = moduleA.UseHost(host);
      }
      sw.Stop();
      Mod.Logger.Log($"UseHost: [{sw.ElapsedMilliseconds}]");


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        value = moduleA.UseB(host);
      }
      sw.Stop();
      Mod.Logger.Log($"UseB: [{sw.ElapsedMilliseconds}]");


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        value = moduleA.UseObject(host);
      }
      sw.Stop();
      Mod.Logger.Log($"UseObject: [{sw.ElapsedMilliseconds}]");


    }
  }
}