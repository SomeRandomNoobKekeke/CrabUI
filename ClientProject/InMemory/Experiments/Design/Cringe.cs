using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class Cringe : Experiment
  {
    public class A
    {
      public class Internals
      {
        public string PropA { get; set; }
      }

      public Internals Bruh { get; set; } = new();
    }

    public class B : A
    {
      public class Internals
      {
        public string PropB { get; set; }
      }

      public Internals Bruh { get; set; } = new();
    }


    public override void Run()
    {
      A a = new();
      B b = new();

      foreach (PropertyInfo pi in typeof(B).GetProperties())
      {
        Mod.Logger.Log(pi);
      }
    }
  }



}