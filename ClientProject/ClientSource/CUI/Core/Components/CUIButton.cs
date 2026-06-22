using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ComponentGenerator;
using Barotrauma.Extensions;

namespace CrabUI
{
  public partial class CUIButton : CUIComponent, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIButton>((c) =>
    {
      c.MasterColor = Color.Blue;
      c.ConsumeMouseClicks = true;
    });
    // public ICUIStyle HoveredStyle = new CUICodeStyle<CUIButton>() { ApplyAction = ApplyHoveredStyle };
    // public ICUIStyle MouseDownStyle = new CUICodeStyle<CUIButton>() { ApplyAction = ApplyMouseDownStyle };
    // public ICUIStyle MouseUpStyle = new CUICodeStyle<CUIButton>() { ApplyAction = ApplyMouseUpStyle };

    // public static void ApplyHoveredStyle(CUIButton button)
    // {
    //   button.Background.Color = button.MouseOverColor;
    // }
    // public static void ApplyMouseDownStyle(CUIButton button)
    // {
    //   button.Background.Color = button.MousePressedColor;
    // }
    // public static void ApplyMouseUpStyle(CUIButton button)
    // {
    //   button.Background.Color = button.InactiveColor;
    // }

    [CUISerializable]
    public Color MouseOverColor { get; set; } = new Color(0, 0, 140);
    [CUISerializable]
    public Color MousePressedColor { get; set; } = new Color(0, 0, 200);
    [CUISerializable]
    public Color InactiveColor { get; set; } = new Color(0, 0, 100);

    public Color MasterColor
    {
      set
      {
        InactiveColor = new Color((int)(value.R * 0.7f), (int)(value.G * 0.7f), (int)(value.B * 0.7f), value.A);
        MouseOverColor = new Color((int)(value.R * 0.9f), (int)(value.G * 0.9f), (int)(value.B * 0.9f), value.A);
        MousePressedColor = value;
        DetermineColor();
      }
    }


    #region TextBlock
    #endregion
    public TextBlock TextBlock { get; } = new();

    [CUISerializable]
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

    protected override CUINullVector2 MinSizeOverride => TextBlock.ForcedSize;

    public void DetermineColor()
    {
      BackgroundColor = InactiveColor;
      if (MouseOver) BackgroundColor = MouseOverColor;
      if (MousePressed) BackgroundColor = MousePressedColor;
    }



    protected override void UpdateRect(CUIRect rect)
    {
      base.UpdateRect(rect);
      TextBlock.Rect = rect;
    }

    [CUISerializable]
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


    public CUIButton() : base()
    {
      MouseOff += (c, e) => DetermineColor();
      MouseOn += (c, e) => DetermineColor();
      DetermineColor();
    }

    public CUIButton(string text) : this()
    {
      Text = text;
    }
  }
}