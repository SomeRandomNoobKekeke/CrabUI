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
  /// i'm trying to figure out how Modules should work, be initialized, used by other Modules and users
  /// This looks like stupid amount of boilerplate code
  /// </summary>
  public class MoreModulesCringe : Experiment
  {


    //---------------- Component ----------------
    public partial class Component
    {
      public Component()
      {
        InitModules();
      }
    }

    // I think modules should be concrete, how can compoment not know what he wants?
    // Component Modules
    public partial class Component : IModuleAContainer, IModuleBContainer
    {
      public ModuleA ModuleA { get; set; }
      public ModuleB ModuleB { get; set; }

      private void InitModules()
      {
        ModuleA = new ModuleA();
        ModuleB = new ModuleB(ModuleA);
      }




      IModuleA IModuleAContainer.ModuleA => ModuleA;
      IModuleB IModuleBContainer.ModuleB => ModuleB;

      public string Cringe => ModuleA.Cringe;
      public string UnCringed => ModuleB.UnCringed;
    }

    // internal module
    //---------------- ModuleA ----------------
    public interface IModuleA
    {
      public string Cringe { get; }
    }
    public class ModuleA : IModuleA
    {
      public string Cringe { get; set; } = "Cringe";
    }


    public interface IModuleAContainer
    {
      // And this is like mandatory forwarding 
      public string Cringe { get; } // => ModuleA.Cringe;

      public IModuleA ModuleA { get; }
    }

    // Another internal module, it uses other module
    //---------------- ModuleB ----------------
    public interface IModuleB
    {
      public string UnCringed { get; }
    }
    public class ModuleB : IModuleB
    {
      IModuleA ModuleA;

      // This is not mandatory
      public string UnCringed => ModuleA.Cringe.Replace("Cringe", "no cringe");

      public ModuleB(IModuleA moduleA) => ModuleA = moduleA;
    }

    public interface IModuleBContainer
    {
      public string UnCringed => ModuleB.UnCringed;
      public IModuleB ModuleB { get; }
    }

    // External module, it uses other component
    //---------------- ModuleC ----------------
    public interface IModuleC
    {
      public string PutTheCringeBack(IModuleBContainer host);
    }
    public class ModuleC : IModuleC
    {
      public string PutTheCringeBack(IModuleBContainer host)
      {
        return host.UnCringed.Replace("no cringe", "even more cringe");
      }
    }

    public interface IModuleCContainer
    {
      public string PutTheCringeBack(IModuleBContainer host) => ModuleC.PutTheCringeBack(host);
      public IModuleC ModuleC { get; }
    }

    //---------------- ComponentB ----------------
    public partial class ComponentB : IModuleCContainer
    {
      public ComponentB()
      {
        InitModules();
      }
    }

    // ComponentB Modules
    public partial class ComponentB
    {
      public ModuleC ModuleC { get; set; }

      private void InitModules()
      {
        ModuleC = new ModuleC();
      }
    }

    // Forwarded for other Modules
    public partial class ComponentB
    {
      IModuleC IModuleCContainer.ModuleC => ModuleC;
    }

    // Forwarded for user
    public partial class ComponentB
    {
      public string PutTheCringeBack(IModuleBContainer host) => ModuleC.PutTheCringeBack(host);
    }



    public override void Run()
    {
      Component component = new Component();
      ComponentB componentB = new ComponentB();

      Mod.Logger.Log(component.Cringe);
      Mod.Logger.Log(component.UnCringed);
      Mod.Logger.Log(componentB.PutTheCringeBack(component));
    }
  }
}