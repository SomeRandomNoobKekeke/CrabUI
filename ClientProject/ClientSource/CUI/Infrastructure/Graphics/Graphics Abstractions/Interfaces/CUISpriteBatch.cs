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
  public interface CUISpriteBatch
  {
    //TODO there should be an abstract factory for this
    public static CUISpriteBatch Create() => __CUISpriteBatch.Create();

    //Note: these are mine
    public void Draw(
      CUITexture2D texture,
      VertexPositionColorTexture lt,
      VertexPositionColorTexture rt,
      VertexPositionColorTexture rb,
      VertexPositionColorTexture lb
    );

    public void Draw(
      CUITexture2D texture,
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
    );

    //This one is BaroDevish
    public void Draw(
      CUITexture2D texture,
      VertexPositionColorTexture[] vertices,
      float layerDepth,
      int? count = null
    );

    //These seems to be vanilla
    public void Draw(
      CUITexture2D texture,
      Rectangle destinationRectangle,
      Color color
    );

    public void Draw(
      CUITexture2D texture,
      Vector2 position,
      Color color
    );

    public void Draw(
      CUITexture2D texture,
      Vector2 position,
      Rectangle? sourceRectangle,
      Color color
    );

    public void Draw(
      CUITexture2D texture,
      Rectangle destinationRectangle,
      Rectangle? sourceRectangle,
      Color color
    );

    public void Draw(
      CUITexture2D texture,
      Rectangle destinationRectangle,
      Rectangle? sourceRectangle,
      Color color,
      float rotation,
      Vector2 origin,
      SpriteEffects effects,
      float layerDepth
    );

    public void Draw(
      CUITexture2D texture,
      Vector2 position,
      Rectangle? sourceRectangle,
      Color color,
      float rotation,
      Vector2 origin,
      float scale,
      SpriteEffects effects,
      float layerDepth
    );

    public void Draw(
      CUITexture2D texture,
      Vector2? position = null,
      Rectangle? destinationRectangle = null,
      Rectangle? sourceRectangle = null,
      Vector2? origin = null,
      float rotation = 0f,
      Vector2? scale = null,
      Color? color = null,
      SpriteEffects effects = SpriteEffects.None,
      float layerDepth = 0f
    );

    public void Draw(
      CUITexture2D texture,
      Vector2 position,
      Rectangle? sourceRectangle,
      Color color,
      float rotation,
      Vector2 origin,
      Vector2 scale,
      SpriteEffects effects,
      float layerDepth
    );




    public void StopStart(Rectangle scissorRect,
      SpriteSortMode sortMode = SpriteSortMode.Deferred,
      BlendState blendState = null,
      SamplerState samplerState = null,
      DepthStencilState depthStencilState = null,
      RasterizerState rasterizerState = null,
      Effect effect = null,
      Matrix? transformMatrix = null
    );

    public void Begin(
      SpriteSortMode sortMode = SpriteSortMode.Deferred,
      BlendState blendState = null,
      SamplerState samplerState = null,
      DepthStencilState depthStencilState = null,
      RasterizerState rasterizerState = null,
      Effect effect = null,
      Matrix? transformMatrix = null
    );

    public void End();
  }

}