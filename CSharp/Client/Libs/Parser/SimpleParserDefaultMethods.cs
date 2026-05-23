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

namespace BaroJunk
{
  public class SimpleParserDefaultMethods
  {
    public static Dictionary<Type, Func<string, object>> Parse { get; set; } = new()
    {
      [typeof(Vector2)] = (raw) => ParseVector2(raw),
      [typeof(Color)] = (raw) => ParseColor(raw),
    };
    public static Dictionary<Type, Func<object, string>> Serialize { get; set; } = new()
    {
      [typeof(Vector2)] = (o) => Vector2ToString((Vector2)o),
      [typeof(Color)] = (o) => ColorToString((Color)o),
    };

    public static Color ParseColor(string raw) => XMLExtensions.ParseColor(raw);
    public static Vector2 ParseVector2(string raw)
    {
      if (raw == null || raw == "") return new Vector2(0, 0);

      string content = raw.Split('[', ']')[1];

      List<string> coords = content.Split(',').Select(s => s.Trim()).ToList();

      float x = 0;
      float y = 0;

      float.TryParse(coords.ElementAtOrDefault(0), out x);
      float.TryParse(coords.ElementAtOrDefault(1), out y);

      return new Vector2(x, y);
    }

    public static string ColorToString(Color cl) => XMLExtensions.ColorToString(cl);
    public static string Vector2ToString(Vector2 v) => $"[{v.X},{v.Y}]";
  }
}
