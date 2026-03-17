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
  public partial class CUIComponent
  {
    protected virtual Visual_Part Visual { get; } = new();
    public class Visual_Part : Part, IVisualComponent
    {
      public SimpleTexture Background { get; } = new();

      public virtual IEnumerable<VisualUnit> VisualSplit()
      {
        yield return new VisualUnit.PrimitiveVisualElement(Background);
        yield return new VisualUnit.LeftContextBound();
        foreach (CUIComponent child in Self.Tree.Children)
        {
          yield return new VisualUnit.NestedVisualComponent(child.Visual);
        }
        yield return new VisualUnit.RightContextBound();
      }
    }
  }
}