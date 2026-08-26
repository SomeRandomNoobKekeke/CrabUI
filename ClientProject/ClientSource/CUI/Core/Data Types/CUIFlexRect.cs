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
  public struct CUIFlexRect
  {
    public Vector2 LT { get; set; }
    public Vector2 RT { get; set; }
    public Vector2 RB { get; set; }
    public Vector2 LB { get; set; }

    public CUIRect Box
    {
      get
      {
        float left = LT.X;
        if (RT.X < left) left = RT.X;
        if (RB.X < left) left = RB.X;
        if (LB.X < left) left = LB.X;

        float top = LT.Y;
        if (RT.Y < top) top = RT.Y;
        if (RB.Y < top) top = RB.Y;
        if (LB.Y < top) top = LB.Y;

        float right = LT.X;
        if (RT.X > right) right = RT.X;
        if (RB.X > right) right = RB.X;
        if (LB.X > right) right = LB.X;

        float bottom = LT.Y;
        if (RT.Y > bottom) bottom = RT.Y;
        if (RB.Y > bottom) bottom = RB.Y;
        if (LB.Y > bottom) bottom = LB.Y;

        return new CUIRect(left, top, right - left, bottom - top);
      }
    }
    public CUIFlexRect()
    {

    }
    public CUIFlexRect(Vector2 lt, Vector2 rt, Vector2 rb, Vector2 lb)
    {
      LT = lt;
      RT = rt;
      RB = rb;
      LB = lb;
    }
  }
}