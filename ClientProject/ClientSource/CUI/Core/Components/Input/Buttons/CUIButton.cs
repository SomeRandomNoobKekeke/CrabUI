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
  [GeneratedComponent]
  public partial class CUIButton : CUIButtonBase, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIButton>((c) =>
    {
      c.MasterColor = c.Palette["main"];
      c.TextColor = c.Palette["text"];
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      Padding = new(2, 4, 2, 4);
      Background.Sprite = CUISprite.Vignette;
    }

    public TextState_Part TextState { get; } = new();

    [CUISerializableProp]
    public Color MouseOverColor
    {
      get => TextState.MouseOverColor;
      set => TextState.MouseOverColor = value;
    }
    [CUISerializableProp]
    public Color MousePressedColor
    {
      get => TextState.MousePressedColor;
      set => TextState.MousePressedColor = value;
    }
    [CUISerializableProp]
    public Color InactiveColor
    {
      get => TextState.InactiveColor;
      set => TextState.InactiveColor = value;
    }


    public override Color MasterColor
    {
      set
      {
        InactiveColor = value.MultOpaque(0.8f);
        MouseOverColor = value.MultOpaque(0.9f);
        MousePressedColor = value;
        DetermineColor();
      }
    }

    public override void DetermineColor()
    {
      Background.Color = InactiveColor;
      if (MouseOver) Background.Color = MouseOverColor;
      if (MousePressed) Background.Color = MousePressedColor;
    }

    public new Action<CUIButton> Style
    {
      set => PersonalStyle = new CUIActionStyle<CUIButton>("personal", value);
    }

    protected override CUINullVector2 MinSizeOverride => new CUINullVector2(
      TextState.TextBlock.ForcedSize.X + Padding.FullWidth,
      TextState.TextBlock.ForcedSize.Y + Padding.FullHeigth
    );

    protected override void UpdateRects()
    {
      base.UpdateRects();
      TextState.TextBlock.Rect = ChildrenRect;
    }

    [CUISerializableProp]
    public override bool Visible
    {
      get => Background.Visible;
      set
      {
        Background.Visible = value;
        TextState.TextBlock.Visible = value;
      }
    }




    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;

      yield return VisualBounds.LeftBound;
      yield return TextState.TextBlock.VisualWrapper;
      yield return VisualBounds.RightBound;

      yield return Borders.VisualWrapper;
    }


    #region Forwarded to TextState
    public string Text { get => TextState.Text; set => TextState.Text = value; }
    public Color TextColor { get => TextState.TextColor; set => TextState.TextColor = value; }
    public float Scale { get => TextState.Scale; set => TextState.Scale = value; }
    public ResizeStrategy ResizeStrategy { get => TextState.ResizeStrategy; set => TextState.ResizeStrategy = value; }
    public Vector2 TextAnchor { get => TextState.TextAnchor; set => TextState.TextAnchor = value; }
    public SpriteEffects SpriteEffects { get => TextState.SpriteEffects; set => TextState.SpriteEffects = value; }
    public float LayerDepth { get => TextState.LayerDepth; set => TextState.LayerDepth = value; }
    public CUIFont Font { get => TextState.Font; set => TextState.Font = value; }

    public string RealText => TextState.RealText;
    public Vector2 RawTextSize => TextState.RawTextSize;
    public Vector2 TextDrawPosition => TextState.TextDrawPosition;
    public CUINullVector2 ForcedSize => TextState.ForcedSize;
    public float RealScale => TextState.RealScale;
    #endregion

    public CUIButton() : base()
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
    public CUIButton(string text) : this()
    {
      Text = text;
    }
  }
}