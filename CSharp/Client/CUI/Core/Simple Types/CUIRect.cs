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
  /// Rectangle with float
  /// </summary>
  public struct CUIRect
  {
    public static CUIRect Zero { get; } = new CUIRect(0, 0, 0, 0);

    public float Left;
    public float Top;
    public float Width;
    public float Height;

    public float Right => Left + Width;
    public float Bottom => Top + Height;

    public Vector2 Size
    {
      get => new Vector2(Width, Height);
      set
      {
        Width = value.X;
        Height = value.Y;
      }
    }
    public Vector2 Position
    {
      get => new Vector2(Left, Top);
      set
      {
        Left = value.X;
        Top = value.Y;
      }
    }
    public Vector2 Center => new Vector2(Left + Width / 2, Top + Height / 2);
    public Rectangle Box => new Rectangle((int)Left, (int)Top, (int)Width, (int)Height);

    public Vector2 LeftTop => new Vector2(Left, Top);
    public Vector2 LeftCenter => new Vector2(Left, Top + Height / 2);
    public Vector2 LeftBottom => new Vector2(Left, Top + Height);
    public Vector2 CenterTop => new Vector2(Left + Width / 2, Top);
    public Vector2 CenterCenter => new Vector2(Left + Width / 2, Top + Height / 2);
    public Vector2 CenterBottom => new Vector2(Left + Width / 2, Top + Height);
    public Vector2 RightTop => new Vector2(Left + Width, Top);
    public Vector2 RightCenter => new Vector2(Left + Width, Top + Height / 2);
    public Vector2 RightBottom => new Vector2(Left + Width, Top + Height);



    public CUIRect Shift(Vector2 shift)
    {
      return new CUIRect(Left + shift.X, Top + shift.Y, Width, Height);
    }

    public bool Contains(float x, float y)
    {
      return Left < x && x < Right && Top < y && y < Bottom;
    }
    public bool Contains(Vector2 pos)
    {
      return Left < pos.X && pos.X < Right && Top < pos.Y && pos.Y < Bottom;
    }
    public bool Intersect(CUIRect r)
    {
      return r.Right >= Left && r.Left <= Right && r.Bottom >= Top && r.Top <= Bottom;
    }


    public CUIRect(Vector2 size) : this(0, 0, size.X, size.Y) { }
    public CUIRect(Vector2 position, Vector2 size) : this(position.X, position.Y, size.X, size.Y) { }
    public CUIRect(float x, float y, float w, float h)
    {
      Left = x;
      Top = y;
      Width = Math.Max(0f, w);
      Height = Math.Max(0f, h);
    }

    public override string ToString() => $"[{Left},{Top},{Width},{Height}]";
  }
}