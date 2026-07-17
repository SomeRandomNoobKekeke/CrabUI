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
  /// if i want to inject host via prop, then that prop has to be public
  /// </summary>
  public class PrivateModules : Experiment
  {
    public interface IModuleAContainer
    {
      public ModuleA ModuleA { get; set; }
    }

    public class ModuleA
    {
      public string Message { get; set; } = "bruh";
    }

    public class ModuleB
    {
      /// <summary>
      /// Host has to be public or i won't be able to inject it
      /// </summary>
      public IModuleAContainer Host { get; set; }

      public string Transform()
      {
        return new string(Host.ModuleA.Message.Reverse().ToArray());
      }
    }

    public class Host : IModuleAContainer
    {
      public string Transform => ModuleB.Transform();

      /// <summary>
      /// Modules have to be public to implement IModuleAContainer
      /// </summary>
      public ModuleA ModuleA { get; set; } = new();
      public ModuleB ModuleB { get; set; } = new();

      private void InjectModules()
      {
        ModuleB.Host = this;
      }

      public Host()
      {
        InjectModules();
      }
    }


    public override void Run()
    {
      Host host = new Host();

      Mod.Logger.Log(host.ModuleB.Transform());
    }
  }
}