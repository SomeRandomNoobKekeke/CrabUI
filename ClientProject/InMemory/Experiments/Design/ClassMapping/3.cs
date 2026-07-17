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
    /// never mind, this is exactly the same as 2
    /// I must cast interfaces up on every step
    /// </summary>
    public class ProxiesWithInterfaces
    {
      // public interface IA
      // {
      //   public string Value { get; set; }

      //   public void UseB(IB b);
      //   public IC CreateC();
      // }

      // public interface IB
      // {
      //   public void MakeDUseMe(ID d);
      // }
      // public interface IC { }
      // public interface ID : IA { }

      // public class PropxyA : IA
      // {
      //   public string Value
      //   {
      //     get => Self.Value;
      //     set => Self.Value = value;
      //   }

      //   public void UseB(IB b) { Self.UseB(); }
      //   public PropxyC CreateC() => new PropxyC(new TheProblem.C());

      //   public TheProblem.A Self { get; }
      //   public PropxyA(TheProblem.A self) => Self = self;
      // }

      // public class PropxyB : IB
      // {
      //   public void MakeDUseMe(PropxyD d) => d.Self.UseB(this.Self);

      //   public TheProblem.B Self { get; }
      //   public PropxyB(TheProblem.B self) => Self = self;
      // }
      // public class PropxyC : IC
      // {
      //   public TheProblem.C Self { get; }
      //   public PropxyC(TheProblem.C self) => Self = self;
      // }
      // public class PropxyD : PropxyA, ID
      // {
      //   public TheProblem.D Self { get; }
      //   public PropxyD(TheProblem.D self) => Self = self;
      // }
    }

  }


}