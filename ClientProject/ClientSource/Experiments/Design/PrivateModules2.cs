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
  /// If i inject host in constructor then Host prop won't have to be public
  /// </summary>
  public class PrivateModules2 : Experiment
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
      protected IModuleAContainer Host { get; set; }
      public string Transform()
      {
        return new string(Host.ModuleA.Message.Reverse().ToArray());
      }

      public ModuleB(Host host) => Host = host;
    }

    public class Host : IModuleAContainer
    {
      public string Transform => ModuleB.Transform();

      /// <summary>
      /// Modules have to be public to implement IModuleAContainer
      /// </summary>
      public ModuleA ModuleA { get; set; }
      public ModuleB ModuleB { get; set; }

      private void InjectModules()
      {
        ModuleA = new ModuleA();
        ModuleB = new ModuleB(this);
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