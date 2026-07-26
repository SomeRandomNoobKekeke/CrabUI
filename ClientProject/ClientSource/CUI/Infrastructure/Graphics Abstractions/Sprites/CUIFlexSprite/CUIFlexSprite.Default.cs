using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using CUILibs;
namespace CrabUI
{
  public partial record CUIFlexSprite
  {
    public static CUIFlexSprite White => new CUIFlexSprite(CUITexture2D.White);
    public static CUIFlexSprite BaroDev => new CUIFlexSprite(CUITexture2D.BaroDev);

    /// <summary>
    /// 64x64 textures separated by 2px transparent lines to avoid sampler artifacts
    /// </summary>
    public static CUIFlexSprite AtPos(int x, int y)
      => new CUIFlexSprite(CUICore.TextureManager.Get("CUI"))
      {
        SourceRectangle = new Rectangle(1 + 66 * x, 1 + 66 * y, 64, 64)
      };

    public static CUIFlexSprite Cross => AtPos(0, 0);
    public static CUIFlexSprite Angle => AtPos(1, 0);

    public static CUIFlexSprite HorizontalGradient => AtPos(2, 1);
    public static CUIFlexSprite VerticalGradient => AtPos(3, 1);
    public static CUIFlexSprite BoxWithAShadow => AtPos(4, 1);

    public static CUIFlexSprite BluredEdges => AtPos(0, 2);
    public static CUIFlexSprite BluredEdgesHorizontal => AtPos(1, 2);
    public static CUIFlexSprite BluredEdgesVertical => AtPos(2, 2);
    public static CUIFlexSprite Window => AtPos(3, 2);
    public static CUIFlexSprite BoxWithALamp => AtPos(4, 2);

    public static CUIFlexSprite Vignette => AtPos(0, 3);
    public static CUIFlexSprite DimmedHorizontal => AtPos(1, 3);
    public static CUIFlexSprite DimmedVertical => AtPos(2, 3);
    public static CUIFlexSprite VignetteLight => AtPos(3, 3);
    public static CUIFlexSprite DimmedHorizontalLight => AtPos(4, 3);
    public static CUIFlexSprite DimmedVerticalLight => AtPos(5, 3);
  }
}