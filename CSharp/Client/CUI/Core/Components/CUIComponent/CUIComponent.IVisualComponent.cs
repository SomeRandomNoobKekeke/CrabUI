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
  public partial class CUIComponent : IVisualComponent
  {
    protected SimpleTexture Background { get; } = new();


    IEnumerable<VisualUnit> IVisualComponent.VisualSplit() => VisualSplit();
    protected virtual IEnumerable<VisualUnit> VisualSplit()
    {
      yield return new VisualUnit.PrimitiveVisualElement(Background);
      yield return new VisualUnit.LeftContextBound();
      foreach (CUIComponent child in Children)
      {
        yield return new VisualUnit.NestedVisualComponent(child);
      }
      yield return new VisualUnit.RightContextBound();
    }
  }
}