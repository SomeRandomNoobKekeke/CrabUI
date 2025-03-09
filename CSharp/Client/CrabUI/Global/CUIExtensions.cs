using System;
using System.Collections.Generic;
using System.Diagnostics;
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
  [CUIInternal]
  public static partial class CUIExtensions
  {
    public static int Fit(this int i, int bottom, int top) => Math.Max(bottom, Math.Min(i, top));
    public static Vector2 Rotate(this Vector2 v, float angle) => Vector2.Transform(v, Matrix.CreateRotationZ(angle));
    public static string SubstringSafe(this string s, int start)
    {
      try
      {
        int safeStart = start.Fit(0, s.Length);
        return s.Substring(safeStart, s.Length - safeStart);
      }
      catch (Exception e)
      {
        CUI.Log($"SubstringSafe {e}");
        return "";
      }
    }
    public static string SubstringSafe(this string s, int start, int length)
    {
      int end = (start + length).Fit(0, s.Length);
      int safeStart = start.Fit(0, s.Length);
      int safeLength = end - safeStart;
      try
      {
        return s.Substring(safeStart, safeLength);
      }
      catch (Exception e)
      {
        CUI.Log($"SubstringSafe {e.Message}\ns:\"{s}\" start: {start}->{safeStart} end: {end} length: {length}->{safeLength} ", Color.Orange);
        return "";
      }
    }

    public static Dictionary<string, string> ParseKVPairs(string raw)
    {
      Dictionary<string, string> props = new();

      if (raw == null || raw == "") return props;

      string content = raw.Split('{', '}')[1];

      List<string> expressions = new();
      int start = 0;
      int end = 0;
      int depth = 0;
      for (int i = 0; i < content.Length; i++)
      {
        char c = content[i];
        end = i;
        if (c == '[' || c == '{') depth++;
        if (c == ']' || c == '}') depth--;

        if (depth <= 0 && c == ',')
        {
          expressions.Add(content.Substring(start, end - start));
          start = end + 1;
        }
      }
      expressions.Add(content.Substring(start, end - start));

      var pairs = expressions.Select(s => s.Split(':').Select(sub => sub.Trim()).ToArray());

      foreach (var pair in pairs) { props[pair[0].ToLower()] = pair[1]; }
      return props;
    }

    public static string ColorToString(Color c) => $"{c.R},{c.G},{c.B},{c.A}";
    public static string Vector2ToString(Vector2 v) => $"[{v.X},{v.Y}]";
    public static string NullVector2ToString(Vector2? v) => v.HasValue ? $"[{v.Value.X},{v.Value.Y}]" : "null";
    public static string NullIntToString(int? i) => i.HasValue ? $"{i}" : "null";
    public static string RectangleToString(Rectangle r) => $"[{r.X},{r.Y},{r.Width},{r.Height}]";
    public static string GUIFontToString(GUIFont f) => f.Identifier.Value;
    public static string SpriteEffectsToString(SpriteEffects e)
    {
      if ((int)e == 3) return "FlipBothSides";
      else return e.ToString();
    }

    public static string IEnumerableStringToString(IEnumerable<string> e) => $"[{string.Join(',', e.ToArray())}]";

    public static IEnumerable<string> ParseIEnumerableString(string raw)
    {
      if (raw == null || raw == "") return new List<string>();
      string content = raw.Split('[', ']')[1];
      return content.Split(',');
    }

    public static string ParseString(string s) => s; // BaroDev (wide)
    //public static GUISoundType ParseGUISoundType(string s) => Enum.Parse<GUISoundType>(s);

    public static GUIFont ParseGUIFont(string raw)
    {
      GUIFont font = GUIStyle.Fonts.GetValueOrDefault(new Identifier(raw.Trim()));
      font ??= GUIStyle.Font;
      return font;
    }

    public static SpriteEffects ParseSpriteEffects(string raw)
    {
      if (raw == "FlipBothSides") return SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
      else return Enum.Parse<SpriteEffects>(raw);
    }


    public static int? ParseNullInt(string raw)
    {
      if (raw == "null") return null;
      return int.Parse(raw);
    }
    public static Vector2? ParseNullVector2(string raw)
    {
      if (raw == "null") return null;
      return ParseVector2(raw);
    }

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

    public static Rectangle ParseRectangle(string raw)
    {
      if (raw == null || raw == "") return new Rectangle(0, 0, 1, 1);

      string content = raw.Split('[', ']')[1];

      List<string> coords = content.Split(',').Select(s => s.Trim()).ToList();

      int x = 0;
      int y = 0;
      int w = 0;
      int h = 0;

      int.TryParse(coords.ElementAtOrDefault(0), out x);
      int.TryParse(coords.ElementAtOrDefault(1), out y);
      int.TryParse(coords.ElementAtOrDefault(2), out w);
      int.TryParse(coords.ElementAtOrDefault(3), out h);

      return new Rectangle(x, y, w, h);
    }


    public static Color ParseColor(string s) => XMLExtensions.ParseColor(s, false);


    public static Dictionary<Type, MethodInfo> Parse;
    public static Dictionary<Type, MethodInfo> CustomToString;

    internal static void InitStatic()
    {
      CUI.OnInit += () =>
      {
        Stopwatch sw = Stopwatch.StartNew();

        Parse = new Dictionary<Type, MethodInfo>();
        CustomToString = new Dictionary<Type, MethodInfo>();

        Parse[typeof(string)] = typeof(CUIExtensions).GetMethod("ParseString");
        //Parse[typeof(GUISoundType)] = typeof(CUIExtensions).GetMethod("ParseGUISoundType");

        Parse[typeof(Rectangle)] = typeof(CUIExtensions).GetMethod("ParseRectangle");
        Parse[typeof(GUIFont)] = typeof(CUIExtensions).GetMethod("ParseGUIFont");
        Parse[typeof(Vector2?)] = typeof(CUIExtensions).GetMethod("ParseNullVector2");
        Parse[typeof(Vector2)] = typeof(CUIExtensions).GetMethod("ParseVector2");
        Parse[typeof(SpriteEffects)] = typeof(CUIExtensions).GetMethod("ParseSpriteEffects");
        Parse[typeof(Color)] = typeof(CUIExtensions).GetMethod("ParseColor");
        Parse[typeof(int?)] = typeof(CUIExtensions).GetMethod("ParseNullInt");
        Parse[typeof(IEnumerable<string>)] = typeof(CUIExtensions).GetMethod("ParseIEnumerableString");


        CustomToString[typeof(IEnumerable<string>)] = typeof(CUIExtensions).GetMethod("IEnumerableStringToString");
        CustomToString[typeof(int?)] = typeof(CUIExtensions).GetMethod("NullIntToString");
        CustomToString[typeof(Color)] = typeof(CUIExtensions).GetMethod("ColorToString");
        CustomToString[typeof(SpriteEffects)] = typeof(CUIExtensions).GetMethod("SpriteEffectsToString");
        CustomToString[typeof(Vector2)] = typeof(CUIExtensions).GetMethod("Vector2ToString");
        CustomToString[typeof(Vector2?)] = typeof(CUIExtensions).GetMethod("NullVector2ToString");
        CustomToString[typeof(GUIFont)] = typeof(CUIExtensions).GetMethod("GUIFontToString");
        CustomToString[typeof(Rectangle)] = typeof(CUIExtensions).GetMethod("RectangleToString");

        CUIDebug.Log($"CUIExtensions.Initialize took {sw.ElapsedMilliseconds}ms");
      };

      CUI.OnDispose += () =>
      {
        Parse.Clear();
        CustomToString.Clear();
      };
    }
  }
}