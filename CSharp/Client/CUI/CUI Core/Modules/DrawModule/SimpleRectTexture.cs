using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class SimpleRectTexture : IVisualElement
  {
    public Rectangle Rect { get; set; }
    public CUITexture Texture { get; set; } = CUITexture.White;
    public Color Color { get; set; }

    public void Draw(CUISpriteBatch spriteBatch)
    {
      spriteBatch.Draw(Texture, Rect, Color);
    }
  }
}