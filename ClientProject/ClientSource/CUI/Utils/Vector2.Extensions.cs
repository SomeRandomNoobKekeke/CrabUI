using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Barotrauma;

namespace CrabUI
{
  public static class Vector2_Extensions
  {
    public static double Pi2 = Math.PI * 2;
    public static double Pi05 = Math.PI * 0.5;


    /// <summary>
    /// lazy unoptimized implementation
    /// </summary>
    public static float DistanceToSegment(this Vector2 point, Vector2 pointA, Vector2 pointB)
    {
      if (pointA == pointB) return Vector2.Distance(point, pointA);


      Vector2 AB = pointB - pointA;
      Vector2 AP = point - pointA;

      float AB_AP = Vector2.Dot(AB, AP);
      if (AB_AP <= 0) return Vector2.Distance(point, pointA);
      if (AB_AP >= AB.LengthSquared()) return Vector2.Distance(point, pointB);

      Vector2 normal = Vector2.Normalize(new Vector2(AB.Y, -AB.X));

      return Math.Abs(Vector2.Dot(normal, AP));
    }

    public static float DistanceToCircle(this Vector2 point, Vector2 origin, float radius)
    {
      return Math.Abs((point - origin).Length() - radius);
    }

    public static Vector2 PointOnACircle(Vector2 origin, float radius, double angle)
      => origin + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * radius;

    /// <summary>
    /// lazy unoptimized implementation
    /// </summary>
    public static float DistanceToArc(this Vector2 point, Vector2 origin, float radius, double startAngle, double endAngle)
    {
      if (radius <= 0) return Vector2.Distance(point, origin);

      if (startAngle == endAngle)
      {
        return Vector2.Distance(point, PointOnACircle(origin, radius, startAngle));
      }

      if (Math.Abs(endAngle - startAngle) > 2 * Math.PI)
      {
        return DistanceToCircle(point, origin, radius);
      }

      startAngle = Utils.BoundAngle(startAngle);
      endAngle = Utils.BoundAngle(endAngle);

      Vector2 v = point - origin;
      double angle = Math.Atan2(v.Y, v.X);

      if (Utils.IsAngleWithin(angle, startAngle, endAngle))
      {
        return DistanceToCircle(point, origin, radius);
      }

      float distanceToStart = Vector2.Distance(point, PointOnACircle(origin, radius, startAngle));
      float distanceToEnd = Vector2.Distance(point, PointOnACircle(origin, radius, endAngle));

      return Math.Min(distanceToStart, distanceToEnd);
    }



  }
}