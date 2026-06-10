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
      public class AbsolutePropPattern : Pattern
      {
        public override void MarkFunc(Target host)
        {
          host.Layout.RequireParentUpdate = true;

          if (host.Parent is not null)
          {
            MarkFunc(host.Parent);
          }
        }
      }
    }
  }

}