using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursedUI
{
  public static class SpriteBatch_Extensions
  {
    public static void Draw(this SpriteBatch spriteBatch,
      Texture2D texture,
      VertexPositionColorTexture lt,
      VertexPositionColorTexture rt,
      VertexPositionColorTexture rb,
      VertexPositionColorTexture lb
    )
    {
      spriteBatch.CheckValid(texture);

      var item = spriteBatch._batcher.CreateBatchItem();
      item.Texture = texture;
      item.Effect = spriteBatch._effect;

      item.SortKey = 0f;

      item.vertexTL = lt;
      item.vertexTR = rt;
      item.vertexBR = rb;
      item.vertexBL = lb;
    }

    /// <summary>
    /// https://github.com/FakeFishGames/Barotrauma/blob/a589d2cee3ff2214c99a7ea30c46f16a5406a01d/Libraries/MonoGame.Framework/Src/MonoGame.Framework/Graphics/SpriteBatch.cs#L297
    /// I just splitted color into colorTL,colorTR,colorBR,colorBL
    /// </summary>
    public static void Draw(this SpriteBatch _,
      Texture2D texture,
      Rectangle destinationRectangle,
      Rectangle? sourceRectangle,
      Color colorTL,
      Color colorTR,
      Color colorBR,
      Color colorBL,
      float rotation,
      Vector2 origin,
      SpriteEffects effects,
      float layerDepth
    )
    {
      _.CheckValid(texture);

      var item = _._batcher.CreateBatchItem();
      item.Texture = texture;
      item.Effect = _._effect;

      // set SortKey based on SpriteSortMode.
      switch (_._sortMode)
      {
        // Comparison of Texture objects.
        case SpriteSortMode.Texture:
          item.SortKey = texture.SortingKey;
          break;
        // Comparison of Depth
        case SpriteSortMode.FrontToBack:
          item.SortKey = layerDepth;
          break;
        // Comparison of Depth in reverse
        case SpriteSortMode.BackToFront:
          item.SortKey = -layerDepth;
          break;
      }

      if (sourceRectangle.HasValue)
      {
        var srcRect = sourceRectangle.GetValueOrDefault();
        _._texCoordTL.X = srcRect.X * texture.TexelWidth;
        _._texCoordTL.Y = srcRect.Y * texture.TexelHeight;
        _._texCoordBR.X = (srcRect.X + srcRect.Width) * texture.TexelWidth;
        _._texCoordBR.Y = (srcRect.Y + srcRect.Height) * texture.TexelHeight;

        if (srcRect.Width != 0)
          origin.X = origin.X * (float)destinationRectangle.Width / (float)srcRect.Width;
        else
          origin.X = origin.X * (float)destinationRectangle.Width * texture.TexelWidth;
        if (srcRect.Height != 0)
          origin.Y = origin.Y * (float)destinationRectangle.Height / (float)srcRect.Height;
        else
          origin.Y = origin.Y * (float)destinationRectangle.Height * texture.TexelHeight;
      }
      else
      {
        _._texCoordTL = Vector2.Zero;
        _._texCoordBR = Vector2.One;

        origin.X = origin.X * (float)destinationRectangle.Width * texture.TexelWidth;
        origin.Y = origin.Y * (float)destinationRectangle.Height * texture.TexelHeight;
      }

      if ((effects & SpriteEffects.FlipVertically) != 0)
      {
        var temp = _._texCoordBR.Y;
        _._texCoordBR.Y = _._texCoordTL.Y;
        _._texCoordTL.Y = temp;
      }
      if ((effects & SpriteEffects.FlipHorizontally) != 0)
      {
        var temp = _._texCoordBR.X;
        _._texCoordBR.X = _._texCoordTL.X;
        _._texCoordTL.X = temp;
      }

      if (rotation == 0f)
      {
        item.Set(destinationRectangle.X - origin.X,
                destinationRectangle.Y - origin.Y,
                destinationRectangle.Width,
                destinationRectangle.Height,
                colorTL, colorTR, colorBR, colorBL,
                _._texCoordTL,
                _._texCoordBR,
                layerDepth);
      }
      else
      {
        item.Set(destinationRectangle.X,
                destinationRectangle.Y,
                -origin.X,
                -origin.Y,
                destinationRectangle.Width,
                destinationRectangle.Height,
                (float)Math.Sin(rotation),
                (float)Math.Cos(rotation),
                colorTL, colorTR, colorBR, colorBL,
                _._texCoordTL,
                _._texCoordBR,
                layerDepth);
      }

      _.FlushIfNeeded();
    }



    /// <summary>
    /// https://github.com/FakeFishGames/Barotrauma/blob/a589d2cee3ff2214c99a7ea30c46f16a5406a01d/Libraries/MonoGame.Framework/Src/MonoGame.Framework/Graphics/SpriteBatchItem.cs#L9
    /// I just splitted color into colorTL,colorTR,colorBR,colorBL
    /// Also it's private coz idk how to bypass inconsistent accessibility
    /// </summary>
    private static void Set(this SpriteBatchItem _, float x, float y, float dx, float dy, float w, float h, float sin, float cos, Color colorTL, Color colorTR, Color colorBR, Color colorBL, Vector2 texCoordTL, Vector2 texCoordBR, float depth)
    {
      _.vertexTL.Position.X = x + dx * cos - dy * sin;
      _.vertexTL.Position.Y = y + dx * sin + dy * cos;
      _.vertexTL.Position.Z = depth;
      _.vertexTL.Color = colorTL;
      _.vertexTL.TextureCoordinate.X = texCoordTL.X;
      _.vertexTL.TextureCoordinate.Y = texCoordTL.Y;

      _.vertexTR.Position.X = x + (dx + w) * cos - dy * sin;
      _.vertexTR.Position.Y = y + (dx + w) * sin + dy * cos;
      _.vertexTR.Position.Z = depth;
      _.vertexTR.Color = colorTR;
      _.vertexTR.TextureCoordinate.X = texCoordBR.X;
      _.vertexTR.TextureCoordinate.Y = texCoordTL.Y;

      _.vertexBL.Position.X = x + dx * cos - (dy + h) * sin;
      _.vertexBL.Position.Y = y + dx * sin + (dy + h) * cos;
      _.vertexBL.Position.Z = depth;
      _.vertexBL.Color = colorBL;
      _.vertexBL.TextureCoordinate.X = texCoordTL.X;
      _.vertexBL.TextureCoordinate.Y = texCoordBR.Y;

      _.vertexBR.Position.X = x + (dx + w) * cos - (dy + h) * sin;
      _.vertexBR.Position.Y = y + (dx + w) * sin + (dy + h) * cos;
      _.vertexBR.Position.Z = depth;
      _.vertexBR.Color = colorBR;
      _.vertexBR.TextureCoordinate.X = texCoordBR.X;
      _.vertexBR.TextureCoordinate.Y = texCoordBR.Y;
    }

    private static void Set(this SpriteBatchItem _, float x, float y, float w, float h, Color colorTL, Color colorTR, Color colorBR, Color colorBL, Vector2 texCoordTL, Vector2 texCoordBR, float depth)
    {
      _.vertexTL.Position.X = x;
      _.vertexTL.Position.Y = y;
      _.vertexTL.Position.Z = depth;
      _.vertexTL.Color = colorTL;
      _.vertexTL.TextureCoordinate.X = texCoordTL.X;
      _.vertexTL.TextureCoordinate.Y = texCoordTL.Y;

      _.vertexTR.Position.X = x + w;
      _.vertexTR.Position.Y = y;
      _.vertexTR.Position.Z = depth;
      _.vertexTR.Color = colorTR;
      _.vertexTR.TextureCoordinate.X = texCoordBR.X;
      _.vertexTR.TextureCoordinate.Y = texCoordTL.Y;

      _.vertexBL.Position.X = x;
      _.vertexBL.Position.Y = y + h;
      _.vertexBL.Position.Z = depth;
      _.vertexBL.Color = colorBL;
      _.vertexBL.TextureCoordinate.X = texCoordTL.X;
      _.vertexBL.TextureCoordinate.Y = texCoordBR.Y;

      _.vertexBR.Position.X = x + w;
      _.vertexBR.Position.Y = y + h;
      _.vertexBR.Position.Z = depth;
      _.vertexBR.Color = colorBR;
      _.vertexBR.TextureCoordinate.X = texCoordBR.X;
      _.vertexBR.TextureCoordinate.Y = texCoordBR.Y;
    }
  }
}