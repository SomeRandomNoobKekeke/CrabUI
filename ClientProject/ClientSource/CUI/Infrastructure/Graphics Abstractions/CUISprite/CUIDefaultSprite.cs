using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace CrabUI
{
  /// <summary>
  /// Note: all of this is cringe, idk which ones i need, what looks good or how to name them
  /// It's volatile, don't rely on this too much
  /// </summary>
  public static class CUIDefaultSprite
  {
    /// <summary>
    /// 64x64 textures separated by 2px transparent lines to avoid sampler artifacts
    /// </summary>
    public static CUISprite AtPos(int x, int y)
      => new CUISprite(CUICore.TextureManager.Get("CUI"))
      {
        SourceRectangle = new Rectangle(1 + 66 * x, 1 + 66 * y, 64, 64)
      };

    public static CUISprite Cross => AtPos(0, 0);
    public static CUISprite Angle => AtPos(1, 0);
    public static CUISprite Vignette => AtPos(2, 0);
    public static CUISprite InnerShadow => AtPos(3, 0);
    public static CUISprite Convex => AtPos(4, 0);
    public static CUISprite Outlined => AtPos(4, 0);
    public static CUISprite HorizontalGradient => AtPos(2, 1);
    public static CUISprite VerticalGradient => AtPos(3, 1);
  }
}