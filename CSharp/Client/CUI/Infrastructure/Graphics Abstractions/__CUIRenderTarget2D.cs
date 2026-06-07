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
  //THINK should i derive __CUIRenderTarget2D from __CUITexture2D?
  public class __CUIRenderTarget2D : CUIRenderTarget2D
  {
    public static __CUIRenderTarget2D Create(int width, int height)
      => new __CUIRenderTarget2D(width, height);

    public RenderTarget2D XNARenderTarget2D { get; set; }

    public void SetData(Color[] data) => XNARenderTarget2D.SetData<Color>(data);
    public int Width => XNARenderTarget2D.Width;
    public int Height => XNARenderTarget2D.Height;

    public __CUIRenderTarget2D(RenderTarget2D target) => XNARenderTarget2D = target;
    public __CUIRenderTarget2D(int width, int height)
    {
      XNARenderTarget2D = new RenderTarget2D(GameMain.Instance.GraphicsDevice, width, height);
    }

    public void Dispose()
    {
      XNARenderTarget2D.Dispose();
    }
  }
}