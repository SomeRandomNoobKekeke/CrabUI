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
  /// I can derive a class, but i can't access privates of a base outer class
  /// Should be a compile time error btw
  /// </summary>
  public class CanIDeriveFromNestedClass : Experiment
  {

    // public class WrapperA
    // {
    //   private string Secret = "Secret";

    //   public class NestedA
    //   {

    //   }
    // }

    // public class WrapperB
    // {
    //   public class NestedB : WrapperA.NestedA
    //   {
    //     public string GetSecret(WrapperA wrapperA)
    //     {
    //       return wrapperA.Secret;
    //     }
    //   }

    //   public NestedB nestedB = new NestedB();
    // }


    public override void Run()
    {
      // WrapperA wrapperA = new WrapperA();
      // WrapperB wrapperB = new WrapperB();

      // wrapperB.nestedB.GetSecret(wrapperA);
    }
  }
}