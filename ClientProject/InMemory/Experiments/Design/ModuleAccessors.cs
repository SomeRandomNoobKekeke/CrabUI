using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CursedUIUser
{


  public class ModuleAccessors : Experiment

  {


    public class Component
    {
      public interface IModuleA_Access { public ModuleA Get(Component host); }
      private class ModuleA_Access : IModuleA_Access
      {
        public ModuleA Get(Component host) => host.ModuleA;
      }

      public interface IComponentTreeModule_Access { public ComponentTreeModule Get(Component host); }
      private class ComponentTreeModule_Access : IComponentTreeModule_Access
      {
        public ComponentTreeModule Get(Component host) => host.ComponentTreeModule;
      }

      private ModuleA ModuleA { get; } = new();

      private ComponentTreeModule ComponentTreeModule { get; } = new();

      public void InjectModules()
      {
        ComponentTreeModule.Accessor = new ComponentTreeModule_Access();
        ModuleA.ExternalModuleA = new ModuleA_Access();
        ModuleA.ComponentTreeModule = ComponentTreeModule;
        ComponentTreeModule.Host = this;
      }

      public string Prop
      {
        get => ModuleA.Prop;
        set => ModuleA.SetPropRecursive(value);
      }
      public void Append(Component child) => ComponentTreeModule.Append(child);

      public Component Parent
      {
        get => ComponentTreeModule.Parent;
        set => ComponentTreeModule.Parent = value;
      }

      public Component()
      {
        InjectModules();
      }

    }

    public class ModuleA
    {
      public ComponentTreeModule ComponentTreeModule { get; set; }
      public Component.IModuleA_Access ExternalModuleA { get; set; }

      public string Prop { get; set; } = "bruh";

      public void SetPropRecursive(string value)
      {
        Prop = value;
        foreach (Component child in ComponentTreeModule.Children)
        {
          ExternalModuleA.Get(child).SetPropRecursive(value);
        }
      }
    }

    public class ComponentTreeModule
    {
      public Component.IComponentTreeModule_Access Accessor { get; set; }
      public Component Host { get; set; }
      public Component Parent { get; set; }
      public List<Component> Children { get; } = new();
      public void Append(Component child)
      {
        Accessor.Get(child).Parent = Host;
        Children.Add(child);
      }
    }





    public override void Run()
    {
      Component component1 = new();
      Component component2 = new();
      Component component3 = new();

      component1.Append(component2);
      component2.Append(component3);

      component1.Prop = "123";

      Mod.Logger.Log(component1.Prop);
      Mod.Logger.Log(component2.Prop);
      Mod.Logger.Log(component3.Prop);
    }
  }
}