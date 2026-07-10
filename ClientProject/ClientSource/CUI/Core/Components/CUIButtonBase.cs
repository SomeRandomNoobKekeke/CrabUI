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

namespace CrabUI
{
  public abstract partial class CUIButtonBase : CUIComponent, IComponent
  {
    public abstract Color MasterColor { set; }
    public abstract void DetermineColor();

    public TextBlock TextBlock { get; } = new();

    [CUISerializableProp]
    public string Text
    {
      get => TextBlock.Text;
      set => TextBlock.Text = value;
    }

    public float Scale
    {
      get => TextBlock.Scale;
      set => TextBlock.Scale = value;
    }

    public Vector2 TextAnchor
    {
      get => TextBlock.Anchor;
      set => TextBlock.Anchor = value;
    }

    public Color TextColor
    {
      get => TextBlock.TextColor;
      set => TextBlock.TextColor = value;
    }

    public SpriteEffects SpriteEffects
    {
      get => TextBlock.SpriteEffects;
      set => TextBlock.SpriteEffects = value;
    }

    public float LayerDepth
    {
      get => TextBlock.LayerDepth;
      set => TextBlock.LayerDepth = value;
    }

    public CUIFont Font
    {
      get => TextBlock.Font;
      set => TextBlock.Font = value;
    }

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

    public string Emit { get; set; }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;
      yield return TextBlock.VisualWrapper;
    }


    public CUIButtonBase() : base()
    {
      MouseOff += (c, e) => DetermineColor();
      MouseOn += (c, e) => DetermineColor();
      DetermineColor();

      MouseDown += (c, e) =>
      {
        if (Emit != null) Commands.SendUp(Emit, Text);
      };

      ConsumeMouseClicks = true;
    }

    public CUIButtonBase(string text) : this()
    {
      Text = text;
    }
  }
}