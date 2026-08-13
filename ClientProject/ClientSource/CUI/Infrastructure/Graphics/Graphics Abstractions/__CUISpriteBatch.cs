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
    public static __CUISpriteBatch Create() => new __CUISpriteBatch(
      new SpriteBatch(GameMain.Instance.GraphicsDevice)
    );

    public SpriteBatch XNASpriteBatch { get; set; }
    public void StopStart(Rectangle scissorRect,
      SpriteSortMode sortMode = SpriteSortMode.Deferred,
      BlendState blendState = null,
      SamplerState samplerState = null,
      DepthStencilState depthStencilState = null,
      RasterizerState rasterizerState = null,
      Effect effect = null,
      Matrix? transformMatrix = null
    )
    {
      XNASpriteBatch.End();
      XNASpriteBatch.GraphicsDevice.ScissorRectangle = scissorRect;
      XNASpriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState ?? GameMain.ScissorTestEnable, effect, transformMatrix);
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