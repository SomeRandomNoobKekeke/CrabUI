using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CursedUIUser
{


  public partial class HowLayoutsShouldGenerallyWork : Experiment
  {
    public abstract class Layout
    {
      public interface Host
      {
        public IReadOnlyList<Child> Children { get; }
        public Rectangle Rect { get; set; }
      }
      public interface ChildBase
      {
        public Rectangle Rect { get; set; }
      }
      public interface Child : ChildBase, ListLayout.Child, CUIPlainLayout.Child
      {

      }


      public virtual void ConnectTo(Host host)
      {
        Parent = host;
      }
      private Host Parent;

      public virtual void UpdateChildren()
      {

      }
    }



  }
}