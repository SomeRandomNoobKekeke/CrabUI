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
  public partial class TextBlock : VisualElementBase
  {
    public float CaretOffset(int i)
      => Font.MeasureString(Text.SubstringSafe(i)).X * Scale;

    //Evil from old cui
    public int CaretIndex(Vector2 clickPos)
    {
      if (!Rect.Contains(clickPos)) return 0;

      float x = clickPos.X - Rect.Left;
      int Aprox = (int)Math.Round(x / Font.MeasureString(Text).X * Text.Length);

      int closestCaretPos = Aprox;
      float smallestDif = Math.Abs(x - CaretOffset(Aprox));

      for (int i = Aprox - 2; i <= Aprox + 2; i++)
      {
        float dif = Math.Abs(x - CaretOffset(i));
        if (dif < smallestDif)
        {
          closestCaretPos = i;
          smallestDif = dif;
        }
      }

      return closestCaretPos;
    }

    private CUIRect _Rect; public override CUIRect Rect
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

    private ResizeStrategyBase _ResizeStrategy = ResizeStrategyBase.ResistStrategy;
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


    public override void Draw(CUISpriteBatch spriteBatch)
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