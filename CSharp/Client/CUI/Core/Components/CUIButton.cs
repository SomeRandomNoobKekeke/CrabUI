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

namespace CrabUI
{
  public partial class CUIButton : CUIComponent, IComponent
  {
    public ICUIStyle HoveredStyle = new CUICodeStyle<CUIButton>() { ApplyAction = ApplyHoveredStyle };
    public ICUIStyle MouseDownStyle = new CUICodeStyle<CUIButton>() { ApplyAction = ApplyMouseDownStyle };
    public ICUIStyle MouseUpStyle = new CUICodeStyle<CUIButton>() { ApplyAction = ApplyMouseUpStyle };

    public static void ApplyHoveredStyle(CUIButton button)
    {
      button.Background.Color = Color.Yellow;
    }
    public static void ApplyMouseDownStyle(CUIButton button)
    {
      button.Background.Color = button.MouseDownColor;
    }
    public static void ApplyMouseUpStyle(CUIButton button)
    {
      button.Background.Color = button.PassiveColor;
    }

    public Color MouseHoverColor { get; set; } = new Color(0, 0, 128);
    public Color MouseDownColor { get; set; } = new Color(0, 0, 180);
    public Color PassiveColor { get; set; } = new Color(0, 0, 100);


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


    public CUIButton() : base()
    {

      MouseDown += (e) => MouseDownStyle.Apply(this);
      MouseUp += (e) => MouseUpStyle.Apply(this);
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