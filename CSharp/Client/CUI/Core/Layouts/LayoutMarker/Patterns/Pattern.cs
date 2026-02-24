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
      public static Pattern None = new Pattern() { Empty = true };
      public static Pattern FromParentAndDown = new FromParentAndDownPattern();
      public static Pattern OnlyParent = new OnlyParentPattern();

      public bool Empty { get; set; }

      public virtual void MarkFunc(IMarkableLayoutContainer Host) { }
    }
  }

}