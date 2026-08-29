using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace CursedUI
{
  public partial record CUISprite
  {
    public static CUISprite Transparent => new CUISprite(CUITexture2D.White) { Color = Color.Transparent };
    public static CUISprite White => new CUISprite(CUITexture2D.White);
    public static CUISprite BaroDev => new CUISprite(CUITexture2D.BaroDev);

    /// <summary>
    /// 64x64 textures separated by 2px transparent lines to avoid sampler artifacts
    /// </summary>
    private static CUISprite AtIndex(int x, int y, int w = 1, int h = 1)
      => new CUISprite(CUICore.TextureManager.Get("CUI"))
      {
        SourceRectangle = new Rectangle(1 + 66 * x, 1 + 66 * y, 64 * w, 64 * h)
      };

    private static CUISprite AtPos(int x, int y, int w, int h)
      => new CUISprite(CUICore.TextureManager.Get("CUI"))
      {
        SourceRectangle = new Rectangle(x, y, w, h)
      };

    public static CUISprite Cross => AtIndex(0, 0);
    public static CUISprite Angle => AtIndex(1, 0);


    public static CUISprite CheckBoxOn => AtIndex(0, 1);
    public static CUISprite CheckBoxOff => AtIndex(1, 1);

    public static CUISprite BoxWithAShadow => AtIndex(4, 1);

    public static CUISprite GlowingEdges => AtIndex(2, 1);
    public static CUISprite BluredEdges => AtIndex(0, 2);
    public static CUISprite BluredEdgesHorizontal => AtIndex(1, 2);
    public static CUISprite BluredEdgesVertical => AtIndex(2, 2);
    public static CUISprite Window => AtIndex(3, 2);
    public static CUISprite BoxWithALamp => AtIndex(4, 2);

    public static CUISprite Vignette => AtIndex(0, 3);
    public static CUISprite DimmedHorizontal => AtIndex(1, 3);
    public static CUISprite DimmedVertical => AtIndex(2, 3);
    public static CUISprite VignetteLight => AtIndex(7, 7, 8, 8);
    public static CUISprite DimmedHorizontalLight => AtIndex(4, 3);
    public static CUISprite DimmedVerticalLight => AtIndex(5, 3);


    public static CUISprite LeftLineEnd => AtIndex(0, 4);
    public static CUISprite LineCenter => AtIndex(1, 4);
    public static CUISprite RightLineEnd => AtIndex(2, 4);
    public static CUISprite Handle => AtIndex(3, 4);
    public static CUISprite LineMark => AtIndex(4, 4);


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