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

    public void Draw(CUITexture2D texture, Rectangle destinationRectangle, Color color);
    public Rectangle ScissorRect { get; }
    public void StopStart(Rectangle ScissorRect);

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