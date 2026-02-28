using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using BaroJunk;
using CrabUI;

namespace CUITest
{
  /// <summary>
  /// Logic is this:
  /// IModule and IModuleContainer are just wrappers to organize props and they should be ignored
  /// But everything else should be tracked
  /// </summary>
  public class PublicPropDiscoveryTest : CodeAnalisysTest
  {
    public class ModuleA : IModule
    {
      public string PropOnModuleA { get; set; }
      public ModuleC ModuleCPropOnModuleA { get; set; }
      public ComponentB ComponentPropOnModuleA { get; set; }
    }

    public class ModuleB : IModule
    {
      public string PropOnModuleB { get; set; }
      public ModuleD ModuleDPropOnModuleB { get; set; }
    }

    public class ModuleC : IModule { }

    public class ModuleD : IModule { }

    public class ComponentA : IComponent
    {
      public class Wrapper1 : IModuleContainer
      {
        public ModuleB ModuleBInWrapper1 { get; set; }
        public Wrapper2 Modules2 { get; set; }

        public string PropInWrapper1 { get; set; }

      }

      public class Wrapper2 : IModuleContainer
      {
        public ModuleA ModuleAInWrapper2 { get; set; }

        public string PropInWrapper2 { get; set; }
      }

      public Wrapper1 Modules1 { get; set; }

      public ModuleA ModuleAOnComponent { get; set; }

      public string PropOnComponent { get; set; }

      public ComponentA Parent { get; set; }
      public ComponentB Neighbour { get; set; }
    }

    public class ComponentB : IComponent
    {

    }


    public override void CreateTests()
    {
      CodeAnalizer codeAnalizer = new CodeAnalizer();
      CAComponentModel model = codeAnalizer.AnalyzeComponent(typeof(ComponentA));

      Logger.Default.Log(model);
      Tests.Add(new USetTest(
        model.PublicGet.SelectMany(kvp => kvp.Value.Keys),
        new HashSet<string> {
          "PropOnComponent",
          "Parent",
          "Neighbour",
          "PropInWrapper1",
          "PropInWrapper2",
          "PropOnModuleA",
          "ComponentPropOnModuleA",
          "PropOnModuleB",
        }
      ));
    }

  }
}