using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public class CUISprite
  {
    public static CUISprite White => new CUISprite(CUITexture2D.White);

    public CUITexture2D Texture { get; set; }
    public Rectangle? SourceRectangle { get; set; } = null;
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0.0f;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0.0f;


    public void Draw(CUISpriteBatch spriteBatch, Rectangle destinationRectangle)
    {
      spriteBatch.Draw(Texture, destinationRectangle, SourceRectangle, Color, Rotation, Origin, Effects, LayerDepth);
    }

    public CUISprite(CUITexture2D texture) { Texture = texture; }
  }
}