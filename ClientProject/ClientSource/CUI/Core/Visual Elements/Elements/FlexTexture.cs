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
using System.Text.Json;

namespace CrabUI
{
  public class FlexTexture : VisualElementBase
  {
    public CUIFlexSprite Sprite { get; set; } = CUIFlexSprite.White;
    private CUIFlexRect _FlexRect; public CUIFlexRect FlexRect
    {
      get => _FlexRect;
      set
      {
        _FlexRect = value;
        Rect = _FlexRect.Box;
      }
    }

    public CUIRect Rect { get; private set; }

    public override bool Contains(Vector2 pos) => Rect.Contains(pos);

    public override void Draw(CUISpriteBatch spriteBatch)
    {
      Sprite.Draw(spriteBatch, FlexRect.LT, FlexRect.RT, FlexRect.RB, FlexRect.LB);
    }
  }
}