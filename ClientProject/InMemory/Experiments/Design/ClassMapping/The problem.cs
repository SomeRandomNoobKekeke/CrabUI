using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CursedUIUser
{

  public partial class ClassMapping : Experiment
  {
    /// <summary>
    /// We got some classes
    /// Now i want user to operate on a different set of classes that behave similarly
    /// and could be mapped to originals, and also User shouldn't know that they are mapping to something
    /// How do i construct such a set?
    /// Spoilers: a proper solution would be to put them in a separate assembly with internal modifiers
    /// but i can't do that
    /// </summary>
    public class TheProblem
    {
      // Original classes
      public class A
      {
        public string Value { get; set; }

        public void UseB(B b) { }
        public C CreateC() => new C();
      }

      public class B
      {
        public void MakeDUseMe(D d) => d.UseB(this);
      }
      public class C { }
      public class D : A { }

      public class User
      {

      }
    }

    public override void Run() { }
  }


}