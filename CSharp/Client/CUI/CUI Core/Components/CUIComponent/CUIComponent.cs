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
  public partial class CUIComponent : CUIVisualComponent, IMouseEventConsumer
  {


    protected SimpleTexture Background { get; } = new();

    protected override Rectangle Rect
    {
      get => Background.Rect;
      set => Background.Rect = value;
    }

    protected override IEnumerable<VisualUnit> VisualSplit()
    {
      yield return new VisualUnit.PrimitiveVisualElement(Background);
      yield return new VisualUnit.LeftContextBound();
      foreach (CUIVisualComponent child in children)
      {
        yield return new VisualUnit.NestedVisualComponent(child);
      }
      yield return new VisualUnit.RightContextBound();
    }


  }
}