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
  public class __CUIRenderTarget2D : __CUITexture2D, CUIRenderTarget2D
  {
    public RenderTarget2D XNARenderTarget2D { get; }

    public __CUIRenderTarget2D(RenderTarget2D target) : base()
    {
      XNARenderTarget2D = target;
      XNATexture = target;
    }
    public __CUIRenderTarget2D(int width, int height) : this(
      new RenderTarget2D(GameMain.Instance.GraphicsDevice, width, height)
    )
    { }
  }
}