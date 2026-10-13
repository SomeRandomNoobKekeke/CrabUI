using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics.CodeAnalysis;


namespace CursedUI
{
  /// <summary>
  /// Vector2, but with float?
  /// </summary>
  public struct CUINullVector2
  {
    public static CUINullVector2 Null => new CUINullVector2(null, null);

    public float? X;
    public float? Y;


    public CUINullVector2()
    {
      X = null;
      Y = null;
    }
    public CUINullVector2(Vector2 v)
    {
      X = v.X;
      Y = v.Y;
    }
    public CUINullVector2(float? x, float? y)
    {
      X = x;
      Y = y;
    }

    public static bool operator ==(CUINullVector2 v1, CUINullVector2 v2)
    {
      return v1.Equals(v2);
    }

    public static bool operator !=(CUINullVector2 v1, CUINullVector2 v2)
    {
      return !v1.Equals(v2);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
      if (obj is not CUINullVector2 other) return false;
      return Equals(other);
    }
    public bool Equals(CUINullVector2 other)
    {
      return X == other.X && Y == other.Y;
    }

    public override string ToString() => $"[{X},{Y}]";
  }
}