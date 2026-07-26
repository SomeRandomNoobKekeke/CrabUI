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
  public class __CUISpriteBatch : CUISpriteBatch
  {
    public static __CUISpriteBatch Create() => new __CUISpriteBatch(
      new SpriteBatch(GameMain.Instance.GraphicsDevice)
    );

    public SpriteBatch XNASpriteBatch { get; set; }
    public void StopStart(Rectangle scissorRect, SamplerState samplerState)
    {
      XNASpriteBatch.End();
      XNASpriteBatch.GraphicsDevice.ScissorRectangle = scissorRect;
      XNASpriteBatch.Begin(
        SpriteSortMode.Deferred,
        samplerState: samplerState,
        rasterizerState: GameMain.ScissorTestEnable
      );
    }

    public void Draw(CUITexture2D texture, Rectangle destinationRectangle, Color color)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, destinationRectangle, color);
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

    public void Draw(
      CUITexture2D texture,
      VertexPositionColorTexture lt,
      VertexPositionColorTexture rt,
      VertexPositionColorTexture rb,
      VertexPositionColorTexture lb
    )
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, lt, rt, rb, lb);
      }
    }

    public void Begin(
      SpriteSortMode sortMode = SpriteSortMode.Deferred,
      BlendState blendState = null,
      SamplerState samplerState = null,
      DepthStencilState depthStencilState = null,
      RasterizerState rasterizerState = null,
      Effect effect = null,
      Matrix? transformMatrix = null
    ) => XNASpriteBatch.Begin(
      sortMode,
      blendState,
      samplerState,
      depthStencilState,
      rasterizerState,
      effect,
      transformMatrix
    );

    public void End() => XNASpriteBatch.End();



    public __CUISpriteBatch() { }
    public __CUISpriteBatch(SpriteBatch spriteBatch) => XNASpriteBatch = spriteBatch;
  }
}