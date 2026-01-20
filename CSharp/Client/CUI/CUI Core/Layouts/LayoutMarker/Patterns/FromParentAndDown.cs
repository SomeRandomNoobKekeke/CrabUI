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
  public partial class LayoutMarkPattern
  {
    public class FromParentAndDownPattern : LayoutMarkPattern
    {
      public override void MarkFunc(TreeAdapter<ILayoutHost> host)
      {
        void MarkRec(TreeAdapter<ILayoutHost> host)
        {
          host.Self.MarkAsRequireChildrenUpdate();
          foreach (ILayoutHost child in host.Children)
          {
            MarkRec(child);
          }
        }

        host.Parent?.MarkAsRequireChildrenUpdate();
        MarkRec(host);
      }
    }
  }

}