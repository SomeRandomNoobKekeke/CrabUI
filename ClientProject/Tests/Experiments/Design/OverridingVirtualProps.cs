using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;

using System.Diagnostics;

namespace CUITests
{

  /// <summary>
  /// All prop initializers always run, from derived to base  
  /// All autoimplemented props have backing fields
  /// </summary>
  public class OverridingVirtualProps : Experiment
  {

    public class PropType
    {
      public string Where { get; }

      public PropType(string where)
      {
        Where = where;
        CUITestPlugin.Logger.Log($"Created {where}");
      }
      public override string ToString() => Where;
    }

    public class A
    {
      public virtual PropType Prop => null;
      public virtual PropType Prop2 { get; set; } = new("Prop2 in A");
    }

    public class B : A
    {
      public override PropType Prop { get; } = new("Prop in B");
      public override PropType Prop2 { get; set; } = new("Prop2 in B");
    }

    public class C : B
    {
      public override PropType Prop { get; } = new("Prop in C");
      public override PropType Prop2 { get; set; } = new("Prop2 in C");
    }

    public override void Run()
    {
      CUITestPlugin.Logger.Log("-------- A");
      A a = new();
      CUITestPlugin.Logger.Log("-------- B");
      B b = new();
      CUITestPlugin.Logger.Log("-------- C");
      C c = new();

      CUITestPlugin.Logger.LogVars(a.Prop, b.Prop, c.Prop);
      CUITestPlugin.Logger.LogVars(a.Prop2, b.Prop2, c.Prop2);

      CUITestPlugin.Logger.Log($"fields in A:");
      foreach (FieldInfo fi in typeof(A).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
      {
        CUITestPlugin.Logger.Log($"{fi.DeclaringType}.{fi}");
      }

      CUITestPlugin.Logger.Log($"fields in B:");
      foreach (FieldInfo fi in typeof(B).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
      {
        CUITestPlugin.Logger.Log($"{fi.DeclaringType}.{fi}");
      }

      CUITestPlugin.Logger.Log($"fields in C:");
      foreach (FieldInfo fi in typeof(C).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
      {
        CUITestPlugin.Logger.Log($"{fi.DeclaringType}.{fi}");
      }
    }
  }
}