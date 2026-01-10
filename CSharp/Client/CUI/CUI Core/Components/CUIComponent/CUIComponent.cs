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
  public partial class CUIComponent : IVisualComponent, IBasicLayoutElement, IMouseEventConsumer
  {
    public CUIMainComponent MainComponent { get; private set; }


    protected SimpleTexture Background { get; } = new();


    Rectangle IRectElement.Rect { get => Rect; set => Rect = value; }
    protected virtual Rectangle Rect
    {
      get => Background.Rect;
      set => Background.Rect = value;
    }

    IEnumerable<VisualUnit> IVisualComponent.VisualSplit() => VisualSplit();
    protected virtual IEnumerable<VisualUnit> VisualSplit()
    {
      yield return new VisualUnit.PrimitiveVisualElement(Background);
      yield return new VisualUnit.LeftContextBound();
      foreach (CUIComponent child in children)
      {
        yield return new VisualUnit.NestedVisualComponent(child);
      }
      yield return new VisualUnit.RightContextBound();
    }
  }
}