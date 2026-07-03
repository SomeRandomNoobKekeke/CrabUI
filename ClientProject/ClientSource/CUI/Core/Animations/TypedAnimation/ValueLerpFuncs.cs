using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using Microsoft.Xna.Framework;


namespace CrabUI
{
  public static class ValueLerpFuncs
  {
    public static Func<T, T, double, T> Get<T>()
    {
      if (Mapping.ContainsKey(typeof(T)))
      {
        return (Func<T, T, double, T>)Mapping[typeof(T)];
      }

      CUI.Logger.Warning($"ValueLerpFuncs doesn't have [{typeof(T).Name}] func, backing to default..");
      return Default<T>;
    }
    public static Dictionary<Type, Delegate> Mapping { get; } = new()
    {
      [typeof(int)] = Int,
      [typeof(float)] = Float,
      [typeof(Color)] = Color,
    };

    public static T Default<T>(T value1, T value2, double lambda)
      => lambda < 0.5 ? value1 : value2;

    public static float Float(float value1, float value2, double lambda)
      => (float)(value1 + (value2 - value1) * lambda);

    public static int Int(int value1, int value2, double lambda)
      => (int)Math.Round(value1 + (value2 - value1) * lambda);

    public static Color Color(Color value1, Color value2, double lambda)
      => Microsoft.Xna.Framework.Color.Lerp(value1, value2, (float)lambda);
  }
}