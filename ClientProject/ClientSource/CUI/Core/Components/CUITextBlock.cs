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

namespace CrabUI
{
  public partial class CUITextBlock : CUIComponent, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUITextBlock>((c) =>
    {
      c.TextColor = c.Palette["text"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      Padding = new(2, 0, 2, 0);
    }

    public TextBlock TextBlock { get; } = new();

    [CUISerializableProp]
    public string Text
    {
      get => TextBlock.Text;
      set => TextBlock.Text = value;
    }
    [CUISerializableProp]
    public float Scale
    {
      get => TextBlock.Scale;
      set => TextBlock.Scale = value;
    }
    [CUISerializableProp]
    public Vector2 TextAnchor
    {
      get => TextBlock.Anchor;
      set => TextBlock.Anchor = value;
    }
    [CUISerializableProp]
    public Color TextColor
    {
      get => TextBlock.TextColor;
      set => TextBlock.TextColor = value;
    }
    [CUISerializableProp]
    public SpriteEffects SpriteEffects
    {
      get => TextBlock.SpriteEffects;
      set => TextBlock.SpriteEffects = value;
    }
    [CUISerializableProp]
    public float LayerDepth
    {
      get => TextBlock.LayerDepth;
      set => TextBlock.LayerDepth = value;
    }

    //TODO [CUISerializableProp]
    public CUIFont Font
    {
      get => TextBlock.Font;
      set => TextBlock.Font = value;
    }
    [CUISerializableProp]
    public ResizeStrategy ResizeStrategy
    {
      get => TextBlock.ResizeStrategy;
      set => TextBlock.ResizeStrategy = value;
    }

    protected override CUINullVector2 MinSizeOverride => new CUINullVector2(
      TextBlock.ForcedSize.X,
      TextBlock.ForcedSize.Y
    );

    protected override void UpdateRects()
    {
      base.UpdateRects();
      TextBlock.Rect = ChildrenRect;
    }

    [CUISerializableProp]
    public override bool Visible
    {
      get => Background.Visible;
      set
      {
        Background.Visible = value;
        TextBlock.Visible = value;
      }
    }
    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;
      yield return TextBlock.VisualWrapper;
    }

    public CUITextBlock() : base() { }
    public CUITextBlock(string text) : base() => Text = text;

  }
}