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
  /// Amazing, but do i really need to create Access object for each module dependency?
  /// </summary>
  public class MultipleAccessorsExperiment : Experiment
  {
    public class Component
    {
      private class AccessClasses
      {
        public class FullNameModule_NameModule_Access : FullNameModule.INameModule
        {
          private Component Host { get; }
          public string Name
          {
            get => Host.Modules.NameModule.Name;
            set => Host.Modules.NameModule.Name = value;
          }
          public FullNameModule_NameModule_Access(Component host) => Host = host;
        }

        public class FullNameModule_SurnameModule_Access : FullNameModule.ISurnameModule
        {
          private Component Host { get; }
          public string Surname
          {
            get => Host.Modules.SurnameModule.Surname;
            set => Host.Modules.SurnameModule.Surname = value;
          }
          public FullNameModule_SurnameModule_Access(Component host) => Host = host;
        }
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

      private void InjectModules()
      {
        Modules.FullNameModule.NameModule = new AccessClasses.FullNameModule_NameModule_Access(this);
        Modules.FullNameModule.SurnameModule = new AccessClasses.FullNameModule_SurnameModule_Access(this);
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
      component.Name = "lol";
      component.Surname = "kekov";

      Mod.Logger.LogVars(component.FullName);
    }
  }
}