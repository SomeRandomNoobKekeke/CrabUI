using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;
using ComponentInjector;
namespace CrabUI
{
  public partial class CUIMainComponent
  {
    protected override CUIComponent.Visual_Part Visual { get; } = new Visual_Part();
    public class Visual_Part : CUIComponent.Visual_Part
    {
      public override IEnumerable<VisualUnit> VisualSplit()
      {
        yield break;
      }
    }
  }
}