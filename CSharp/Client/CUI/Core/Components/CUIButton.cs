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

    public Color MouseOverColor { get; set; } = new Color(0, 0, 140);
    public Color MousePressedColor { get; set; } = new Color(0, 0, 200);
    public Color InactiveColor { get; set; } = new Color(0, 0, 100);

    public Color MasterColor
    {
      set
      {
        InactiveColor = value.Multiply(0.7f);
        MouseOverColor = value.Multiply(0.9f);
        MousePressedColor = value;
        DetermineColor();
      }
    }

    public Color MasterColorOpaque
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

    public void DetermineColor()
    {
      BackgroundColor = InactiveColor;
      if (MouseOver) BackgroundColor = MouseOverColor;
      if (MousePressed) BackgroundColor = MousePressedColor;
    }

    public CUIButton() : base()
    {
      MouseOff += (e) => DetermineColor();
      MouseOn += (e) => DetermineColor();
      DetermineColor();
    }

    public override void UpdateRect(CUIRect rect)
    {
      base.UpdateRect(rect);
      TextBlock.Rect = rect;
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      yield return new VisualUnit.PrimitiveVisualElement(Background);
      yield return new VisualUnit.PrimitiveVisualElement(TextBlock);
      yield return new VisualUnit.LeftContextBound();
      foreach (CUIComponent child in Tree.Children)
      {
        yield return new VisualUnit.NestedVisualComponent(child);
      }
      yield return new VisualUnit.RightContextBound();
    }

  }
}