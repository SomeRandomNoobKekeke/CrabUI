using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using CUILibs;
namespace CursedUI
{
  public partial record CUISprite
  {
    private CUITexture2D _Texture; public CUITexture2D Texture
    {
      get => _Texture;
      set
      {
        _Texture = value;
        UpdateDataBuffer();
      }
    }
    public Rectangle? SourceRectangle { get; set; } = null;

    public Point Size => SourceRectangle.HasValue ? SourceRectangle.Value.Size : Texture.Bounds.Size;

    private static Color DefaultColor => Color.White;
    public Color ColorTL { get; set; } = DefaultColor;
    public Color ColorTR { get; set; } = DefaultColor;
    public Color ColorBR { get; set; } = DefaultColor;
    public Color ColorBL { get; set; } = DefaultColor;
    public Color Color
    {
      get => ColorTL;
      set
      {
        ColorTL = value;
        ColorTR = value;
        ColorBR = value;
        ColorBL = value;
      }
    }

    public float Rotation { get; set; } = 0.0f;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0.0f;


    public void Draw(CUISpriteBatch spriteBatch, Rectangle destinationRectangle)
    {
      spriteBatch.Draw(Texture, destinationRectangle, SourceRectangle, ColorTL, ColorTR, ColorBR, ColorBL, Rotation, Origin, Effects, LayerDepth);
    }

    public CUISprite() { Texture = CUITexture2D.White; }
    public CUISprite(CUITexture2D texture) { Texture = texture; }
  }
}