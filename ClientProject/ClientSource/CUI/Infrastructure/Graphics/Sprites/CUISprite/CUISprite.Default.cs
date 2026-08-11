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
  public partial record CUISprite
  {
    public static CUISprite White => new CUISprite(CUITexture2D.White);
    public static CUISprite BaroDev => new CUISprite(CUITexture2D.BaroDev);

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

    public static CUISprite HorizontalGradient => AtPos(2, 1);
    public static CUISprite VerticalGradient => AtPos(3, 1);
    public static CUISprite BoxWithAShadow => AtPos(4, 1);

    public static CUISprite BluredEdges => AtPos(0, 2);
    public static CUISprite BluredEdgesHorizontal => AtPos(1, 2);
    public static CUISprite BluredEdgesVertical => AtPos(2, 2);
    public static CUISprite Window => AtPos(3, 2);
    public static CUISprite BoxWithALamp => AtPos(4, 2);

    public static CUISprite Vignette => AtPos(0, 3);
    public static CUISprite DimmedHorizontal => AtPos(1, 3);
    public static CUISprite DimmedVertical => AtPos(2, 3);
    public static CUISprite VignetteLight => AtPos(3, 3);
    public static CUISprite DimmedHorizontalLight => AtPos(4, 3);
    public static CUISprite DimmedVerticalLight => AtPos(5, 3);


    public static CUISprite LeftLineEnd => AtPos(0, 4);
    public static CUISprite LineCenter => AtPos(1, 4);
    public static CUISprite RightLineEnd => AtPos(2, 4);
    public static CUISprite Handle => AtPos(3, 4);
    public static CUISprite LineMark => AtPos(4, 4);
  }
}