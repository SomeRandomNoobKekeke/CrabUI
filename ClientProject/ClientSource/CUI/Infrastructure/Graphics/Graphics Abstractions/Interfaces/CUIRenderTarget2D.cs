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
  public interface CUIRenderTarget2D : CUITexture2D
  {
    public static CUIRenderTarget2D Create(int width, int height)
      => new __CUIRenderTarget2D(width, height);
  }

}