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
  /// I want to hide some internal props from users but give some specified modules access to them
  /// Internals should be protected in both host and module, so they won't be able to pass them to each other
  /// But this setup might work, i think it's actually the only solution
  /// ModuleAClass has access to Host.Internals because it's a nested class
  /// </summary>
  public class InternalModules : Experiment
  {
    public class Host
    {
      public class ModuleAClass
      {
        public Host Host { get; set; }
        protected HostInternals Internals => Host.Internals;

        public int GetInternals() => Internals.InternalProp;
      }

      public class HostInternals
      {
        public int InternalProp { get; set; } = 123;
      }

      protected HostInternals Internals { get; set; } = new();

      public ModuleAClass ModuleA { get; set; } = new();

      private void InjectModules()
      {
        ModuleA.Host = this;
        // this won't work because ModuleA.Internals is also protected
        // ModuleA.Internals = Internals;
      }

      public Host()
      {
        InjectModules();
      }
    }


    public override void Run()
    {
      Host host = new Host();
      Mod.Logger.Log(host.ModuleA.GetInternals());

    }
  }
}