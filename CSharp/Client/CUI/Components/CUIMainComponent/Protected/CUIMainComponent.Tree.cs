using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentInjector;

namespace CrabUI
{

  public partial class CUIMainComponent
  {
    protected override CUIComponent.Tree_Part Tree { get; } = new Tree_Part();
    public class Tree_Part : CUIComponent.Tree_Part
    {
      public override void OnChildAdded(CUIComponent child)
      {
        CUI.Logger.Log($"OnChildAdded {child}");
      }

    }
  }
}