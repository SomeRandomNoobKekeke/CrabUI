using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Barotrauma;

namespace CursedUI
{
  public static partial class Utils
  {
    //TODO
    // public static double BoundAngle(double a)
    // {
    //   return a - Math.Floor((a) / 2.0 / Math.PI);
    // }
    public static double BoundAngle(double a)
    {
      if (a < -Math.PI)
      {
        while (a < -Math.PI) a += 2 * Math.PI;
        return a;
      }

      if (a > Math.PI)
      {
        while (a > Math.PI) a -= 2 * Math.PI;
        return a;
      }

      return a;
    }

    /// <summary>
    /// All angles should be bounded to [-Math.PI, Math.PI]
    /// </summary>
    public static bool IsAngleWithin(double a, double start, double end)
    {
      if (start <= end) return start <= a && a <= end;

      return (start <= a && a <= Math.PI) || (-Math.PI <= a && a <= end);
    }
  }
}