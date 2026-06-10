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
  public class __CUIGraphicsDevice : CUIGraphicsDevice
  {
    public int BackBufferWidth => GameMain.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth;
    public int BackBufferHeight => GameMain.Instance.GraphicsDevice.PresentationParameters.BackBufferHeight;
    public SurfaceFormat BackBufferFormat => GameMain.Instance.GraphicsDevice.PresentationParameters.BackBufferFormat;

    public Rectangle ScissorRect => GameMain.Instance.GraphicsDevice.ScissorRectangle;

    public void GetBackBufferData(Color[] buffer)
    {
      GameMain.Instance.GraphicsDevice.GetBackBufferData<Color>(buffer);
    }

    public void SetRenderTarget(CUIRenderTarget2D target)
    {
      if (target is __CUIRenderTarget2D)
      {
        GameMain.Instance.GraphicsDevice.SetRenderTarget(
          ((__CUIRenderTarget2D)target).XNARenderTarget2D
        );
      }
    }
  }
}