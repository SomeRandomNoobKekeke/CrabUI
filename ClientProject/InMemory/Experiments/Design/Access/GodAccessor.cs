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
  /// Everything is injected
  /// Modules depend on abstractions
  /// Internal stuff isn't exposed
  /// Amazing, now with a single god access object
  /// </summary>
  public class GodAccessorExperiment : Experiment
  {
    public class Component
    {
      private class ModuleAccess : FullNameModule.INameModule, FullNameModule.ISurnameModule
      {
        private Component Host { get; }

        string FullNameModule.INameModule.Name
        {
          get => Host.Modules.NameModule.Name;
          set => Host.Modules.NameModule.Name = value;
        }

        string FullNameModule.ISurnameModule.Surname
        {
          get => Host.Modules.SurnameModule.Surname;
          set => Host.Modules.SurnameModule.Surname = value;
        }
        public ModuleAccess(Component host) => Host = host;
      }

      public class ModuleWrapper
      {
        public FullNameModule FullNameModule { get; set; } = new();
        public NameModule NameModule { get; set; } = new();
        public SurnameModule SurnameModule { get; set; } = new();
      }

      public string FullName => Modules.FullNameModule.FullName;

      public string Name
      {
        get => Modules.NameModule.Name;
        set => Modules.NameModule.Name = value;
      }

      public string Surname
      {
        get => Modules.SurnameModule.Surname;
        set => Modules.SurnameModule.Surname = value;
      }

      private ModuleWrapper Modules { get; } = new();
      private ModuleAccess Access { get; set; }

      private void InjectModules()
      {
        Access = new ModuleAccess(this);
        Modules.FullNameModule.NameModule = Access;
        Modules.FullNameModule.SurnameModule = Access;
      }

      public Component()
      {
        InjectModules();
      }
    }

    public class FullNameModule
    {
      public interface INameModule
      {
        public string Name { get; set; }
      }

      public interface ISurnameModule
      {
        public string Surname { get; set; }
      }

      public INameModule NameModule { get; set; }
      public ISurnameModule SurnameModule { get; set; }

      public string FullName => $"{NameModule.Name} {SurnameModule.Surname}";
    }

    public class NameModule
    {
      public string Name { get; set; }
    }

    public class SurnameModule
    {
      public string Surname { get; set; }
    }


    public override void Run()
    {
      Component component = new();
      component.Name = "kek";
      component.Surname = "lolov";

      Mod.Logger.LogVars(component.FullName);
    }
  }
}