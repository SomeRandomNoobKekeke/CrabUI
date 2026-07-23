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
  }
}