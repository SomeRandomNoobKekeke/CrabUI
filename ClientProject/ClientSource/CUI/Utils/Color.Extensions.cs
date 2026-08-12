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
  public static class Color_Extensions
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
  }
}