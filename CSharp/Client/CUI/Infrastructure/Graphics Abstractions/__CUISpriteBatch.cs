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
    public Rectangle ScissorRect => XNASpriteBatch.GraphicsDevice.ScissorRectangle;

    public void StopStart(Rectangle ScissorRect)
    {
      XNASpriteBatch.End();
      XNASpriteBatch.GraphicsDevice.ScissorRectangle = ScissorRect;
      XNASpriteBatch.Begin(SpriteSortMode.Deferred, rasterizerState: GameMain.ScissorTestEnable);
    }

    public void Draw(CUITexture2D texture, Rectangle destinationRectangle, Color color)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, destinationRectangle, color);
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