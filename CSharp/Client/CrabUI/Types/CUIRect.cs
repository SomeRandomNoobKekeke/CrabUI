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
    /// <summary>
    /// IDK where to put it
    /// </summary>
    public static Rectangle CreateRect(float x, float y, float w, float h)
    {
      return new Rectangle((int)Math.Round(x), (int)Math.Round(y), (int)Math.Round(w), (int)Math.Round(h));
    }


    public float Left;
    public float Top;
    public float Width;
    public float Height;

    public float Right => Left + Width;
    public float Bottom => Top + Height;

    public Vector2 Size => new Vector2(Width, Height);
    public Vector2 Position => new Vector2(Left, Top);
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

    public Rectangle Zoom(float Z)
    {
      Vector2 ScreenCenter = CUI.GameScreenSize / 2.0f;
      Vector2 PosDif = Position - ScreenCenter;
      Vector2 newPos = PosDif * Z + ScreenCenter;

      return new Rectangle(
        (int)newPos.X, (int)newPos.Y,
        (int)(Width / Z), (int)(Height / Z)
      );
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

  /// <summary>
  /// Rectangle with float?
  /// </summary>
  public struct CUINullRect
  {
    // Guh...
    public static CUINullRect Parse(string s)
    {
      string content = s.Substring(
        s.IndexOf('[') + 1,
        s.IndexOf(']') - s.IndexOf('[') - 1
      );

      var components = content.Split(',').Select(a => a.Trim());

      string sx = components.ElementAtOrDefault(0);
      string sy = components.ElementAtOrDefault(1);
      string sw = components.ElementAtOrDefault(2);
      string sh = components.ElementAtOrDefault(3);

      float? x = null;
      float? y = null;
      float? w = null;
      float? h = null;

      if (sx == null || sx == "") x = null;
      else x = float.Parse(sx);

      if (sy == null || sy == "") y = null;
      else y = float.Parse(sy);

      if (sw == null || sw == "") w = null;
      else w = float.Parse(sw);

      if (sh == null || sh == "") h = null;
      else h = float.Parse(sh);

      return new CUINullRect(x, y, w, h);
    }

    public float? Left;
    public float? Top;
    public float? Width;
    public float? Height;

    public Vector2 Size
    {
      get => new Vector2(Width ?? 0, Height ?? 0);
      set { Width = value.X; Height = value.Y; }
    }
    public Vector2 Position
    {
      get => new Vector2(Left ?? 0, Top ?? 0);
      set { Left = value.X; Top = value.Y; }
    }

    public Vector2 Center => new Vector2(
      (Left ?? 0) + (Width ?? 0) / 2,
      (Top ?? 0) + (Height ?? 0) / 2
    );

    public CUINullRect(Vector2 position, Vector2 size) : this(position.X, position.Y, size.X, size.Y) { }

    public CUINullRect(float? x = null, float? y = null, float? w = null, float? h = null)
    {
      Left = x;
      Top = y;
      Width = w;
      Height = h;
    }

    public override string ToString() => $"[{Left},{Top},{Width},{Height}]";

  }
}