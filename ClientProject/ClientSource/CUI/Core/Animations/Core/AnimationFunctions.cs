using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

namespace CrabUI
{
  /// <summary>
  /// Bank of animation functions.  
  /// https://easings.net/
  /// </summary>
  public static class AnimationFunctions
  {
    public static double Linear(double x) => x;
    public static double EasyIn(double x) => x * x * x;
    public static double EasyOut(double x) => (double)(1 - Math.Pow(1 - x, 3));
    public static double EasyInOut(double x) => (double)(x < 0.5 ? 4 * x * x * x : 1 - Math.Pow(-2 * x + 2, 3) / 2);

    public static Func<double, double> Parse(string raw)
    {
      return raw switch
      {
        nameof(Linear) => Linear,
        nameof(EasyIn) => EasyIn,
        nameof(EasyOut) => EasyOut,
        nameof(EasyInOut) => EasyInOut,
      };
    }
  }
}