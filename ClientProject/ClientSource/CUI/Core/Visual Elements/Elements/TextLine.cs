using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUILibs;

namespace CrabUI
{
  /// <summary>
  /// This is just raw unleashed line of text
  /// </summary>
  public class TextLine : VisualElementBase
  {
    public CUIRect Rect
    {
      get => new CUIRect(Position, Font.MeasureString(Text));
      set => Position = value.Position;
    }

    public override bool Contains(Vector2 pos) => Rect.Contains(pos);

    public string Text { get; set; } = "";
    public Vector2 Position { get; set; }
    public Color TextColor { get; set; } = Color.White;
    public float Rotation { get; set; } = 0f;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public float Scale { get; set; } = 1.0f;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0.1f;
    public Alignment Alignment { get; set; } = Alignment.TopLeft;
    public ForceUpperCase ForceUpperCase { get; set; } = ForceUpperCase.Inherit;

    public CUIFont Font { get; set; } = CUIFont.Font;



    public override void Draw(CUISpriteBatch spriteBatch)
    {
      if (Visible)
      {
        Font.DrawString(
          spriteBatch,
          Text,
          Position,
          TextColor,
          Rotation,
          Origin,
          Scale,
          SpriteEffects,
          LayerDepth,
          Alignment,
          ForceUpperCase
        );
      }
    }
  }
}