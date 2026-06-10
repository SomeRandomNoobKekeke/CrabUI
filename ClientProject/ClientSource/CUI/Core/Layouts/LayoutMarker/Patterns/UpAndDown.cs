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
      public class UpAndDownPattern : Pattern
      {
        public void MarkAsRequireParentUpdate(Target host)
        {
          host.Layout.RequireParentUpdate = true;

          if (host.Parent is not null)
          {
            MarkAsRequireParentUpdate(host.Parent);
          }
        }


        public void MarkAsRequireChildrenUpdate(Target host)
        {
          void MarkRec(Target container)
          {
            container.Layout.RequireChildrenUpdate = true;

            foreach (Target child in container.Children)
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

        public override void MarkFunc(Target host)
        {
          MarkAsRequireParentUpdate(host);
          MarkAsRequireChildrenUpdate(host);
        }
      }
    }
  }

}