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
    public class OnlyParentPattern : LayoutMarkPattern
    {
      public override void MarkFunc(CUIComponent host)
      {
        ((ILayoutHost)host.Parent)?.MarkAsRequireChildrenUpdate();
      }
    }
  }

}