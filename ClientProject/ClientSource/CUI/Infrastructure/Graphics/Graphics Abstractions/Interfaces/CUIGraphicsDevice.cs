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
  public interface CUIGraphicsDevice
  {
    public void SetRenderTarget(CUIRenderTarget2D target);
    public void GetBackBufferData(Color[] buffer);

    public int BackBufferWidth { get; }
    public int BackBufferHeight { get; }
    public SurfaceFormat BackBufferFormat { get; }

    public Rectangle ScissorRect { get; }
  }

}