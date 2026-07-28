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
  public partial class __CUISpriteBatch : CUISpriteBatch
  {
    public void Draw(CUITexture2D texture, VertexPositionColorTexture lt, VertexPositionColorTexture rt, VertexPositionColorTexture rb, VertexPositionColorTexture lb)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, lt, rt, rb, lb);
      }
    }

    public void Draw(CUITexture2D texture, VertexPositionColorTexture[] vertices, float layerDepth, int? count = null)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, vertices, layerDepth, count);
      }
    }

    public void Draw(CUITexture2D texture, Rectangle destinationRectangle, Color color)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, destinationRectangle, color);
      }
    }

    public void Draw(CUITexture2D texture, Vector2 position, Color color)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, position, color);
      }
    }

    public void Draw(CUITexture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, position, sourceRectangle, color);
      }
    }

    public void Draw(CUITexture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, destinationRectangle, sourceRectangle, color);
      }
    }

    public void Draw(CUITexture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
      }
    }

    public void Draw(CUITexture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
      }
    }

    public void Draw(CUITexture2D texture, Vector2? position = null, Rectangle? destinationRectangle = null, Rectangle? sourceRectangle = null, Vector2? origin = null, float rotation = 0, Vector2? scale = null, Color? color = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, position, destinationRectangle, sourceRectangle, origin, rotation, scale, color, effects, layerDepth); // Soon (tm)
      }
    }

    public void Draw(CUITexture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
      }
    }
  }
}