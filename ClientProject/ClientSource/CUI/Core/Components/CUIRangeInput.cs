using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;
using Barotrauma.Extensions;
using CUILibs;

namespace CrabUI
{
  public partial class CUIRangeInput : CUIComponent, IComponent
  {
    protected override void InitStyle()
    {
      base.InitStyle();
      Background.Color = Color.Brown;

      LeftLineEnd.Color = Color.Red;
      RightLineEnd.Color = Color.Red;
    }

    public float Lambda
    {
      get;
      set;
    }

    public SimpleTexture LeftLineEnd { get; } = new() { Sprite = CUISprite.LeftLineEnd };
    public SimpleTexture RightLineEnd { get; } = new() { Sprite = CUISprite.RightLineEnd };
    public SimpleTexture LineCenter { get; } = new() { Sprite = CUISprite.LineCenter };
    public CUIComponent Handle { get; }

    protected override void UpdateRects()
    {
      base.UpdateRects();

      float squareSide = ChildrenRect.Height;

      LeftLineEnd.Rect = new CUIRect(
        ChildrenRect.Left,
        ChildrenRect.Top,
        squareSide,
        ChildrenRect.Height
      );

      RightLineEnd.Rect = new CUIRect(
        ChildrenRect.Left + ChildrenRect.Width - squareSide,
        ChildrenRect.Top,
        squareSide,
        ChildrenRect.Height
      );

      LineCenter.Rect = new CUIRect(
        ChildrenRect.Left + squareSide,
        ChildrenRect.Top,
        ChildrenRect.Width - 2 * squareSide,
        ChildrenRect.Height
      );

      Handle.Rect = new CUIRect(
        ChildrenRect.Left + (ChildrenRect.Width - squareSide) * Lambda,
        ChildrenRect.Top,
        squareSide,
        ChildrenRect.Height
      );
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;
      yield return LineCenter.VisualWrapper;
      yield return LeftLineEnd.VisualWrapper;
      yield return RightLineEnd.VisualWrapper;
      yield return Handle.VisualWrapper;
      yield return Borders.VisualWrapper;
    }

    public CUIRangeInput()
    {
      ChildrenBounds = CUIBoundaries.Box;

      this["handle"] = Handle = new CUIComponent()
      {
        Background =
        {
          Sprite =  CUISprite.Handle,
          Color = Color.Red,
        },
        Relative = new CUINullRect(h: 1),
        CrossRelative = new CUINullRect(w: 1),
        Draggable = true,
      };

      RectSet += (c, rect) =>
      {
        Handle.Absolute = Handle.Absolute with
        {
          Left = Lambda * (ChildrenRect.Width - Handle.Rect.Width)
        };
      };

      Handle.Dragged += (c, v) =>
      {
        Lambda = (Handle.Rect.Left - ChildrenRect.Left) / (ChildrenRect.Width - Handle.Rect.Width);
      };
    }
  }
}