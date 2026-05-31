using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaroJunk;

namespace CrabUI
{
  public partial class TextBlock : VisualElementBase, IVisualElement
  {
    private CUIRect _Rect; public CUIRect Rect
    {
      get => _Rect;
      set
      {
        _Rect = value;
        _ResizeStrategy.MeasureRealTextSize(Rect, Anchor, Text, Scale, Font);
      }
    }

    private string _Text = ""; public string Text
    {
      get => _Text;
      set
      {
        _Text = value;
        _ResizeStrategy.MeasureRawTextSize(Text, Scale, Font);
      }
    }

    private float _Scale = 1.0f; public float Scale
    {
      get => _Scale;
      set
      {
        _Scale = Math.Max(0, value);
        _ResizeStrategy.MeasureRawTextSize(Text, Scale, Font);
      }
    }

    private ResizeStrategyBase _ResizeStrategy = ResizeStrategyBase.PassiveStrategy;
    public ResizeStrategy ResizeStrategy
    {
      get => ResizeStrategyBase.ToEnum(_ResizeStrategy);
      set
      {
        _ResizeStrategy = ResizeStrategyBase.FromEnum(value);
        _ResizeStrategy.MeasureRawTextSize(Text, Scale, Font);
      }
    }

    public Vector2 Anchor { get; set; } = new Vector2(0.5f, 0.5f);


    public Color TextColor { get; set; } = Color.White;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0.1f;


    public CUIFont Font { get; set; } = CUIFont.Font;

    public string RealText => _ResizeStrategy.RealText;
    public Vector2 RawTextSize => _ResizeStrategy.RawTextSize;
    public Vector2 TextDrawPosition => _ResizeStrategy.TextDrawPosition;
    public CUINullVector2 ForcedSize => _ResizeStrategy.ForcedSize;
    public float RealScale => _ResizeStrategy.RealScale;


    public void Draw(CUISpriteBatch spriteBatch)
    {
      Font.DrawString(
        spriteBatch,
        RealText,
        TextDrawPosition,
        TextColor,
        rotation: 0,
        origin: Vector2.Zero,
        RealScale,
        SpriteEffects,
        LayerDepth
      );
    }
  }
}