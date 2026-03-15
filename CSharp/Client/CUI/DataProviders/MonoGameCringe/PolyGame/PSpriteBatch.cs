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
  public class PSpriteBatch(SpriteBatch spriteBatch)
  {
    public SpriteBatch SpriteBatch { get; } = spriteBatch;

    public void Draw(PTexture texture, Rectangle destinationRectangle, Color color)
    {
      SpriteBatch.Draw(texture.Texture, destinationRectangle, color);
    }
  }
}