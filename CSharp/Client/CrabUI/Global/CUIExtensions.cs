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
  [CUIInternal]
  public static class CUIExtensions
  {
    public static Color RandomColor() => new Color(CUI.Random.Next(256), CUI.Random.Next(256), CUI.Random.Next(256));

    public static Color GrayScale(int v) => new Color(v, v, v);


    public static int Fit(this int i, int bottom, int top) => Math.Max(bottom, Math.Min(i, top));

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


    public static string Vector2ToString(Vector2 v) => $"[{v.X},{v.Y}]";
    public static string RectangleToString(Rectangle r) => $"[{r.X},{r.Y},{r.Width},{r.Height}]";
    public static string GUIFontToString(GUIFont f) => f.Identifier.Value;
    public static string SpriteEffectsToString(SpriteEffects e)
    {
      if ((int)e == 3) return "FlipBothSides";
      else return e.ToString();
    }

    public static string ParseString(string s) => s; // BaroDev (wide)
    public static GUISoundType ParseGUISoundType(string s) => Enum.Parse<GUISoundType>(s);

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
        Parse = new Dictionary<Type, MethodInfo>();
        CustomToString = new Dictionary<Type, MethodInfo>();

        Parse[typeof(string)] = typeof(CUIExtensions).GetMethod("ParseString");
        Parse[typeof(GUISoundType)] = typeof(CUIExtensions).GetMethod("ParseGUISoundType");
        Parse[typeof(Color)] = typeof(CUIExtensions).GetMethod("ParseColor");
        Parse[typeof(Vector2)] = typeof(CUIExtensions).GetMethod("ParseVector2");
        Parse[typeof(Rectangle)] = typeof(CUIExtensions).GetMethod("ParseRectangle");
        Parse[typeof(GUIFont)] = typeof(CUIExtensions).GetMethod("ParseGUIFont");
        Parse[typeof(SpriteEffects)] = typeof(CUIExtensions).GetMethod("ParseSpriteEffects");


        CustomToString[typeof(SpriteEffects)] = typeof(CUIExtensions).GetMethod("SpriteEffectsToString");
        CustomToString[typeof(Vector2)] = typeof(CUIExtensions).GetMethod("Vector2ToString");
        CustomToString[typeof(Rectangle)] = typeof(CUIExtensions).GetMethod("RectangleToString");
        CustomToString[typeof(GUIFont)] = typeof(CUIExtensions).GetMethod("GUIFontToString");
      };

      CUI.OnDispose += () =>
      {
        Parse.Clear();
        CustomToString.Clear();
      };
    }
  }
}