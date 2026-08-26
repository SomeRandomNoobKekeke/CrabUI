using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace CursedUI
{

  public struct CUISizes : IParsable
  {
    public static CUIRect Zero { get; } = new CUIRect(0, 0, 0, 0);

    public float Left;
    public float Top;
    public float Right;
    public float Bottom;

    public float FullWidth => Left + Right;
    public float FullHeigth => Top + Bottom;

    public static CUISizes operator +(CUISizes a, CUISizes b)
      => new CUISizes(
        a.Top + b.Top,
        a.Right + b.Right,
        a.Bottom + b.Bottom,
        a.Left + b.Left
      );

    public static CUISizes operator -(CUISizes a, CUISizes b)
      => new CUISizes(
        a.Top - b.Top,
        a.Right - b.Right,
        a.Bottom - b.Bottom,
        a.Left - b.Left
      );

    public CUISizes(float top = 0, float right = 0, float bottom = 0, float left = 0)
    {
      Top = top;
      Right = right;
      Bottom = bottom;
      Left = left;
    }

    public override string ToString() => $"[{Top},{Right},{Bottom},{Left}]";

    public string ToText() => ToString();

    static object IParsable.Parse(string raw) => Parse(raw);
    public static CUISizes Parse(string s)
    {
      string content = s.Substring(
        s.IndexOf('[') + 1,
        s.IndexOf(']') - s.IndexOf('[') - 1
      );

      var components = content.Split(',').Select(a => a.Trim());

      string st = components.ElementAtOrDefault(0);
      string sr = components.ElementAtOrDefault(1);
      string sb = components.ElementAtOrDefault(2);
      string sl = components.ElementAtOrDefault(3);

      float t = 0;
      float r = 0;
      float b = 0;
      float l = 0;

      if (!String.IsNullOrEmpty(st)) t = float.Parse(st);
      if (!String.IsNullOrEmpty(sr)) r = float.Parse(sr);
      if (!String.IsNullOrEmpty(sb)) b = float.Parse(sb);
      if (!String.IsNullOrEmpty(sl)) l = float.Parse(sl);

      return new CUISizes(t, r, b, l);
    }
  }


}