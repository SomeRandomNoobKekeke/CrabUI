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
  /// Vector2, but with float?
  /// </summary>
  public struct CUINullVector2
  {
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

    public override string ToString() => $"[{X},{Y}]";

  }
}