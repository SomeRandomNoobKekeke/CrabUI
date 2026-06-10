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
  /// I was sure that static initializers are running in the order they appear in code, wtf is going on here?
  /// Ok nevermind they actually do, but i thought that i should get compile time error here
  /// </summary>
  public class StaticInitializers : Experiment
  {
    public class A
    {
      public B B { get; set; }
      public bool IsValid => B is not null;
      public A(B b) => B = b;
    }

    public class B
    {
      public C C { get; set; }
      public bool IsValid => C is not null;
      public B(C c) => C = c;
    }

    public class C
    {
      public A A { get; set; }
      public bool IsValid => A is not null;
      public C(A a) => A = a;
    }

    public static class Bruh
    {
      public static A A = new A(B);
      public static B B = new B(C);
      public static C C = new C(A);
    }

    public override void Run()
    {
      Mod.Logger.LogVars(Bruh.A.IsValid);
      Mod.Logger.LogVars(Bruh.B.IsValid);
      Mod.Logger.LogVars(Bruh.C.IsValid);
    }
  }
}