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
      public override void MarkFunc(CUIComponent host)
      {

        void MarkRec(CUIComponent component)
        {
          ((ILayoutHost)component).MarkAsRequireChildrenUpdate();
          foreach (CUIComponent child in component.Children)
          {
            MarkRec(child);
          }
        }

        ((ILayoutHost)host.Parent)?.MarkAsRequireChildrenUpdate();
        MarkRec(host);
      }
    }
  }

}