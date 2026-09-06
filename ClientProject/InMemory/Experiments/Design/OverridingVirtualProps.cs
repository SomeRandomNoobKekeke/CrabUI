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
        Mod.Logger.Log($"Created {where}");
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
      Mod.Logger.Log("-------- A");
      A a = new();
      Mod.Logger.Log("-------- B");
      B b = new();
      Mod.Logger.Log("-------- C");
      C c = new();

      Mod.Logger.LogVars(a.Prop, b.Prop, c.Prop);
      Mod.Logger.LogVars(a.Prop2, b.Prop2, c.Prop2);

      Mod.Logger.Log($"fields in A:");
      foreach (FieldInfo fi in typeof(A).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
      {
        Mod.Logger.Log($"{fi.DeclaringType}.{fi}");
      }

      Mod.Logger.Log($"fields in B:");
      foreach (FieldInfo fi in typeof(B).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
      {
        Mod.Logger.Log($"{fi.DeclaringType}.{fi}");
      }

      Mod.Logger.Log($"fields in C:");
      foreach (FieldInfo fi in typeof(C).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
      {
        Mod.Logger.Log($"{fi.DeclaringType}.{fi}");
      }
    }
  }
}