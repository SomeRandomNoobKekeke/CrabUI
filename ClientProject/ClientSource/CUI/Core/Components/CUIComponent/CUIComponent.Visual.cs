using System;
using System.Collections.Generic;
using CUILibs;
using Microsoft.Xna.Framework;


namespace CrabUI
{
  public partial class CUIComponent
  {
    [CUISerializableProp]
    public SimpleTexture Background { get; } = new();
    [CUISerializableProp]
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


    [CUISerializableProp]
    public bool CullChildren { get; set; }
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

    /// <summary>
    /// Half assed substitution for z-index, set to CUIDirection.Reverse to draw children in reverse order
    /// </summary>
    [CUISerializableProp]
    public CUIDirection VisualChildrenOrder { get; set; } = CUIDirection.Straight;

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;
      if (Children.Count != 0)
      {
        yield return VisualBounds.LeftBound; //TODO bounds should be yielded only if there's something non standart

        if (VisualChildrenOrder == CUIDirection.Straight)
        {
          for (int i = 0; i < Children.Count; i++)
          {
            yield return Children[i].VisualWrapper;
          }
        }
        else
        {
          for (int i = Children.Count - 1; i >= 0; i--)
          {
            yield return Children[i].VisualWrapper;
          }
        }
        yield return VisualBounds.RightBound;
      }
      yield return RightResizeHandle.VisualWrapper;
      yield return Borders.VisualWrapper;
    }
  }
}