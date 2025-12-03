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
    public SpriteBatch SpriteBatch;

    public void Draw(ICUITexture texture, Rectangle destinationRectangle, Color color)
    {
      if (texture is CUITexture t)
      {
        SpriteBatch.Draw(t.Texture, destinationRectangle, color);
      }
    }

    public void Use(SpriteBatch spriteBatch) => SpriteBatch = spriteBatch;
    public CUISpriteBatch() { }
    public CUISpriteBatch(SpriteBatch spriteBatch) => SpriteBatch = spriteBatch;
  }
}