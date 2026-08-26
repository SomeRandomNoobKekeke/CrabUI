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
  /// Idea is this, i want to store all data and functionality in modules
  /// But accessing modules for any prop is inconvenient + if i decide to move prop from module to 
  /// component then all code using that prop will break
  /// It won't break if i use forwarded prop and it's also easier to use
  /// But it's feels so stupid
  /// What's the point of modules if i expose every prop and method?
  /// </summary>
  public class PropForwarding : Experiment
  {
    public interface IModuleBHost
    {
      public ModuleB ModuleB { get; set; }
    }

    // public interface IValueContained
    // {
    //   public int Value { get; set; }
    // }

    public class ModuleB //: IValueContained
    {
      public int Value { get; set; } = 123;
    }

    public class Host : IModuleBHost //, IValueContained
    {
      public ModuleB ModuleB { get; set; } = new ModuleB();

      public int Value
      {
        get => ModuleB.Value;
        set => ModuleB.Value = value;
      }
    }


    public class HostAfterRefactor //: IValueContained
    {
      public int Value { get; set; } = 123;
    }



    public int repeats = 1000000000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();
      int value = 0;

      Host host = new();
      HostAfterRefactor hostAfter = new();

      value = host.Value;
      value = hostAfter.Value;
    }
  }
}