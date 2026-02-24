using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class LayoutMarker
  {
    public partial class Pattern
    {
      public class OnlyParentPattern : Pattern
      {
        public override void MarkFunc(IMarkableLayoutContainer host)
        {
          host.Parent.Layout.RequireChildrenUpdate = true;
        }
      }
    }
  }



}