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
    public static LayoutMarkPattern None = new() { Empty = false, Name = "None" };
    public static LayoutMarkPattern ParentChanged = new()
    {
      UpdateChildrenOnParent = true,
      Name = "ParentChanged"
    };


    public string Name { get; set; }
    public bool Empty { get; set; }
    public bool UpdateChildrenHere { get; set; }
    public bool UpdateChildrenOnParent { get; set; }

    public override string ToString() => $"LayoutMarkPattern {Name}";
  }
}