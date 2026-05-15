using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace CrabUI
{
  /// <summary>
  /// Rectangle with float?
  /// </summary>
  public struct CUINullRect
  {
    // Guh...
    public static CUINullRect Parse(string s)
    {
      string content = s.Substring(
        s.IndexOf('[') + 1,
        s.IndexOf(']') - s.IndexOf('[') - 1
      );

      var components = content.Split(',').Select(a => a.Trim());

      string sx = components.ElementAtOrDefault(0);
      string sy = components.ElementAtOrDefault(1);
      string sw = components.ElementAtOrDefault(2);
      string sh = components.ElementAtOrDefault(3);

      float? x = null;
      float? y = null;
      float? w = null;
      float? h = null;

      if (sx == null || sx == "") x = null;
      else x = float.Parse(sx);

      if (sy == null || sy == "") y = null;
      else y = float.Parse(sy);

      if (sw == null || sw == "") w = null;
      else w = float.Parse(sw);

      if (sh == null || sh == "") h = null;
      else h = float.Parse(sh);

      return new CUINullRect(x, y, w, h);
    }

    public float? Left;
    public float? Top;
    public float? Width;
    public float? Height;

    public Vector2 Size
    {
      get => new Vector2(Width ?? 0, Height ?? 0);
      set { Width = value.X; Height = value.Y; }
    }
    public Vector2 Position
    {
      get => new Vector2(Left ?? 0, Top ?? 0);
      set { Left = value.X; Top = value.Y; }
    }

    public Vector2 Center => new Vector2(
      (Left ?? 0) + (Width ?? 0) / 2,
      (Top ?? 0) + (Height ?? 0) / 2
    );

    public CUINullRect(Vector2 position, Vector2 size) : this(position.X, position.Y, size.X, size.Y) { }

    public CUINullRect(float? x = null, float? y = null, float? w = null, float? h = null)
    {
      Left = x;
      Top = y;
      Width = w;
      Height = h;
    }

    public override string ToString() => $"[{Left},{Top},{Width},{Height}]";

  }
}