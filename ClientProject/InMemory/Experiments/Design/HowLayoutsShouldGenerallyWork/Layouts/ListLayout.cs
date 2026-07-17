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
    public class ListLayout : Layout
    {
      public interface Host : Layout.Host
      {
        public bool ResizeChildrenToHost { get; }
      }
      public interface Child : Layout.ChildBase
      {
        public bool Flex { get; }
      }


      public override void ConnectTo(Layout.Host host)
      {
        base.ConnectTo(host);
        Parent = host as Host;
      }

      private Host Parent;


      public void UpdateChildren()
      {
        if (Parent.ResizeChildrenToHost)
        {
          foreach (Child child in Parent.Children)
          {
            child.Rect = child.Flex ? new Rectangle(0, 0, 0, 0) : Parent.Rect;
          }
        }
      }
    }



  }
}