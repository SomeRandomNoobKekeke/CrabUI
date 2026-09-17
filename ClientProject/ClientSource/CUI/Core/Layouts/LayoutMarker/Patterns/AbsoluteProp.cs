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
      public class AbsolutePropPattern : Pattern
      {

        private void MarkUp(Target host)
        {
          host.Layout.RequireParentUpdate = true;

          if (host.Parent is not null)
          {
            MarkUp(host.Parent);
          }
          else // We reached the top
          {
            MarkDown(host);
          }
        }

        private void MarkDown(Target container)
        {
          container.Layout.RequireChildrenUpdate = true;

          foreach (Target child in container.Children)
          {
            MarkDown(child);
          }
        }

        public override void MarkFunc(Target host)
        {
          MarkUp(host);
        }
      }
    }
  }

}