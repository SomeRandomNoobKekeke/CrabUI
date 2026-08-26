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
      [typeof(Color)] = (o) => ColorToString((Color)o),
      [typeof(Vector2)] = (o) => Vector2ToString((Vector2)o),
      [typeof(__CUITexture2D)] = (o) => ((CUITexture2D)o).Key,
      [typeof((int, int))] = (o) => Tupple2IntIntToString(((int, int))o),
    };

    public static string Tupple2IntIntToString((int, int) tupple) => $"[{tupple.Item1},{tupple.Item2}]";
    public static string ColorToString(Color cl) => $"{cl.R},{cl.G},{cl.B},{cl.A}";
    public static string Vector2ToString(Vector2 v) => $"[{v.X},{v.Y}]";
  }
}
