using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Barotrauma;

namespace CrabUI
{
  public static class CUIColor
  {
    // https://en.wikipedia.org/wiki/Alpha_compositing
    public static Color Over(this Color top, Color bottom)
    {
      float TopAlpha = top.A / 255.0f;
      float BottomAlpha = bottom.A / 255.0f;

      float invTopAlpha = 1 - TopAlpha;

      float ResultAlpha = TopAlpha + BottomAlpha * invTopAlpha;

      return new Color(
        (byte)((top.R * TopAlpha + bottom.R * BottomAlpha * invTopAlpha) / ResultAlpha),
        (byte)((top.G * TopAlpha + bottom.G * BottomAlpha * invTopAlpha) / ResultAlpha),
        (byte)((top.B * TopAlpha + bottom.B * BottomAlpha * invTopAlpha) / ResultAlpha),
        (byte)ResultAlpha * 255
      );
    }

    /// <summary>
    /// It simply uses mask alpha
    /// </summary>
    public static Color Mask(this Color mask, Color original)
    {
      return new Color(
        original.R,
        original.G,
        original.B,
        (int)(original.A * (mask.A / 255.0f))
      );
    }

    public static Color InvertMask(this Color mask, Color original)
    {
      return new Color(
        original.R,
        original.G,
        original.B,
        (int)(original.A * (1 - mask.A / 255.0f))
      );
    }

    public static Color MultOpaque(this Color color, float l)
      => new Color(
        (int)(color.R * l),
        (int)(color.G * l),
        (int)(color.B * l),
        (int)color.A
      );

    //FIXME that's not how it's calculated
    public static float Brightness(this Color cl)
      => Math.Clamp((cl.R + cl.G + cl.B) / 255.0f, 0.0f, 1.0f);

    public static Color Add(this Color cl, Color other)
      => new Color(cl.R, cl.G, cl.B, cl.A);



    // https://github.com/MonoGame/MonoGame/blob/ef06375d186e85db9bf554679aaf509278072823/MonoGame.Framework/Color.cs
    #region From MonoGame

    //BRUH these methods are out of sync with FromHSV methods, wtf
    // /// <summary>
    // /// Calculates Hue and Saturation value from RGB, to avoid code duplication.
    // /// </summary>
    // /// <param name="h">Hue component value from 0.0f to 360.0f</param>
    // /// <param name="s">Saturation component</param>
    // /// <param name="max">Returns the highest value of the <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values</param>
    // /// <param name="min">Returns the lowest value of the <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values</param>
    // private static void ToHS(Color _, out float h, out float s, out double max, out double min)
    // {
    //   double r = _.R / 255f;
    //   double g = _.G / 255f;
    //   double b = _.B / 255f;

    //   max = Math.Max(r, Math.Max(g, b));
    //   min = Math.Min(r, Math.Min(g, b));
    //   double delta = max - min;


    //   // calculating hue
    //   if (delta == 0f)
    //     h = 0.0f;
    //   else if (max == r)
    //     h = (float)(60.0 * (((g - b) / delta) % 6.0));
    //   else if (max == g)
    //     h = (float)(60.0 * (((b - r) / delta) + 2.0));
    //   else
    //     h = (float)((60.0 * (((r - g) / delta)) + 4.0));

    //   if (h < 0.0f)
    //     h += 360.0f;
    //   // calculating saturation
    //   s = 0.0f;
    //   if (max != 0.0)
    //     s = (float)((delta / max) * 100.0);

    // }
    // /// <summary>
    // /// Converts <see cref="Color"/> into HSL components.
    // /// </summary>
    // /// <param name="h">Hue component from 0.0f to 360.0f.</param>
    // /// <param name="s">Saturation component from 0.0f to 100.0f.</param>
    // /// <param name="l">Luminosity (or brightness) component from 0.0f to 100.0f.</param>
    // public static void ToHSL(this Color _, out float h, out float s, out float l)
    // {
    //   double max, min;
    //   ToHS(_, out h, out s, out max, out min);

    //   // luminosity
    //   l = (float)((max + min) / 2.0) * 100.0f;
    // }

    // /// <summary>
    // /// Converts <see cref="Color"/> into HSV components 
    // /// </summary>
    // /// <param name="h">Hue component value from 0.0f to 360.0f</param>
    // /// <param name="s">Saturation component value from 0.0f to 100.0f</param>
    // /// <param name="v">Value component value from 0.0f to 100.0f</param>
    // public static void ToHSV(this Color _, out float h, out float s, out float v)
    // {
    //   double max, min;
    //   ToHS(_, out h, out s, out max, out min);

    //   // value
    //   v = (float)(max * 100.0);
    // }


    // public static float GetHue(this Color _)
    // {
    //   float h, s, v;
    //   _.ToHSV(out h, out s, out v);
    //   return h;
    // }

    // public static float GetSaturation(this Color _)
    // {
    //   float h, s, v;
    //   _.ToHSV(out h, out s, out v);
    //   return s;
    // }

    // public static float GetValue(this Color _)
    // {
    //   float h, s, v;
    //   _.ToHSV(out h, out s, out v);
    //   return v;
    // }

    // public static float GetLuminocity(this Color _)
    // {
    //   float h, s, l;
    //   _.ToHSL(out h, out s, out l);
    //   return l;
    // }

    /// <summary>
    /// Converts the Hue value to either an R, G or B value
    /// </summary>
    /// <param name="c">chroma</param>
    /// <param name="x"></param>
    /// <param name="rh"></param>
    /// <returns>the hue for R, G or B</returns>
    private static float HtoRGB(float c, float x, float rh)
    {
      if ((6 * rh) < 1)
        return (c + (x - c) * 6 * rh);
      if ((2 * rh) < 1)
        return x;
      if ((3 * rh) < 2)
        return (c + (x - c) * ((2.0f / 3.0f) - rh) * 6);
      else
        return c;
    }

    /// <summary>
    /// Creates a <see cref="Color"/> from HSL values
    /// </summary>
    /// <param name="h">Hue component value, from 0.0f to 360.0f</param>
    /// <param name="s">Saturation component value, from 0.0f to 100.0f</param>
    /// <param name="l">Luminosity (brightness) component value, from 0.0f to 100.0f</param>
    /// <returns><see cref="Color"/> with the HSL values</returns>
    public static Color FromHSL(float h, float s, float l)
    {
      s /= 100;
      l /= 100;
      //converting hue to be between 1 and 0
      h /= 360.0f;
      h %= 1;

      float r, g, b; //defining values for easier colour conversion at end

      if (s == 0)
        r = g = b = l;//for greyscale values
      else
      {
        float c, x;
        if (l < 0.5)
          x = l * (1 + s);
        else
          x = (l + s) - (s * l);

        c = 2 * l - x;

        r = HtoRGB(c, x, (h + (1.0f / 3.0f)));
        g = HtoRGB(c, x, (h));
        b = HtoRGB(c, x, h - (1.0f / 3.0f));
      }


      return new Color(r, g, b);

    }

    /// <summary>
    /// Creates a <see cref="Color"/> from HSV values. 
    /// </summary>
    /// <param name="h">Hue component value, ranging from 0.0f to 360.0f</param>
    /// <param name="s">Saturation component value, ranging from 0.0f to 1.0f</param>
    /// <param name="v">Value component value, ranging from 0.0f to 1.0f</param>
    /// <returns><see cref="Color"/> with the HSV values</returns>
    public static Color FromHSV(float h, float s, float v)
    {
      //defining values for easier colour conversion at end
      float r = 0f;
      float g = 0f;
      float b = 0f;

      h %= 360.0f;
      s = MathHelper.Clamp(s, 0.0f, 1.0f);
      v = MathHelper.Clamp(v, 0.0f, 1.0f);

      if (s == 0)
        r = g = b = v;
      //working out which segment of colour wheel the hue is.
      int i = (int)(h / 60.0f);
      float f = (h % 60.0f) / 60.0f;
      float p = v * (1.0f - s);
      float q = v * (1.0f - s * f);
      float t = v * (1.0f - s * (1.0f - f));

      switch (i)
      {
        case 0:
          r = v;
          g = t;
          b = p;
          break;
        case 1:
          r = q;
          g = v;
          b = p;
          break;
        case 2:
          r = p;
          g = v;
          b = t;
          break;
        case 3:
          r = p;
          g = q;
          b = v;
          break;
        case 4:
          r = t;
          g = p;
          b = v;
          break;
        default:
          r = v;
          g = p;
          b = q;
          break;


      }

      return new Color(r, g, b);

    }
    #endregion



  }
}