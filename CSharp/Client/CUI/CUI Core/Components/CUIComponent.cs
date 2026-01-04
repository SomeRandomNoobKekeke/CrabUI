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
  public partial class CUIComponent : CUIVisualComponent
  {
    public SimpleRectTexture SimpleRectTexture { get; private set; }

    protected override void InitModules()
    {
      SimpleRectTexture = new();
    }

    public override IEnumerable<VI.VisualFlattenerInstruction> VisualSplit()
    {
      yield return new VI.PrimitiveVisualElement(SimpleRectTexture);
      yield return new VI.LeftContextBound();
      foreach (CUIVisualComponent child in children)
      {
        yield return new VI.NestedVisualComponent(child);
      }
      yield return new VI.RightContextBound();
    }


  }
}