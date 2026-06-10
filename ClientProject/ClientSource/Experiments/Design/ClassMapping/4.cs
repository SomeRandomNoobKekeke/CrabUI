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
    /// now PropxyA doesn't have to extract self from PropxyB
    /// But external MappingLayer has to
    /// </summary>
    public class MappingLayerTest
    {
      public static class MappingLayer
      {
        public static void A_UseB_B(PropxyA propxyA, PropxyB propxyB)
        {
          TheProblem.A a = propxyA.Self;
          TheProblem.B b = propxyB.Self;

          a.UseB(b);
        }
      }

      public class PropxyA
      {
        public void UseB(PropxyB b) => MappingLayer.A_UseB_B(this, b);

        public TheProblem.A Self { get; }
        public PropxyA(TheProblem.A self) => Self = self;
      }

      public class PropxyB
      {
        public TheProblem.B Self { get; }
        public PropxyB(TheProblem.B self) => Self = self;
      }

    }

  }


}