using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  public partial class ModuleInjection : Experiment
  {
    public class ModuleDependencyAttribute : System.Attribute { }
    public class ModuleAccessorAttribute : System.Attribute { }

    public interface IModule
    {

    }

    public class PropA : IModule
    {
      public int Value { get; set; }
    }

    public class PropB : IModule
    {
      [ModuleDependency]
      public PropA PropA { get; set; }

      public string Value => $"the value is [{PropA.Value}]";
    }

    public class TreeProp : IModule
    {
      [ModuleDependency]
      public ComponentTreeModule TreeModule { get; set; }

      [ModuleAccessorAttribute]
      public Func<Component, TreeProp> GetTreeProp { get; set; }

      private int _value;
      public int Value
      {
        get => _value;
        set
        {
          _value = value;
          foreach (Component child in TreeModule.Children)
          {
            GetTreeProp(child).Value = value;
          }
        }
      }
    }

    public class ComponentTreeModule : IModule
    {
      [ModuleDependency]
      public Component Host { get; set; }
      public Component Parent { get; set; }
      public List<Component> Children { get; } = new();
    }
  }
}