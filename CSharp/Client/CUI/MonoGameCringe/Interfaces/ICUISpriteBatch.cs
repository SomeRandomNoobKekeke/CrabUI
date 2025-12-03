using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public interface ICUISpriteBatch
  {
    public void Draw(ICUITexture texture, Rectangle destinationRectangle, Color color);
  }
}