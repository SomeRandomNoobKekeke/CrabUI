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
  public static partial class Utils
  {
    public static Color MultOpaque(this Color color, float l)
      => new Color(
        (int)(color.R * l),
        (int)(color.G * l),
        (int)(color.B * l),
        (int)color.A
      );

    public static float Brightness(this Color cl)
      => Math.Clamp((cl.R + cl.G + cl.B) / 255.0f, 0.0f, 1.0f);

    public static Color Add(this Color cl, Color other)
      => new Color(cl.R, cl.G, cl.B, cl.A);
  }
}