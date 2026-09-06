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
  /// It's actually calls base class getter
  /// If prop in base class is autoimplemented you'll get the value of backing field in base class
  /// Which kinda make sense
  /// </summary>
  public class OverridePropWithoutGetter : Experiment
  {

    public class A
    {

      public virtual string Prop { get; set; } = "A.Prop";
    }

    public class B : A
    {
      private string newBackingField;

      public override string Prop
      {
        // get
        // {
        //   return "B.Prop";
        // }
        set
        {
          newBackingField = value;
        }
      }

    }


    public override void Run()
    {
      B b = new B();
      b.Prop = "B.Prop";
      Mod.Logger.Log(b.Prop); // "A.Prop";

      Mod.Logger.Log("fields of A");
      foreach (FieldInfo fi in typeof(A).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
      {
        Mod.Logger.Log(fi);
      }
      Mod.Logger.Log("fields of B");
      foreach (FieldInfo fi in typeof(B).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
      {
        Mod.Logger.Log(fi);
      }
    }
  }
}