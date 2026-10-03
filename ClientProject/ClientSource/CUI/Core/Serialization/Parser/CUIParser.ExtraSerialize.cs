using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.IO;

namespace CursedUI
{
  public partial class CUIParser
  {
    public static Dictionary<Type, Func<object, string>> ExtraSerializeMethods { get; } = new()
    {
      [typeof(Vector2)] = (o) => Vector2ToString((Vector2)o),
      [typeof(Vector2?)] = (o) => NullVector2ToString((Vector2?)o),
      [typeof(Rectangle)] = (o) => RectangleToString((Rectangle)o),
      [typeof(Rectangle?)] = (o) => NullRectangleToString((Rectangle?)o),
      [typeof(Color)] = (o) => ColorToString((Color)o),
      [typeof(__CUITexture2D)] = (o) => ((CUITexture2D)o).Key,
      [typeof((int, int))] = (o) => Tupple2IntIntToString(((int, int))o),
      [typeof(IEnumerable<string>)] = (o) => IEnumerable_StringToString((IEnumerable<string>)o),
    };

    public static string IEnumerable_StringToString(IEnumerable<string> items)
    {
      CUI.Logger.Point();
      return $"[{string.Join(';', items)}]";
    }

    public static string Tupple2IntIntToString((int, int) tupple) => $"[{tupple.Item1},{tupple.Item2}]";
    public static string ColorToString(Color cl) => $"{cl.R},{cl.G},{cl.B},{cl.A}";
    public static string Vector2ToString(Vector2 v) => $"[{v.X},{v.Y}]";
    public static string NullVector2ToString(Vector2? v) => v.HasValue ?
      $"[{v.Value.X},{v.Value.Y}]" : "";

    public static string RectangleToString(Rectangle rect)
      => $"[{rect.X},{rect.Y},{rect.Width},{rect.Height}]";

    public static string NullRectangleToString(Rectangle? rect)
      => rect.HasValue ?
          $"[{rect.Value.X},{rect.Value.Y},{rect.Value.Width},{rect.Value.Height}]" :
          "";

  }
}
