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
  /// Prop constructor runs 3 times, only 1 backing field
  /// </summary>
  public class OverridingVirtualProps : Experiment
  {

    public class PropType
    {
      public PropType(string where)
      {
        Mod.Logger.Log($"VirtualProp Created in {where}");
      }
    }

    public class A
    {
      public virtual PropType VirtualProp { get; set; } = new("A");
    }

    public class B : A
    {
      public override PropType VirtualProp { get; set; } = new("B");
    }

    public class C : B
    {
      public override PropType VirtualProp { get; set; } = new("C");
    }

    public override void Run()
    {
      C c = new();

      foreach (FieldInfo fi in typeof(C).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
      {
        Mod.Logger.Log(fi);
      }
    }
  }
}