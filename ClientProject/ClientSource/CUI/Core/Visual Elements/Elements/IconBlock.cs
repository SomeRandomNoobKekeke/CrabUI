using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUILibs;

namespace CursedUI
{
  public partial class IconBlock : VisualElementBase
  {
    private CUISprite _Icon = CUISprite.Transparent; public CUISprite Icon
    {
      get => _Icon;
      set
      {
        _Icon = value;
        RecalcIconRectangle();
      }
    }

    private CUIRect _Rect; public CUIRect Rect
    {
      get => _Rect;
      set
      {
        _Rect = value;
        RecalcIconRectangle();
      }
    }

    private float _Scale = 1.0f; public float Scale
    {
      get => _Scale;
      set
      {
        _Scale = Math.Max(0, value);
        RecalcIconRectangle();
      }
    }

    private Vector2 _Anchor = new Vector2(0.5f, 0.5f); public Vector2 Anchor
    {
      get => _Anchor;
      set
      {
        _Anchor = value;
        RecalcIconRectangle();
      }
    }

    public Color Color
    {
      get => Icon.Color;
      set => Icon.Color = value;
    }

    public Vector2 ForcedSize { get; private set; }

    public Rectangle IconRectangle { get; private set; }
    private void RecalcIconRectangle()
    {
      IconRectangle = new CUIRect(
        CUIAnchor.ChildPosIn(Rect, Anchor, Icon.Size.ToVector2() * Scale),
        Icon.Size.ToVector2() * Scale
      ).Round();

      ForcedSize = Icon.Size.ToVector2() * Scale;
    }

    public override bool Contains(Vector2 pos) => Rect.Contains(pos);

    public override void Draw(CUISpriteBatch spriteBatch)
    {
      Icon.Draw(spriteBatch, IconRectangle);
    }
  }
}