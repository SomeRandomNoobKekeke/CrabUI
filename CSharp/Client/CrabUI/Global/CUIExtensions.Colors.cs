using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;
using System.Globalization;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;

namespace CrabUI
{
  // [CUIInternal]
  public static partial class CUIExtensions
  {
    public static Color RandomColor() => new Color(CUI.Random.Next(256), CUI.Random.Next(256), CUI.Random.Next(256));

    public static Color GrayScale(int v) => new Color(v, v, v);

    public static Color Mult(this Color cl, float f) => new Color((int)(cl.R * f), (int)(cl.G * f), (int)(cl.B * f), cl.A);
    public static Color To(this Color colorA, Color colorB, float f) => ToolBox.GradientLerp(f, new Color[] { colorA, colorB });

    public static Dictionary<string, Color> GetShades(Color colorA, Color? colorB = null)
    {
      Color clB = colorB ?? Color.Black;

      Dictionary<string, Color> shades = new();

      float steps = 6.0f;

      shades["0"] = colorA.To(clB, 0.0f / steps);
      shades["1"] = colorA.To(clB, 1.0f / steps);
      shades["2"] = colorA.To(clB, 2.0f / steps);
      shades["3"] = colorA.To(clB, 3.0f / steps);
      shades["4"] = colorA.To(clB, 4.0f / steps);
      shades["5"] = colorA.To(clB, 5.0f / steps);
      shades["6"] = colorA.To(clB, 6.0f / steps);

      return shades;
    }

    public static void GeneratePaletteFromColors(Color colorA, Color colorB)
    {

    }

  }
}