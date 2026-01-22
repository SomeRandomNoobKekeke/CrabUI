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
  // It's too hard to decouple from CUIComponent, i will just pretend that it's not a problem
  public partial class LayoutMarkPattern
  {
    public static LayoutMarkPattern None = new LayoutMarkPattern() { Empty = true };
    public static LayoutMarkPattern FromParentAndDown = new FromParentAndDownPattern();
    public static LayoutMarkPattern OnlyParent = new OnlyParentPattern();


    public bool Empty { get; set; }

    public virtual void MarkFunc(CUIComponent host) { }
  }
}