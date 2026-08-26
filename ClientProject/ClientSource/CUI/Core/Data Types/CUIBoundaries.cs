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

  /// <summary>
  /// Defining Boundaries, not the same as rect  
  /// containing min/max x, y
  /// </summary>
  public struct CUIBoundaries : IParsable
  {
    public static Func<CUIRect, CUIBoundaries> Free =
      (Rect) => new CUIBoundaries(null, null, null, null);
    public static Func<CUIRect, CUIBoundaries> Box =
      (Rect) => new CUIBoundaries(Rect.Left, Rect.Left + Rect.Width, Rect.Top, Rect.Top + Rect.Height);
    public static Func<CUIRect, CUIBoundaries> HorizontalTube =
      (Rect) => new CUIBoundaries(null, null, Rect.Top, Rect.Top + Rect.Height);
    public static Func<CUIRect, CUIBoundaries> VerticalTube =
      (Rect) => new CUIBoundaries(Rect.Left, Rect.Left + Rect.Width, null, null);


    public float? MinX;
    public float? MaxX;
    public float? MinY;
    public float? MaxY;

    public CUIRect Check(CUIRect rect) => Check(rect.Left, rect.Top, rect.Width, rect.Height);
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


    public bool OutOfBounds(CUIRect rect) => OutOfBounds(rect.Left, rect.Top, rect.Width, rect.Height);
    public bool OutOfBounds(float x, float y, float w, float h)
    {
      if (MaxX.HasValue && x + w > MaxX.Value) return true;
      if (MaxY.HasValue && y + h > MaxY.Value) return true;
      if (MinX.HasValue && x < MinX.Value) return true;
      if (MinY.HasValue && y < MinY.Value) return true;

      return false;
    }

    //BRUH mb this should be in some CUIBoundaries extensions
    public CUIRect FitMovingRect(CUIRect prev, CUIRect next)
    {
      float x = next.Left;
      float y = next.Top;
      float w = next.Width;
      float h = next.Height;


      if (MinX.HasValue && next.Left < MinX.Value)
      {
        x = MinX.Value;
        w = prev.Width;
      }

      if (MaxX.HasValue && next.Right > MaxX.Value)
      {
        x = MaxX.Value - prev.Width;
        w = prev.Width;
      }

      if (MinX.HasValue && next.Left < MinX.Value && MaxX.HasValue && next.Right > MaxX.Value)
      {
        x = MinX.Value;
        w = MaxX.Value - MinX.Value;
      }


      if (MinY.HasValue && next.Top < MinY.Value)
      {
        y = MinY.Value;
        h = prev.Height;
      }

      if (MaxY.HasValue && next.Bottom > MaxY.Value)
      {
        y = MaxY.Value - prev.Height;
        h = prev.Height;
      }

      if (MinY.HasValue && next.Top < MinY.Value && MaxY.HasValue && next.Bottom > MaxY.Value)
      {
        y = MinY.Value;
        h = MaxY.Value - MinY.Value;
      }

      return new CUIRect(x, y, w, h);
    }

    public CUIRect FitResizingRect(CUIRect prev, CUIRect next)
    {
      float x = next.Left;
      float y = next.Top;
      float w = next.Width;
      float h = next.Height;


      if (MinX.HasValue && next.Left < MinX.Value)
      {
        w = prev.Right - MinX.Value;
        x = MinX.Value;
      }

      if (MaxX.HasValue && next.Right > MaxX.Value)
      {
        w = MaxX.Value - prev.Left;
        x = MaxX.Value - w;
      }

      if (MinX.HasValue && next.Left < MinX.Value && MaxX.HasValue && next.Right > MaxX.Value)
      {
        x = MinX.Value;
        w = MaxX.Value - MinX.Value;
      }


      if (MinY.HasValue && next.Top < MinY.Value)
      {
        h = prev.Bottom - MinY.Value;
        y = MinY.Value;
      }

      if (MaxY.HasValue && next.Bottom > MaxY.Value)
      {
        h = MaxY.Value - prev.Top;
        y = MaxY.Value - h;
      }

      if (MinY.HasValue && next.Top < MinY.Value && MaxY.HasValue && next.Bottom > MaxY.Value)
      {
        y = MinY.Value;
        h = MaxY.Value - MinY.Value;
      }

      return new CUIRect(x, y, w, h);
    }

    public IntBounds Round() => new IntBounds(
      (int)MinX,
      (int)MaxX,
      (int)MinY,
      (int)MaxY
    );

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
    public static CUIBoundaries Parse(string s)
    {
      string content = s.Substring(
        s.IndexOf('[') + 1,
        s.IndexOf(']') - s.IndexOf('[') - 1
      );

      var parts = content.Split(',').Select(a => a.Trim());

      string minXPart = parts.ElementAtOrDefault(0);
      string maxXPart = parts.ElementAtOrDefault(1);
      string minYPart = parts.ElementAtOrDefault(2);
      string maxYPart = parts.ElementAtOrDefault(3);

      float? minX = null;
      float? maxX = null;
      float? minY = null;
      float? maxY = null;

      if (!String.IsNullOrEmpty(minXPart)) minX = float.Parse(minXPart);
      if (!String.IsNullOrEmpty(maxXPart)) maxX = float.Parse(maxXPart);
      if (!String.IsNullOrEmpty(minYPart)) minY = float.Parse(minYPart);
      if (!String.IsNullOrEmpty(maxYPart)) maxY = float.Parse(maxYPart);

      return new CUIBoundaries(minX, maxX, minY, maxY);
    }

    static object IParsable.Parse(string raw) => Parse(raw);
    public string ToText() => ToString();
  }
}