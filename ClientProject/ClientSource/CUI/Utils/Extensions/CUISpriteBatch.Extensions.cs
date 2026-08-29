using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursedUI
{
  /// <summary>
  ///     Sprite batch extensions for drawing primitive shapes
  ///     Modified from: https://github.com/FakeFishGames/Barotrauma/blob/master/Barotrauma/BarotraumaClient/ClientSource/GUI/ShapeExtensions.cs
  /// </summary>
  public static class CUISpriteBatch_Extensions
  {

    /// <summary>
    ///     Draws a closed polygon from a <see cref="Polygon" /> shape
    /// </summary>
    public static void DrawPolygon(this CUISpriteBatch spriteBatch, Vector2 position, Polygon polygon, Color color,
        float thickness = 1f)
    {
      DrawPolygon(spriteBatch, position, polygon.Vertices, color, thickness);
    }

    /// <summary>
    ///     Draws a closed polygon from an array of points
    /// </summary>
    public static void DrawPolygon(this CUISpriteBatch spriteBatch, Vector2 offset, IReadOnlyList<Vector2> points, Color color,
        float thickness = 1f)
    {
      if (points.Count == 0)
        return;

      if (points.Count == 1)
      {
        DrawPoint(spriteBatch, points[0], color, (int)thickness);
        return;
      }

      for (var i = 0; i < points.Count - 1; i++)
        DrawPolygonEdge(spriteBatch, points[i] + offset, points[i + 1] + offset, color, thickness);

      DrawPolygonEdge(spriteBatch, points[points.Count - 1] + offset, points[0] + offset, color,
          thickness);
    }

    /// <summary>
    /// Draws a closed polygon from an array of points
    /// </summary>
    public static void DrawPolygonInner(this CUISpriteBatch spriteBatch, Vector2 offset, IReadOnlyList<Vector2> points, Color color, float thickness = 1f)
    {
      if (points.Count == 0) { return; }

      if (points.Count == 1)
      {
        DrawPoint(spriteBatch, points[0], color, (int)thickness);
        return;
      }

      for (var i = 0; i < points.Count - 1; i++)
      {
        Vector2 point1 = points[i] + offset,
                point2 = points[i + 1] + offset;

        DrawPolygonEdgeInner(spriteBatch, point1, point2, color, thickness);
      }

      DrawPolygonEdgeInner(spriteBatch, points[^1] + offset, points[0] + offset, color, thickness);
    }

    private static void DrawPolygonEdgeInner(CUISpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness)
    {
      var length = Vector2.Distance(point1, point2) + thickness;
      var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
      var scale = new Vector2(length, thickness);
      Vector2 middle = new Vector2((point1.X + point2.X) / 2f, (point1.Y + point2.Y) / 2f);
      CUITexture2D tex = CUITexture2D.White;
      spriteBatch.Draw(tex, middle, null, color, angle, new Vector2(tex.Width / 2f, tex.Height / 2f), scale, SpriteEffects.None, 0);
    }

    private static void DrawPolygonEdge(CUISpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness)
    {
      var length = Vector2.Distance(point1, point2);
      var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
      var scale = new Vector2(length, thickness);

      CUITexture2D tex = CUITexture2D.White;
      spriteBatch.Draw(tex, point1, null, color, angle, Vector2.Zero, scale, SpriteEffects.None, 0);
    }

    /// <summary>
    ///     Draws a line from point1 to point2 with an offset
    /// </summary>
    public static void DrawLine(this CUISpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color,
        float thickness = 1f)
    {
      DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, thickness);
    }

    public static void DrawLineWithTexture(this CUISpriteBatch spriteBatch, CUITexture2D tex, Vector2 point1, Vector2 point2,
                                           Color color, float thickness = 1f)
    {
      // calculate the distance between the two vectors
      var distance = Vector2.Distance(point1, point2);

      // calculate the angle between the two vectors
      var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

      DrawLine(spriteBatch, tex, point1, distance, angle, color, thickness);
    }

    /// <summary>
    ///     Draws a line from point1 to point2 with an offset
    /// </summary>
    public static void DrawLine(this CUISpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color,
        float thickness = 1f)
    {
      // calculate the distance between the two vectors
      var distance = Vector2.Distance(point1, point2);

      // calculate the angle between the two vectors
      var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

      CUITexture2D tex = CUITexture2D.White;
      DrawLine(spriteBatch, tex, point1, distance, angle, color, thickness);
    }

    /// <summary>
    ///     Draws a line from point1 to point2 with an offset
    /// </summary>
    public static void DrawLine(this CUISpriteBatch spriteBatch, CUITexture2D tex, Vector2 point, float length, float angle, Color color,
        float thickness = 1f)
    {
      var origin = new Vector2(0f, tex.Height / 2f);
      var scale = new Vector2(length / tex.Width, thickness / tex.Height);
      spriteBatch.Draw(tex, point, null, color, angle, origin, scale, SpriteEffects.None, 0);
    }

    /// <summary>
    ///     Draws a point at the specified x, y position. The center of the point will be at the position.
    /// </summary>
    public static void DrawPoint(this CUISpriteBatch spriteBatch, float x, float y, Color color, float size = 1f)
    {
      DrawPoint(spriteBatch, new Vector2(x, y), color, size);
    }

    /// <summary>
    ///     Draws a point at the specified position. The center of the point will be at the position.
    /// </summary>
    public static void DrawPoint(this CUISpriteBatch spriteBatch, Vector2 position, Color color, float size = 1f)
    {
      var offset = new Vector2(0.5f) - new Vector2(size * 0.5f);

      CUITexture2D tex = CUITexture2D.White;
      spriteBatch.Draw(tex, position + offset, null, color, 0.0f, Vector2.Zero, new Vector2(size), SpriteEffects.None, 0);
    }

    public static void DrawCircle(this CUISpriteBatch spriteBatch, Vector2 center, float radius, int sides, Color color,
        float thickness = 1f)
    {
      DrawPolygon(spriteBatch, center, CreateCircle(radius, sides), color, thickness);
    }

    public static void DrawCircle(this CUISpriteBatch spriteBatch, float x, float y, float radius, int sides,
        Color color, float thickness = 1f)
    {
      DrawPolygon(spriteBatch, new Vector2(x, y), CreateCircle(radius, sides), color, thickness);
    }

    public static void DrawSector(this CUISpriteBatch spriteBatch, Vector2 center, float radius, float radians, int sides, Color color, float offset = 0, float thickness = 1)
    {
      DrawPolygon(spriteBatch, center, CreateSector(radius, sides, radians, offset), color, thickness);
    }

    private static Vector2[] CreateSector(double radius, int sides, float radians, float offset = 0)
    {
      //circle sectors need one extra point at the center
      var points = new Vector2[radians < MathHelper.TwoPi ? sides + 1 : sides];
      var step = radians / sides;

      double theta = offset;
      for (var i = 0; i < sides; i++)
      {
        points[i] = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * (float)radius;
        theta += step;
      }

      return points;
    }

    private static Vector2[] CreateCircle(double radius, int sides)
    {
      return CreateSector(radius, sides, MathHelper.TwoPi);
    }
  }
}