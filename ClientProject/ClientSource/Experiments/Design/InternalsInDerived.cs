using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  /// <summary>
  /// So i want to hide all internal stuff in some internal object
  /// But then i'll have a name collision with the same object in derived classes
  /// Ofc i can generate InternalAccess objects, but it still looks like cringe
  /// This is 100 times harder than just protected prop at component level
  /// </summary>
  public class InternalsInDerived : Experiment
  {
    public class ComponentA
    {
      public class Internals
      {
        public string PropA { get; set; } = "kek";
      }

      protected Internals Internal_ComponentA { get; } = new();
      public PublicInternalAccess Internal { get; }

      public ComponentA()
      {
        Internal = new(this);
      }
      public class PublicInternalAccess
      {
        private ComponentA Host;
        public PublicInternalAccess(ComponentA host) => Host = host;

        public string PropA
        {
          get => Host.Internal_ComponentA.PropA;
          set => Host.Internal_ComponentA.PropA = value;
        }
      }
    }

    public class ComponentB : ComponentA
    {
      public class Internals
      {
        public string PropB { get; set; } = "lol";
      }

      protected Internals Internal_ComponentB { get; } = new();
      public PublicInternalAccess Internal { get; }

      public ComponentB()
      {
        Internal = new(this);
      }

      public class PublicInternalAccess
      {
        private ComponentB Host;
        public PublicInternalAccess(ComponentB host) => Host = host;

        public string PropA
        {
          get => Host.Internal_ComponentA.PropA;
          set => Host.Internal_ComponentA.PropA = value;
        }

        public string PropB
        {
          get => Host.Internal_ComponentB.PropB;
          set => Host.Internal_ComponentB.PropB = value;
        }
      }
    }

    public override void Run()
    {
      ComponentA componentA = new();
      ComponentB componentB = new();

      Mod.Logger.Log(componentA.Internal.PropA);
      Mod.Logger.Log(componentB.Internal.PropA);
      Mod.Logger.Log(componentB.Internal.PropB);


    }
  }
}