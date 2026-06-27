using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public class SimpleTexture : VisualElementBase
  {
    public override CUIRect Rect { get; set; }
    // public CUITexture2D Texture { get; set; } = CUITexture2D.White;

    private CUISprite _Sprite = CUISprite.White; public CUISprite Sprite
    {
      get => _Sprite;
      set
      {
        _Sprite = value;
        _Sprite.Color = _Color;
      }
    }

    //BRUH Elevated from CUISprite, do i really need to elevate props? why only color?
    private Color _Color; public Color Color
    {
      get => _Color;
      set
      {
        _Color = value;
        Sprite.Color = value;
      }
    }


    #region Forwarded to CUISprite
    public CUITexture2D Texture { get => Sprite.Texture; set => Sprite.Texture = value; }
    public Rectangle? SourceRectangle { get => Sprite.SourceRectangle; set => Sprite.SourceRectangle = value; }
    public float Rotation { get => Sprite.Rotation; set => Sprite.Rotation = value; }
    public Vector2 Origin { get => Sprite.Origin; set => Sprite.Origin = value; }
    public SpriteEffects Effects { get => Sprite.Effects; set => Sprite.Effects = value; }
    public float LayerDepth { get => Sprite.LayerDepth; set => Sprite.LayerDepth = value; }
    #endregion

    public override void Draw(CUISpriteBatch spriteBatch)
    {
      if (Visible)
      {
        Sprite.Draw(spriteBatch, Rect.Box);
      }
    }
  }
}