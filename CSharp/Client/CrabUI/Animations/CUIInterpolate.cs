using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  /// <summary>
  /// Class containing a few interpolate functions for CUIAnimation
  /// </summary>
  public class CUIInterpolate
  {
    public static object InterpolateColor(object start, object end, double lambda)
    {
      return ((Color)start).To(((Color)end), (float)lambda);
    }

    public static object InterpolateVector2(object start, object end, double lambda)
    {
      Vector2 a = (Vector2)start;
      Vector2 b = (Vector2)end;
      return a + (b - a) * (float)lambda;
    }

    public static object InterpolateFloat(object start, object end, double lambda)
    {
      float a = (float)start;
      float b = (float)end;
      return a + (b - a) * (float)lambda;
    }

    public static Dictionary<Type, Func<object, object, double, object>> Interpolate = new();

    internal static void InitStatic()
    {
      CUI.OnInit += () =>
      {
        Interpolate[typeof(Color)] = InterpolateColor;
        Interpolate[typeof(Vector2)] = InterpolateVector2;
        Interpolate[typeof(float)] = InterpolateFloat;
      };

      CUI.OnDispose += () =>
      {
        Interpolate.Clear();
      };
    }
  }
}