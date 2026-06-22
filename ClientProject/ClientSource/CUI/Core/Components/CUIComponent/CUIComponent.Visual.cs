using System;
using System.Collections.Generic;
using BaroJunk;
using Microsoft.Xna.Framework;


namespace CrabUI
{
  public partial class CUIComponent
  {
    public DebugNode<CUIComponent, CUIRect> Debug_RectSet { get; } = new(
      DebugCategory.RectSet, CUI.DebugHub,
      (component, rect) => $"{component}.Rect = {rect}"
    );

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
    public bool Displayed { get; set; } = true;



    protected bool CulledOut { get; set; }


    protected virtual void UpdateRect(CUIRect rect)
    {
      Debug_PropSet.Send(typeof(CUIRect), rect, this, "Rect");
      Background.Rect = rect;
      RightResizeHandle.UpdateRect();

      if (CullChildren)
      {
        ScissorRect = rect.Box;
      }

      Debug_RectSet.Send(this, rect);
    }

    protected Rectangle? ScissorRect
    {
      get => VisualBounds.ScissorRect;
      set => VisualBounds.ScissorRect = value;
    }
    protected VisualBounds VisualBounds { get; } = new();

    [CUISerializable] //TODO will this just magically work?
    public virtual bool Visible
    {
      get => Background.Visible;
      set => Background.Visible = value;
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;
      if (Tree.Children.Count != 0)
      {
        yield return VisualBounds.LeftBound; //TODO bounds should be yielded only if there's something non standart
        foreach (CUIComponent child in Tree.Children)
        {
          yield return child.VisualWrapper;
        }
        yield return VisualBounds.RightBound;
      }
      yield return RightResizeHandle.VisualWrapper;
    }
  }
}