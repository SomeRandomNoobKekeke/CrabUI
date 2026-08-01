using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public partial class TextureBuilder
  {
    public TextureBuilder DrawPoint(Vector2 point, Color color, float thickness, float fade)
    {
      float outerRadius = thickness + fade;

      IntBounds affected = Bounds.FromRadius(point, outerRadius).Round();
      if (!affected.Intersects(0, 0, Width, Height)) return this;
      affected.Fit(0, 0, Width, Height);

      for (int y = affected.MinY; y < affected.MaxY; y++)
      {
        for (int x = affected.MinX; x < affected.MaxX; x++)
        {
          Vector2 v = new Vector2(x - point.X, y - point.Y);
          float l = v.Length();

          if (l > outerRadius) continue;

          float lambda = (l - thickness) / fade;
          SetPixel(x, y, Color.Lerp(color, Color.Transparent, lambda));
        }
      }

      return this;
    }

    public TextureBuilder DrawCircle(Vector2 origin, float radius, Color fillColor)
    {
      IntBounds affected = Bounds.FromRadius(origin, radius).Round();
      if (!affected.Intersects(0, 0, Width, Height)) return this;
      affected.Fit(0, 0, Width, Height);

      float r2 = radius * radius;

      for (int y = affected.MinY; y < affected.MaxY; y++)
      {
        for (int x = affected.MinX; x < affected.MaxX; x++)
        {
          Vector2 v = new Vector2(x - origin.X, y - origin.Y);

          if (v.LengthSquared() <= r2)
          {
            SetPixel(x, y, fillColor);
          }
        }
      }

      return this;
    }

    public TextureBuilder DrawCircleBorder(Vector2 origin, float radius, Color borderColor, float borderThickness, float borderFade)
    {
      float outerRadius = radius + borderThickness + borderFade;

      IntBounds affected = Bounds.FromRadius(origin, outerRadius).Round();
      if (!affected.Intersects(0, 0, Width, Height)) return this;
      affected.Fit(0, 0, Width, Height);

      float totalThickness = borderThickness + borderFade;

      for (int y = affected.MinY; y < affected.MaxY; y++)
      {
        for (int x = affected.MinX; x < affected.MaxX; x++)
        {
          Vector2 v = new Vector2(x - origin.X, y - origin.Y);
          float dl = Math.Abs(v.Length() - radius);


          if (dl > totalThickness) continue;

          float lambda = (dl - borderThickness) / borderFade;
          SetPixel(x, y, Color.Lerp(borderColor, Color.Transparent, lambda));
        }
      }

      return this;
    }

    public TextureBuilder DrawCircle(Vector2 origin, float radius, Color fillColor, Color borderColor, float borderThickness, float borderFade)
    {
      float outerRadius = radius + borderThickness + borderFade;

      IntBounds affected = Bounds.FromRadius(origin, outerRadius).Round();
      if (!affected.Intersects(0, 0, Width, Height)) return this;
      affected.Fit(0, 0, Width, Height);

      float totalThickness = borderThickness + borderFade;

      for (int y = affected.MinY; y < affected.MaxY; y++)
      {
        for (int x = affected.MinX; x < affected.MaxX; x++)
        {
          Vector2 v = new Vector2(x - origin.X, y - origin.Y);
          float l = v.Length();

          if (l <= radius) SetPixel(x, y, fillColor);

          float dl = Math.Abs(l - radius);


          if (dl > totalThickness) continue;

          float lambda = (dl - borderThickness) / borderFade;
          SetPixel(x, y, Color.Lerp(borderColor, Color.Transparent, lambda));
        }
      }

      return this;
    }

    public TextureBuilder DrawLine(Vector2 pointA, Vector2 pointB, Color color, float thickness, float fade)
    {
      if (pointA == pointB) return DrawPoint(pointA, color, thickness, fade);

      float outerRadius = thickness + fade;

      IntBounds affected = Bounds.From2Points(pointA, pointB, outerRadius).Round();
      if (!affected.Intersects(0, 0, Width, Height)) return this;
      affected.Fit(0, 0, Width, Height);

      for (int y = affected.MinY; y < affected.MaxY; y++)
      {
        for (int x = affected.MinX; x < affected.MaxX; x++)
        {
          float distance = new Vector2(x, y).DistanceToSegment(pointA, pointB);
          if (distance > outerRadius) continue;

          float lambda = (distance - thickness) / fade;
          SetPixel(x, y, Color.Lerp(color, Color.Transparent, lambda));
        }
      }

      return this;
    }

    public TextureBuilder DrawArc(Vector2 origin, float radius, double startAngle, double endAngle, Color borderColor, float thickness, float fade)
    {
      float outerRadius = radius + thickness + fade;

      IntBounds affected = Bounds.FromRadius(origin, outerRadius).Round();
      if (!affected.Intersects(0, 0, Width, Height)) return this;
      affected.Fit(0, 0, Width, Height);

      float totalThickness = thickness + fade;

      for (int y = affected.MinY; y < affected.MaxY; y++)
      {
        for (int x = affected.MinX; x < affected.MaxX; x++)
        {
          float distance = new Vector2(x, y).DistanceToArc(origin, radius, startAngle, endAngle);
          if (distance > totalThickness) continue;

          float lambda = (distance - thickness) / fade;
          SetPixel(x, y, Color.Lerp(borderColor, Color.Transparent, lambda));
        }
      }

      return this;
    }

  }
}
