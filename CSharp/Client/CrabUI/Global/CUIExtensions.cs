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


    public static string Vector2ToString(Vector2 v) => $"[{v.X},{v.Y}]";


    public static string ParseString(string s) => s; // BaroDev (wide)
    public static GUISoundType ParseGUISoundType(string s) => Enum.Parse<GUISoundType>(s);


    public static Vector2 ParseVector2(string raw)
    {
      string content = raw.Split('[', ']')[1];

      List<string> coords = content.Split(',').Select(s => s.Trim()).ToList();

      float x = 0;
      float y = 0;

      float.TryParse(coords.ElementAtOrDefault(0), out x);
      float.TryParse(coords.ElementAtOrDefault(1), out y);

      return new Vector2(x, y);
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

        CustomToString[typeof(Vector2)] = typeof(CUIExtensions).GetMethod("Vector2ToString");
      };

      CUI.OnDispose += () =>
      {
        Parse.Clear();
        CustomToString.Clear();
      };
    }
  }
}