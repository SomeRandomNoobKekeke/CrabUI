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

namespace CrabUI
{
  public class Borders : VisualElementBase, CUISerializable
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

    public CUISprite Sprite { get; set; } = CUISprite.White;
    [CUISerializableProp]
    public Color Color { get => Sprite.Color; set => Sprite.Color = value; }

    public override bool Contains(Vector2 pos)
    {
      return Rect.Contains(pos) && !InnerRect.Contains(pos);
    }

    private void UpdateRects()
    {
      InnerRect = Rect - Sizes; //TODO use rounded rect

      Rectangle rounded = Rect.Round();

      LeftRect = new Rectangle(
        rounded.X,
        rounded.Y,
        (int)Sizes.Left,
        rounded.Height
      );

      TopRect = new Rectangle(
        rounded.X,
        rounded.Y,
        rounded.Width,
        (int)Sizes.Top
      );

      RightRect = new Rectangle(
        (int)(rounded.X + rounded.Width - Sizes.Right),
        rounded.Y,
        (int)Sizes.Right,
        rounded.Height
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