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
  public partial class CUISprite
  {
    public static CUISprite GetByName(string name)
    {
      PropertyInfo pi = typeof(CUISprite).GetProperty(name, BindingFlags.Public | BindingFlags.Static);
      if (pi is not null) return (CUISprite)pi.GetValue(null);
      return White;
    }

    public static CUISprite Transparent => new CUISprite(CUITexture2D.White)
    {
      // Name = "Transparent",
      Color = Color.Transparent,
    };
    public static CUISprite White => new CUISprite(CUITexture2D.White) { Name = "White" };
    public static CUISprite BaroDev => new CUISprite(CUITexture2D.BaroDev) { Name = "BaroDev" };

    public static Rectangle IndexToPos(Rectangle rect)
      => new Rectangle(1 + 66 * rect.Left, 1 + 66 * rect.Top, 64 * rect.Width, 64 * rect.Height);
    public static Rectangle IndexToPos(int x, int y, int w = 1, int h = 1)
      => new Rectangle(1 + 66 * x, 1 + 66 * y, 64 * w, 64 * h);

    //TODO i should hide these AtIndex methods and exposes all sprites as props

    /// <summary>
    /// 64x64 textures separated by 2px transparent lines to avoid sampler artifacts
    /// </summary>
    public static CUISprite AtIndex(string name, int x, int y, int w = 1, int h = 1)
      => new CUISprite(CUICore.TextureManager.Get("CUI"))
      {
        Name = name,
        SourceRectangle = IndexToPos(x, y, w, h)
      };

    public static CUISprite AtPos(string name, int x, int y, int w, int h)
      => new CUISprite(CUICore.TextureManager.Get("CUI"))
      {
        Name = name,
        SourceRectangle = new Rectangle(x, y, w, h)
      };

    public static CUISprite CutAtIndexAs(string name, int x, int y, int w = 1, int h = 1)
    {
      CUICore.TextureManager.Ensure(name,
        () => CUICore.TextureManager.Get("CUI").Cut(IndexToPos(x, y, w, h))
      );

      return new CUISprite(CUICore.TextureManager.Get(name)) { Name = name };
    }

    public static CUISprite Cross => AtIndex("Cross", 0, 0);
    public static CUISprite Angle => AtIndex("Angle", 1, 0);


    public static CUISprite CheckBoxOn => AtIndex("CheckBoxOn", 0, 1);
    public static CUISprite CheckBoxOff => AtIndex("CheckBoxOff", 1, 1);

    public static CUISprite BoxWithAShadow => AtIndex("BoxWithAShadow", 4, 1);

    public static CUISprite GlowingEdges => AtIndex("GlowingEdges", 2, 1);
    public static CUISprite BluredEdges => AtIndex("BluredEdges", 0, 2);
    public static CUISprite BluredEdgesHorizontal => AtIndex("BluredEdgesHorizontal", 1, 2);
    public static CUISprite BluredEdgesVertical => AtIndex("BluredEdgesVertical", 2, 2);
    public static CUISprite Window => AtIndex("Window", 3, 2);
    public static CUISprite BoxWithALamp => AtIndex("BoxWithALamp", 4, 2);

    public static CUISprite Vignette => AtIndex("Vignette", 0, 3);
    public static CUISprite DimmedHorizontal => AtIndex("DimmedHorizontal", 1, 3);
    public static CUISprite DimmedVertical => AtIndex("DimmedVertical", 2, 3);
    public static CUISprite VignetteDithered => AtIndex("VignetteDithered", 7, 7, 8, 8);
    public static CUISprite DimmedHorizontalLight => AtIndex("DimmedHorizontalLight", 4, 3);
    public static CUISprite DimmedVerticalLight => AtIndex("DimmedVerticalLight", 5, 3);


    public static CUISprite LeftLineEnd => AtIndex("LeftLineEnd", 0, 4);
    public static CUISprite LineCenter => AtIndex("LineCenter", 1, 4);
    public static CUISprite RightLineEnd => AtIndex("RightLineEnd", 2, 4);
    public static CUISprite Handle => AtIndex("Handle", 3, 4);
    public static CUISprite LineMark => AtIndex("LineMark", 4, 4);

    public static CUISprite FuzzyCircle => AtIndex("FuzzyCircle", 5, 4);

    public static CUISprite Hex => CutAtIndexAs("Hex", 0, 5, 2, 2);


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