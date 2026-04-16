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
  /// Bruh, spring update brought back access checks
  /// everything's broken, i'm clueless again
  /// </summary>
  public partial class HowToAccessProtectedInGrandParent : Experiment
  {
    public class A
    {
      protected string Prop = "bruh";
    }
    public class B : A
    {
      protected string Prop = "lol";
      public void PrintProp() => Mod.Logger.Log(this.Prop);
    }
    public class C : B
    {
      protected string Prop = "kek";
      public void PrintProp() => Mod.Logger.Log(this.Prop);
    }

    /// <summary>
    /// Idea is: I can't use protected Prop from parent because it's hidden by prop in D
    /// But if it's private it won't be visible in derived classes 
    /// And i can access parent props from such derived classes
    /// Unfortunatelly doesn't work, if you access CringeD.Prop from D you still get D.Prop even tho it's private
    /// And it's not quite what i want, i want to get Prop from specific parents 
    /// </summary>
    public class CringeD : D
    {
      public string Revealed_Prop => Prop;
      public D Self { get; set; }
      public CringeD(D self) => Self = self;
    }

    public class D : C
    {
      public CringeD Cringe => new CringeD(this);

      private string Prop = "cheburek";
      public void PrintProp() => Mod.Logger.Log(this.Cringe.Prop);
    }

    public override void Run()
    {
      D d = new();
      C c = new();
      B b = new();
      A a = new();

      d.PrintProp();
    }
  }



}