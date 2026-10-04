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
      public class UpdateAllPattern : Pattern
      {
        public void MarkUp(Target host)
        {
          host.Layout.RequireParentUpdate = true;
          host.Layout.RequireChildrenUpdate = true;

          if (host.Parent is not null)
          {
            MarkUp(host.Parent);
          }
        }


        public void MarkDown(Target host)
        {
          void MarkRec(Target container)
          {
            container.Layout.RequireChildrenUpdate = true;
            container.Layout.RequireParentUpdate = true;

            foreach (Target child in container.Children)
            {
              MarkRec(child);
            }
          }

          MarkRec(host);
        }

        public override void MarkFunc(Target host)
        {
          MarkUp(host);
          MarkDown(host);
        }
      }
    }
  }

}