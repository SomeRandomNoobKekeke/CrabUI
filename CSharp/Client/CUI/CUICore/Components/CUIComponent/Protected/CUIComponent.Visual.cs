using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {
    public virtual Visual_Part Visual { get; } = new();
    public class Visual_Part : Part, IModule, IVisualComponent
    {
      [In] public Tree_Part Tree { get; set; }

      public SimpleTexture Background { get; } = new();

      //TODO shouldn't this be in the interface?
      public void UpdateRect(CUIRect rect)
      {
        Background.Rect = rect.Box;
      }

      public virtual IEnumerable<VisualUnit> VisualSplit()
      {
        yield return new VisualUnit.PrimitiveVisualElement(Background);
        yield return new VisualUnit.LeftContextBound();
        foreach (CUIComponent child in Tree.Children)
        {
          yield return new VisualUnit.NestedVisualComponent(child.Visual);
        }
        yield return new VisualUnit.RightContextBound();
      }
    }
  }
}