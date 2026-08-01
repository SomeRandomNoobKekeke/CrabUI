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
  public class IntBounds
  {
    public int MinX { get; set; }
    public int MaxX { get; set; }
    public int MinY { get; set; }
    public int MaxY { get; set; }

    public bool Intersects(int x, int y, int w, int h)
    {
      if (MaxX < x) return false;
      if (MaxY < y) return false;
      if (MinX > x + w) return false;
      if (MinY > y + h) return false;
      return true;
    }

    public bool Intersects(Rectangle rect)
    {
      if (MaxX < rect.Left) return false;
      if (MaxY < rect.Top) return false;
      if (MinX > rect.Right) return false;
      if (MinY > rect.Bottom) return false;
      return true;
    }

    public void Fit(int x, int y, int w, int h)
    {
      MinX = Math.Max(x, MinX);
      MaxX = Math.Min(x + w, MaxX);
      MinY = Math.Max(y, MinY);
      MaxY = Math.Min(y + h, MaxY);
    }

    public void Fit(Rectangle rect)
    {
      MinX = Math.Max(rect.Left, MinX);
      MaxX = Math.Min(rect.Right, MaxX);
      MinY = Math.Max(rect.Top, MinY);
      MaxY = Math.Min(rect.Bottom, MaxY);
    }
  }

  public class Bounds
  {
    public float MinX { get; set; }
    public float MaxX { get; set; }
    public float MinY { get; set; }
    public float MaxY { get; set; }

    public Rectangle ToRect() => new Rectangle(
      (int)Math.Round(MinX),
      (int)Math.Round(MinY),
      (int)Math.Round(MaxX - MinX),
      (int)Math.Round(MaxY - MinY)
    );

    public IntBounds Round() => new IntBounds()
    {
      MinX = (int)Math.Round(MinX),
      MaxX = (int)Math.Round(MaxX),
      MinY = (int)Math.Round(MinY),
      MaxY = (int)Math.Round(MaxY),
    };

    public static Bounds From2Points(Vector2 pointA, Vector2 pointB, float margin = 0) => new Bounds()
    {
      MinX = Math.Min(pointA.X, pointB.X) - margin,
      MaxX = Math.Max(pointA.X, pointB.X) + margin,
      MinY = Math.Min(pointA.Y, pointB.Y) - margin,
      MaxY = Math.Max(pointA.Y, pointB.Y) + margin,
    };

    public static Bounds FromRadius(Vector2 origin, float radius) => new Bounds()
    {
      MinX = origin.X - radius,
      MaxX = origin.X + radius,
      MinY = origin.Y - radius,
      MaxY = origin.Y + radius,
    };
  }


}