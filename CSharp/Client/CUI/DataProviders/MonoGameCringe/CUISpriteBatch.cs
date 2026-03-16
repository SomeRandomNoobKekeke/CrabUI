using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public class CUISpriteBatch : ICUISpriteBatch
  {
    public SpriteBatch XNASpriteBatch { get; set; }

    public void Draw(CUITexture2D texture, Rectangle destinationRectangle, Color color)
    {
      if (texture.HasData)
      {
        XNASpriteBatch.Draw(texture.XNATexture, destinationRectangle, color);
      }
    }

    public CUISpriteBatch() { }
    public CUISpriteBatch(SpriteBatch spriteBatch) => XNASpriteBatch = spriteBatch;
  }
}