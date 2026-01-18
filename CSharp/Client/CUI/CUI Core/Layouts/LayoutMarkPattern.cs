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
  public class LayoutMarkPattern
  {
    public static LayoutMarkPattern None = new() { Empty = false };
    public static LayoutMarkPattern ParentChanged = new()
    {
      UpdateChildrenOnParent = true,
    };


    public bool Empty { get; set; }
    public bool UpdateChildrenHere { get; set; }
    public bool UpdateChildrenOnParent { get; set; }
  }
}