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
  /// The problem is: you can't access shadowed protected props in base class, this means you can generate code accessing those parts
  /// All solutions resolve naming conflicts one way or another, i think this is the most flexuble one
  /// </summary>
  public class InjectingInheritedParts : Experiment
  {
    public partial class A
    {
      protected class Self_As_A_Part
      {
        public A Self { get; set; }

        public A_FunnyPart Funny
        {
          get => Self.Funny;
          set => Self.Funny = value;
        }
        public A_BoringPart Boring
        {
          get => Self.Boring;
          set => Self.Boring = value;
        }
      }

      protected Self_As_A_Part Self_As_A { get; } = new();

      public class A_FunnyPart
      {

      }

      public class A_BoringPart
      {

      }

      protected A_FunnyPart Funny { get; private set; }
      protected A_BoringPart Boring { get; private set; }



      public virtual void Inject()
      {
        Self_As_A.Self = this;
        Self_As_A.Funny = new A_FunnyPart();
        Self_As_A.Boring = new A_BoringPart();
      }
    }


    public partial class B : A
    {
      protected class Self_As_B_Part
      {
        public B Self { get; set; }

        public B_FunnyPart Funny
        {
          get => Self.Funny;
          set => Self.Funny = value;
        }
        public B_BoringPart Boring
        {
          get => Self.Boring;
          set => Self.Boring = value;
        }
      }

      protected Self_As_B_Part Self_As_B { get; } = new();

      public class B_FunnyPart
      {

      }

      public class B_BoringPart
      {

      }

      protected B_FunnyPart Funny { get; private set; }
      protected B_BoringPart Boring { get; private set; }



      public override void Inject()
      {
        Self_As_A.Self = this;
        Self_As_B.Self = this;
        Self_As_A.Funny = new A_FunnyPart();
        Self_As_A.Boring = new A_BoringPart();
        Self_As_B.Funny = new B_FunnyPart();
        Self_As_B.Boring = new B_BoringPart();
      }
    }



    public override void Run()
    {
      A a = new();
      B b = new();

      b.Inject();

      foreach (PropertyInfo pi in typeof(B).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic))
      {
        Mod.Logger.Log($"{pi} not null:[{pi.GetValue(b) != null}]");
      }
    }
  }
}