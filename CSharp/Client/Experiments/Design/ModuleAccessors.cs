using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CrabUIUser
{


  public class ModuleAccessors : Experiment
  {
    public class Component
    {
      static Component()
      {
        ModuleA_Accessor.Inject();
      }
      private static ModuleA_Accessor ModuleA_accessor;
      public class ModuleA_Accessor
      {
        public static void Inject() => ModuleA_accessor = new();
        public ModuleA Get(Component c) => c.ModuleA;
        private ModuleA_Accessor() { }
      }

      private ModuleA ModuleA { get; } = new();

      private ComponentTreeModule ComponentTreeModule { get; } = new();

      public void InjectModules()
      {
        ModuleA.ComponentTreeModule = ComponentTreeModule;
        ModuleA.ModuleA_Accessor = ModuleA_accessor;
        ComponentTreeModule.Host = this;
      }

      public string Prop
      {
        get => ModuleA.Prop;
        set => ModuleA.SetPropRecursive(value);
      }
      public void AddChild(Component child) => ComponentTreeModule.AddChild(child);

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
      public Component.ModuleA_Accessor ModuleA_Accessor { get; set; }

      public string Prop { get; set; } = "bruh";

      public void SetPropRecursive(string value)
      {
        Prop = value;
        foreach (Component component in ComponentTreeModule.Children)
        {
          ModuleA_Accessor.Get(component).SetPropRecursive(value);
        }
      }
    }

    public class ComponentTreeModule
    {
      public Component Host { get; set; }
      public Component Parent { get; set; }
      public List<Component> Children { get; } = new();
      public void AddChild(Component child)
      {
        child.Parent = Host;
        Children.Add(child);
      }
    }





    public override void Run()
    {
      Component component1 = new();
      Component component2 = new();
      Component component3 = new();

      component1.AddChild(component2);
      component2.AddChild(component3);

      component1.Prop = "123";

      Mod.Logger.Log(component1.Prop);
      Mod.Logger.Log(component2.Prop);
      Mod.Logger.Log(component3.Prop);
    }
  }
}