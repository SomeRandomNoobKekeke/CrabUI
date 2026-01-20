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
    public static LayoutMarkPattern None = new LayoutMarkPattern();
    public static LayoutMarkPattern FromParentAndDown = new FromParentAndDownPattern();

    public virtual void MarkFunc(TreeAdapter<ILayoutHost> host) { }
  }
}