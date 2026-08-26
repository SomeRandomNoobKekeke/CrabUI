using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CursedUIUser
{

  /// <summary>
  /// If you don't add setters there will be no compile time errors
  /// It looks like compiler creates props with new keyword in this case
  /// very sneaky
  /// </summary>
  public class WTFWithTheseProps : Experiment
  {
    public class Component
    {
      public class PropType
      {

      }

      public virtual PropType Prop { get; } = new();
    }

    public class Component2 : Component
    {
      public class PropType : Component.PropType
      {

      }

      public override PropType Prop { get; } = new(); // Guess the type)
    }


    public override void Run()
    {

    }
  }
}