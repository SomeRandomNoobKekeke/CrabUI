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
  /// Defining Boundaries, not the same as rect  
  /// containing min/max x, y
  /// </summary>
  public struct CUIBoundaries
  {
    public static Func<CUIRect, CUIBoundaries> Free = (Rect) => new CUIBoundaries(null, null, null, null);
    public static Func<CUIRect, CUIBoundaries> Box = (Rect) => new CUIBoundaries(0, Rect.Width, 0, Rect.Height);
    public static Func<CUIRect, CUIBoundaries> HorizontalTube = (Rect) => new CUIBoundaries(null, null, 0, Rect.Height);
    public static Func<CUIRect, CUIBoundaries> VerticalTube = (Rect) => new CUIBoundaries(0, Rect.Width, null, null);


    public float? MinX;
    public float? MaxX;
    public float? MinY;
    public float? MaxY;

    public CUIRect Check(float x, float y, float w, float h)
    {
      if (MaxX.HasValue && x + w > MaxX.Value) x = MaxX.Value - w;
      if (MaxY.HasValue && y + h > MaxY.Value) y = MaxY.Value - h;
      if (MinX.HasValue && x < MinX.Value) x = MinX.Value;
      if (MinY.HasValue && y < MinY.Value) y = MinY.Value;

      return new CUIRect(x, y, w, h);
    }


    public Vector2 Check(Vector2 offset) => Check(offset.X, offset.Y);
    public Vector2 Check(float x, float y)
    {
      if (MaxX.HasValue && x > MaxX.Value) x = MaxX.Value;
      if (MaxY.HasValue && y > MaxY.Value) y = MaxY.Value;

      if (MinX.HasValue && x < MinX.Value) x = MinX.Value;
      if (MinY.HasValue && y < MinY.Value) y = MinY.Value;

      return new Vector2(x, y);
    }

    public CUIBoundaries(
      float? minX = null, float? maxX = null,
      float? minY = null, float? maxY = null
    )
    {
      MinX = minX;
      MaxX = maxX;
      MinY = minY;
      MaxY = maxY;
    }

    public override string ToString() => $"[{MinX},{MaxX},{MinY},{MaxY}]";
  }
}