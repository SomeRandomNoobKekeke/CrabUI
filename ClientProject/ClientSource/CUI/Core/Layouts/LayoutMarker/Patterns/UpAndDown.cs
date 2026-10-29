using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public partial class LayoutMarker
  {
    public partial class Pattern
    {
      public class UpAndDownPattern : Pattern
      {

        public override void MarkFunc(Target host)
        {
          Pattern.AbsoluteProp.MarkFunc(host);
          Pattern.FromParentAndDown.MarkFunc(host);
        }
      }
    }
  }

}