using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CursedUIUser
{


  public class HowReimplementingInterfaceWorks : Experiment
  {
    public interface INamed
    {
      public string Name { get; }
    }

    public class A : INamed
    {
      string INamed.Name { get; } = "A";
    }

    public class B : A, INamed
    {
      string INamed.Name { get; } = "B";
    }

    public override void Run()
    {
      A a = new A();
      B b = new B();

      A bAsA = b;

      Mod.Logger.LogVars((a as INamed).Name); //A
      Mod.Logger.LogVars((b as INamed).Name); //B
      Mod.Logger.LogVars(((b as A) as INamed).Name); //B
      Mod.Logger.LogVars((bAsA as INamed).Name); //B
    }
  }
}