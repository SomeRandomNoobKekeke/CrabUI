using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;
using CUICodeGenerator;
namespace CrabUI
{
  public partial class CUIMainComponent
  {
    public override IEnumerable<VisualUnit> VisualSplit()
    {
      foreach (CUIComponent child in Tree.Children)
      {
        yield return child.VisualWrapper;
      }
    }
  }
}