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
  /// I think it's easier to just check things like this than to read docs
  /// </summary>
  public class ProtectedInterfaceMembers : Experiment
  {
    public interface StrangeInterface
    {
      protected string Prop { get; set; }

      // private string Secret { get; set; } // must declare a body
    }


    public class StrangeClass : StrangeInterface
    {
      // protected string Prop { get; set; } // cannot implement a member coz it's not public
      // string Prop { get; set; } // cannot implement a member coz it's not public
      // protected string StrangeInterface.Prop { get; set; } // modifier is not valid for this item
      string StrangeInterface.Prop { get; set; } // doens't throw an error, but i can't use it

      // public string NormalProp => this.Prop; // no such prop
    }



    public override void Run()
    {

    }
  }
}