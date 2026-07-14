using System;
using System.Collections.Generic;
using BaroJunk;
using Microsoft.Xna.Framework;


namespace CrabUI
{
  public partial class CUIComponent
  {
    [CUISerializableProp]
    public SimpleTexture Background { get; } = new();
    public Borders Borders { get; } = new();


    private bool _Displayed = true;
    [CUISerializableProp]
    public bool Displayed
    {
      get => _Displayed;
      set
      {
        if (_Displayed == value) return;
        _Displayed = value;
        VisualRestructureNotifier.Notify();
      }
    }



    protected bool CulledOut { get; set; }

    [CUISerializableProp]
    public bool IgnoretransparentPixels
    {
      get => Background.IgnoretransparentPixels;
      set => Background.IgnoretransparentPixels = value;
    }


    protected virtual void UpdateRects()
    {
      Debug_PropSet.Send(typeof(CUIRect), Rect, this, "Rect");
      Background.Rect = Rect;
      Borders.Rect = Rect;

      RightResizeHandle.UpdateRect();

      if (CullChildren)
      {
        ScissorRect = ChildrenRect.Round();
      }

      Debug_RectSet.Send(this, Rect);
      Events.RectSet.Raise(this, OuterRect);
    }

    protected Rectangle? ScissorRect
    {
      get => VisualBounds.ScissorRect;
      set => VisualBounds.ScissorRect = value;
    }
    protected VisualBounds VisualBounds { get; } = new();

    [CUISerializableProp] //TODO will this just magically work?
    public virtual bool Visible
    {
      get => Background.Visible;
      set => Background.Visible = value;
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;
      if (Children.Count != 0)
      {
        yield return VisualBounds.LeftBound; //TODO bounds should be yielded only if there's something non standart
        foreach (CUIComponent child in Children)
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