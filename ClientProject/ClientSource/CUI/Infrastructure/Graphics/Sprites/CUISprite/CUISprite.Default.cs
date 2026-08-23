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
    public static CUISprite AtPos(int x, int y, int w = 1, int h = 1)
      => new CUISprite(CUICore.TextureManager.Get("CUI"))
      {
        SourceRectangle = new Rectangle(1 + 66 * x, 1 + 66 * y, 64 * w, 64 * h)
      };

    public static CUISprite Cross => AtPos(0, 0);
    public static CUISprite Angle => AtPos(1, 0);

    public static CUISprite HorizontalGradient => AtPos(7, 6, 8, 1);
    public static CUISprite VerticalGradient => AtPos(6, 7, 1, 8);
    public static CUISprite BoxWithAShadow => AtPos(4, 1);

    public static CUISprite BluredEdges => AtPos(0, 2);
    public static CUISprite BluredEdgesHorizontal => AtPos(1, 2);
    public static CUISprite BluredEdgesVertical => AtPos(2, 2);
    public static CUISprite Window => AtPos(3, 2);
    public static CUISprite BoxWithALamp => AtPos(4, 2);

    public static CUISprite Vignette => AtPos(0, 3);
    public static CUISprite DimmedHorizontal => AtPos(1, 3);
    public static CUISprite DimmedVertical => AtPos(2, 3);
    public static CUISprite VignetteLight => AtPos(7, 7, 8, 8);
    public static CUISprite DimmedHorizontalLight => AtPos(4, 3);
    public static CUISprite DimmedVerticalLight => AtPos(5, 3);


    public static CUISprite LeftLineEnd => AtPos(0, 4);
    public static CUISprite LineCenter => AtPos(1, 4);
    public static CUISprite RightLineEnd => AtPos(2, 4);
    public static CUISprite Handle => AtPos(3, 4);
    public static CUISprite LineMark => AtPos(4, 4);


    public static CUISprite CreateRadialColorPicker(int w, int h)
    {
      TextureBuilder tb = new TextureBuilder(w, h);

      Vector2 center = new Vector2(0.5f, 0.5f);

      tb.Fill((i, j) =>
      {
        Vector2 v = new Vector2(((float)i) / ((float)w), ((float)j) / ((float)h));
        Vector2 d = v - center;
        float l = d.Length();

        double a = Math.Atan2(d.Y, d.X) * 180.0 / Math.PI;
        if (a < 0) a += 360;

        return CUIColor.FromHSV((float)a, 1, 1 - l * 2.0f);
      });


      return new CUISprite(tb.Build(tracked: true, key: $"RadialColorPicker[{w},{h}]"));
    }

    public static CUISprite CreateHSVColorPicker(int w, int h)
    {
      TextureBuilder tb = new TextureBuilder(w, h);

      Vector2 center = new Vector2(0.5f, 0.5f);

      tb.Fill((i, j) =>
      {
        Vector2 v = new Vector2(((float)i) / ((float)w), ((float)j) / ((float)h));
        Vector2 d = v - center;
        float l = d.Length();

        double a = Math.Atan2(d.Y, d.X) * 180.0 / Math.PI;
        if (a < 0) a += 360;

        return CUIColor.FromHSV((float)a, 1, 1 - l * 2.0f);
      });


      return new CUISprite(tb.Build(tracked: true, key: $"RadialColorPicker[{w},{h}]"));
    }
  }
}