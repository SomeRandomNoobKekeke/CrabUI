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
    protected SimpleTexture Background { get; } = new();

    protected virtual IVisualComponent AsVisualComponent => VisualRepresentation;
    protected VisualRepresentation_Part VisualRepresentation { get; } = new();
    public class VisualRepresentation_Part : Part, IVisualComponent
    {
      public IEnumerable<VisualUnit> VisualSplit()
      {
        yield return new VisualUnit.PrimitiveVisualElement(Self.Background);
        yield return new VisualUnit.LeftContextBound();
        foreach (CUIComponent child in Self.Tree.Children)
        {
          yield return new VisualUnit.NestedVisualComponent(child.AsVisualComponent);
        }
        yield return new VisualUnit.RightContextBound();
      }
    }
  }
}