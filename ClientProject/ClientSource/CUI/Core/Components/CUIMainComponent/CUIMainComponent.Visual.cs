using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using CUILibs;
using CUICodeGenerator;
namespace CrabUI
{
  public partial class CUIMainComponent
  {
    public override IEnumerable<VisualUnit> VisualSplit()
    {
      foreach (CUIVisualComponent child in Children)
      {
        yield return child.VisualWrapper;
      }
    }
  }
}