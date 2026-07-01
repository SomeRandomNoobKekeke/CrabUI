using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public class Borders : VisualElementBase
  {
    private CUIRect _Rect; public override CUIRect Rect
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
    public bool Visible { get; set; }


    public CUISprite Sprite { get; set; } = CUISprite.White;
    public Color Color { get => Sprite.Color; set => Sprite.Color = value; }

    public override bool Contains(Vector2 pos)
    {
      return Rect.Contains(pos) && !InnerRect.Contains(pos);
    }

    private void UpdateRects()
    {
      InnerRect = Rect - Sizes;

      LeftRect = new Rectangle(
        (int)Rect.Left,
        (int)Rect.Top,
        (int)Sizes.Left,
        (int)Rect.Height
      );

      TopRect = new Rectangle(
        (int)Rect.Left,
        (int)Rect.Top,
        (int)Rect.Width,
        (int)Sizes.Top
      );

      RightRect = new Rectangle(
        (int)(Rect.Left + Rect.Width - Sizes.Right),
        (int)Rect.Top,
        (int)Sizes.Right,
        (int)Rect.Height
      );

      BottomRect = new Rectangle(
        (int)Rect.Left,
        (int)(Rect.Top + Rect.Height - Sizes.Bottom),
        (int)Rect.Width,
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
  }
}