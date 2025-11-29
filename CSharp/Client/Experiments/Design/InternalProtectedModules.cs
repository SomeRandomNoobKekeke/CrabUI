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
  /// Ok, so not only internal modules have access to private props
  /// But also internal modules in derived classes have access to protected props
  /// </summary>
  public class InternalProtectedModules : Experiment
  {
    public class HostInternals
    {
      public int InternalProp { get; set; } = 123;
    }

    public class Host
    {
      protected HostInternals Internals { get; set; } = new();
    }

    public class HostB : Host
    {
      public class ModuleAClass
      {
        public Host Host { get; set; }
        protected HostInternals Internals => Host.Internals;

        public int GetInternals() => Internals.InternalProp;
      }

      public ModuleAClass ModuleA { get; set; } = new();

      private void InjectModules()
      {
        ModuleA.Host = this;
      }

      public HostB() : base()
      {
        InjectModules();
      }
    }


    public override void Run()
    {
      HostB host = new HostB();
      Mod.Logger.Log(host.ModuleA.GetInternals());

    }
  }
}