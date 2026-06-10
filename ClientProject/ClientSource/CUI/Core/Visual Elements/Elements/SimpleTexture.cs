using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;

namespace CrabUI
{
  public class SimpleTexture : VisualElementBase, IVisualElement
  {
    public CUIRect Rect { get; set; }
    // public CUITexture2D Texture { get; set; } = CUITexture2D.White;
    public CUISprite Sprite { get; set; } = CUISprite.White;
    public Color Color { get; set; }

    public void Draw(CUISpriteBatch spriteBatch)
    {
      Sprite.Draw(spriteBatch, Rect.Box, Color);
    }
  }
}