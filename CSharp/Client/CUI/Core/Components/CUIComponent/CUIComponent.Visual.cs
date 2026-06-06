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
    public SimpleTexture Background { get; } = new();

    protected CUIRect _Rect;
    public override CUIRect Rect
    {
      get => _Rect;
      set
      {
        _Rect = value;
        UpdateRect(value);
      }
    }

    [CUISerializable]
    public bool Visible { get; set; } = true;
    protected bool CulledOut { get; set; }


    protected virtual void UpdateRect(CUIRect rect)
    {
      Debug_PropSet.Send(typeof(CUIRect), rect, this, "Rect");
      Background.Rect = rect;
      RightResizeHandle.UpdateRect();
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Visible || CulledOut) yield break;

      yield return new VisualUnit.PrimitiveVisualElement(Background);
      yield return new VisualUnit.LeftContextBound();
      foreach (CUIComponent child in Tree.Children)
      {
        yield return new VisualUnit.NestedVisualComponent(child);
      }
      yield return new VisualUnit.RightContextBound();
      yield return new VisualUnit.NestedVisualComponent(RightResizeHandle);
    }
  }
}