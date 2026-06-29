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
    public Borders Borders { get; } = new();



    [CUISerializable]
    public bool Displayed { get; set; } = true;



    protected bool CulledOut { get; set; }


    protected virtual void UpdateRects()
    {
      Debug_PropSet.Send(typeof(CUIRect), Rect, this, "Rect");
      Background.Rect = Rect;
      Borders.Rect = Rect;

      RightResizeHandle.UpdateRect();

      if (CullChildren)
      {
        ScissorRect = Rect.Box;
      }

      Debug_RectSet.Send(this, Rect);
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
      yield return Borders.VisualWrapper;
    }
  }
}