using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class SimpleDrawModule : IDrawModule
  {
    public Rectangle DrawRect { get; set; }
    public CUITexture Texture { get; set; } = CUITexture.White;
    public Color Color { get; set; }

    public void Draw(CUISpriteBatch spriteBatch)
    {
      spriteBatch.Draw(Texture, DrawRect, Color);
    }
  }
}