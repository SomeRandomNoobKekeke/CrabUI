using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CrabUIUser
{

  public partial class ClassMapping : Experiment
  {
    /// <summary>
    /// They must expose real selves publicly, i don't like it
    /// </summary>
    public class DirectProxies
    {
      public class PropxyA
      {
        public string Value
        {
          get => Self.Value;
          set => Self.Value = value;
        }

        public void UseB(PropxyB b) { Self.UseB(b.Self); }
        public PropxyC CreateC() => new PropxyC(new TheProblem.C());

        public TheProblem.A Self { get; }
        public PropxyA(TheProblem.A self) => Self = self;
      }

      public class PropxyB
      {
        public void MakeDUseMe(PropxyD d) => d.Self.UseB(this.Self);

        public TheProblem.B Self { get; }
        public PropxyB(TheProblem.B self) => Self = self;
      }
      public class PropxyC
      {
        public TheProblem.C Self { get; }
        public PropxyC(TheProblem.C self) => Self = self;
      }
      public class PropxyD : PropxyA
      {
        public TheProblem.D Self { get; }
        public PropxyD(TheProblem.D self) : base(self) => Self = self;
      }
    }

  }


}