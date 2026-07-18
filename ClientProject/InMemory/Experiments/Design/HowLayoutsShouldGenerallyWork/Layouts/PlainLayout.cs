using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CrabUIUser
{


  public partial class HowLayoutsShouldGenerallyWork : Experiment
  {
    public class CUIPlainLayout : Layout
    {
      public interface Host : Layout.Host
      {

      }
      public interface Child : Layout.ChildBase
      {
        public Rectangle Absolute { get; }
      }


      public override void ConnectTo(Layout.Host host)
      {
        base.ConnectTo(host);
        Parent = host as Host;
      }

      private Host Parent;


      public override void UpdateChildren()
      {
        foreach (Child child in Parent.Children)
        {
          child.Rect = child.Absolute;
        }
      }
    }



  }
}