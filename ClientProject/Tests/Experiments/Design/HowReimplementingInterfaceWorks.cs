using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CUITests
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

      CUITestPlugin.Logger.LogVars((a as INamed).Name); //A
      CUITestPlugin.Logger.LogVars((b as INamed).Name); //B
      CUITestPlugin.Logger.LogVars(((b as A) as INamed).Name); //B
      CUITestPlugin.Logger.LogVars((bAsA as INamed).Name); //B
    }
  }
}