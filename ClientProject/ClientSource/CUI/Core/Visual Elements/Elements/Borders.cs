using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;

namespace CursedUI
{
  public class Borders : VisualElementBase, CUISerializable
  {
    private CUIRect _Rect; public CUIRect Rect
    {
      get => _Rect;
      set
      {
        _Rect = value;
        UpdateRects();
      }
    }
    public CUIRect InnerRect { get; set; }

    public CUISizes Sizes { get; set; }
    public float Top
    {
      get => Sizes.Top;
      set => Sizes = Sizes with { Top = value };
    }
    public float Right
    {
      get => Sizes.Right;
      set => Sizes = Sizes with { Right = value };
    }
    public float Bottom
    {
      get => Sizes.Bottom;
      set => Sizes = Sizes with { Bottom = value };
    }
    public float Left
    {
      get => Sizes.Left;
      set => Sizes = Sizes with { Left = value };
    }

    public CUISprite Sprite { get; set; } = CUISprite.Vignette;


    [CUISerializableProp]
    public Color Color { get => Sprite.Color; set => Sprite.Color = value; }

    public override bool Contains(Vector2 pos)
    {
      return Rect.Contains(pos) && !InnerRect.Contains(pos);
    }

    private void UpdateRects()
    {
      InnerRect = Rect - Sizes;

      Rectangle rounded = Rect.Round();

      LeftRect = new Rectangle(
        rounded.X,
        rounded.Y + (int)Sizes.Top,
        (int)Sizes.Left,
        rounded.Height - (int)Sizes.Top - (int)Sizes.Bottom
      );

      TopRect = new Rectangle(
        rounded.X,
        rounded.Y,
        rounded.Width,
        (int)Sizes.Top
      );

      RightRect = new Rectangle(
        (int)(rounded.X + rounded.Width - Sizes.Right),
        rounded.Y + (int)Sizes.Top,
        (int)Sizes.Right,
        rounded.Height - (int)Sizes.Top - (int)Sizes.Bottom
      );

      BottomRect = new Rectangle(
        rounded.X,
        (int)(rounded.Y + rounded.Height - Sizes.Bottom),
        rounded.Width,
        (int)Sizes.Bottom
      );
    }

    private Rectangle LeftRect;
    private Rectangle TopRect;
    private Rectangle RightRect;
    private Rectangle BottomRect;


    public override void Draw(CUISpriteBatch spriteBatch)
    {
      if (Visible)
      {
        if (Sizes.Left != 0) Sprite.Draw(spriteBatch, LeftRect);
        if (Sizes.Top != 0) Sprite.Draw(spriteBatch, TopRect);
        if (Sizes.Right != 0) Sprite.Draw(spriteBatch, RightRect);
        if (Sizes.Bottom != 0) Sprite.Draw(spriteBatch, BottomRect);
      }
    }

    public static object Deserialize(XElement element)
    {
      return new Borders();
    }
  }
}