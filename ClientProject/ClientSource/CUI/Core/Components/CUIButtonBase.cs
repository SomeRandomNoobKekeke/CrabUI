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

namespace CursedUI
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
      set
      {
        TextBlock.Text = value;
        LayoutMarker.Mark(LayoutMarker.Pattern.FromParentAndDown);
      }
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

    public bool PlaySound { get; set; } = true;
    public GUISoundType ClickSound { get; set; } = GUISoundType.Select;//TODO don't reference it directly? there should be some cui sound manager


    protected override CUINullVector2 MinSizeOverride => new CUINullVector2(
      TextBlock.ForcedSize.X + Padding.FullWidth,
      TextBlock.ForcedSize.Y + Padding.FullHeigth
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

      yield return VisualBounds.LeftBound;
      yield return TextBlock.VisualWrapper;
      yield return VisualBounds.RightBound;

      yield return Borders.VisualWrapper;
    }


    public CUIButtonBase() : base()
    {
      MouseOff += (e) => DetermineColor();
      MouseOn += (e) => DetermineColor();
      DetermineColor();

      MouseDown += (e) =>
      {
        if (PlaySound) SoundPlayer.PlayUISound(ClickSound);
        if (Emit != null) Commands.SendUp(Emit, Text);
      };

      ConsumeMouseEvents = true;
    }

    public CUIButtonBase(string text) : this()
    {
      Text = text;
    }
  }
}