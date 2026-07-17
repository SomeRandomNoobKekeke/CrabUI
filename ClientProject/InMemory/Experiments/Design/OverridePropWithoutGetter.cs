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
  /// It's actually calls base class getter
  /// If prop in base class is autoimplemented you'll get the value of backing field in base class
  /// Which is sneaky
  /// </summary>
  public class OverridePropWithoutGetter : Experiment
  {

    public class A
    {

      public virtual string Prop
      {
        get
        {
          return "A.Prop";
        }
        set
        {

        }
      }
    }

    public class B : A
    {
      public override string Prop
      {
        // get
        // {
        //   return "B.Prop";
        // }
        set
        {

        }
      }

    }


    public override void Run()
    {
      B b = new B();
      Mod.Logger.Log(b.Prop);
    }
  }
}