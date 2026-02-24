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
      public class FromParentAndDownPattern : Pattern
      {
        public override void MarkFunc(IMarkableLayoutContainer host)
        {
          void MarkRec(IMarkableLayoutContainer container)
          {
            container.Layout.RequireChildrenUpdate = true;

            foreach (IMarkableLayoutContainer child in container.Children)
            {
              MarkRec(child);
            }
          }

          if (host.Parent is not null)
          {
            host.Parent.Layout.RequireChildrenUpdate = true;
          }

          MarkRec(host);
        }
      }
    }
  }

}