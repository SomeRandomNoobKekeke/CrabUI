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
  /// Trying to solve that relations problem
  /// </summary>
  public class ComponentCoresRelations : Experiment
  {
    public class Component
    {
      public class ComponentCore
      {
        // This has to be public
        public Component Component { get; }

        // this also has to be pubilc, event tho it shouldn't
        public List<ComponentCore> Children = new();
        public void AddChild(ComponentCore child) => Children.Add(child);

        public ComponentCore(Component component) => Component = component; // (component)
      }

      public IEnumerable<Component> Children => Core.Children.Select(core => core.Component);
      public void AddChild(Component child) => Core.AddChild(child.Core);

      protected virtual ComponentCore Core { get; }

      public Component()
      {
        Core = new ComponentCore(this);
      }
    }


    public override void Run()
    {
      Component component1 = new();
      Component component2 = new();

      component1.AddChild(component2);

      foreach (Component child in component1.Children)
      {
        Mod.Logger.Log(child);
      }


    }
  }
}