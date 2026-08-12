using System;
using System.Collections.Generic;
using CUICodeGenerator;
using CUILibs;
using Microsoft.Xna.Framework;


namespace CrabUI
{
  public partial class CUIComponent
  {
    [InitMethod]
    protected void InitVisualSlots()
    {
      BackgroundSlot = new Slot<SimpleTexture>()
      {
        OnWireUp = (background) =>
        {
          // DebugRelays[DebugCategory.RoundedRect].Route(background.Debug_RoundedRect);
          Events.Route(background);
          background.FocusProbed.Add(HandleFocusProbe);
        },
        OnTearDown = (background) =>
        {
          // DebugRelays[DebugCategory.RoundedRect].Route(background.Debug_RoundedRect);
          Events.Route(background);
          background.FocusProbed.Add(HandleFocusProbe);
        },
        Value = new(),
      };
    }


    protected Slot<SimpleTexture> BackgroundSlot;

    [CUISerializableProp]
    public SimpleTexture Background
    {
      get => BackgroundSlot.Value;
      set => BackgroundSlot.Value = value;
    }

    [CUISerializableProp]
    public Borders Borders { get; } = new();

    [CUISerializableProp]
    public bool IgnoretransparentPixels
    {
      get => Background.IgnoretransparentPixels;
      set => Background.IgnoretransparentPixels = value;
    }

    protected virtual void UpdateRects()
    {
      Background.Rect = Rect;
      Borders.Rect = Rect;

      RightResizeHandle.UpdateRect();

      if (CullChildren)
      {
        ScissorRect = ChildrenRect.Round();
      }

      Debug_RectSet.Send(this, OuterRect);
      Events.RectSet.Raise(this, OuterRect);
    }

    protected Rectangle? ScissorRect
    {
      get => VisualBounds.ScissorRect;
      set => VisualBounds.ScissorRect = value;
    }
    protected VisualBounds VisualBounds { get; } = new();

    public override bool Visible
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