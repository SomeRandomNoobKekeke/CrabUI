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
    /// now user don't know that object Self is, but it still can touch it if he wants
    /// and it makes things much uglier and doesn't fix anything 
    /// </summary>
    public class InconvenientProxies
    {
      public class PropxyA
      {
        public string Value
        {
          get => (Self as TheProblem.A).Value;
          set => (Self as TheProblem.A).Value = value;
        }

        public void UseB(PropxyB b) { (Self as TheProblem.A).UseB((b.Self as TheProblem.B)); }
        public PropxyC CreateC() => new PropxyC(new TheProblem.C());

        public object Self { get; }
        public PropxyA(object self) => Self = self;
      }

      public class PropxyB
      {
        public void MakeDUseMe(PropxyD d) => (d.Self as TheProblem.D).UseB((this.Self as TheProblem.B));

        public object Self { get; }
        public PropxyB(object self) => Self = self;
      }
      public class PropxyC
      {
        public object Self { get; }
        public PropxyC(object self) => Self = self;
      }
      public class PropxyD : PropxyA
      {
        public object Self { get; }
        public PropxyD(object self) : base(self) => Self = self;
      }
    }

  }


}