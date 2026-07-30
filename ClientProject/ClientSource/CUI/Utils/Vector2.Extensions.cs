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
    /// Dot with orthogonal vector
    /// If it's > 0 then v is on the right, if < 0 then on the left
    /// </summary>
    public static float OrthDot(this Vector2 self, Vector2 v)
    {
      // Vector2 orthogonal = new Vector2(self.Y, -self.X);
      return self.Y * v.X - self.X * v.Y;
    }
  }
}