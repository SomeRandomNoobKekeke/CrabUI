using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  /// <summary>
  /// So dict always uses both, GetHashCode and Equals to find the value 
  /// even if there's only 1 element with such hash
  /// </summary>
  public partial class HashCodeExperiment : Experiment
  {
    public class A
    {
      public int ID { get; set; }

      public override int GetHashCode() => ID;

      // public override bool Equals(object obj)
      // {
      //   if (obj is not A other) return false;
      //   return ID == other.ID;
      // }
    }

    public override void Run()
    {
      A a1 = new A() { ID = 1 };
      A a2 = new A() { ID = 1 };
      A a3 = new A() { ID = 1 };

      Dictionary<A, string> dict = new Dictionary<A, string>()
      {
        [a1] = "a1",
        [a2] = "a2",
        [a3] = "a3",
      };

      Mod.Logger.Log(dict[a1]);
      Mod.Logger.Log(dict[a2]);
      Mod.Logger.Log(dict[a3]);
    }
  }



}