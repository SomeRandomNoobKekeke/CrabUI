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
    public static CUISprite BaroDev => new CUISprite(CUICore.TextureManager.Get("BaroDev"));

    public static CUISprite Load(string path, string key = null)
    {
      return new CUISprite(
        CUICore.TextureManager.Load(path, key)
      );
    }

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
    public Color Color { get; set; } = Color.White; // !!!
    public float Rotation { get; set; } = 0.0f;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0.0f;


    public void Draw(CUISpriteBatch spriteBatch, Rectangle destinationRectangle)
    {
      spriteBatch.Draw(Texture, destinationRectangle, SourceRectangle, Color, Rotation, Origin, Effects, LayerDepth);
    }

    //TODO how to UpdateDataBuffer if user changes data in texture manually? 
    private Color[] DataBuffer;
    public void UpdateDataBuffer()
    {
      DataBuffer = ShouldBufferData ? Texture.Data : [];
    }
    public bool _ShouldBufferData; public bool ShouldBufferData
    {
      get => _ShouldBufferData;
      set
      {
        if (ShouldBufferData == value) return;
        _ShouldBufferData = value;
        UpdateDataBuffer();
      }
    }

    public Color[] Data => ShouldBufferData ? DataBuffer : Texture.Data;



    public bool IsPointOnTransparentPixel(Vector2 point)
    {
      Rectangle SourceRect = SourceRectangle.HasValue ? SourceRectangle.Value : Texture.Bounds;

      int textureX = (int)Math.Round(SourceRect.X + point.X * SourceRect.Width);
      int textureY = (int)Math.Round(SourceRect.Y + point.Y * SourceRect.Height);

      if (textureX < SourceRect.X || (SourceRect.X + SourceRect.Width - 1) < textureX) return true;
      if (textureY < SourceRect.Y || (SourceRect.Y + SourceRect.Height - 1) < textureY) return true;

      Color cl = Data[textureY * Texture.Width + textureX];

      return cl.A == 0;
    }

    public CUISprite(CUITexture2D texture) { Texture = texture; }
  }
}