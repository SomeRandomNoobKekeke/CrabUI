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
  public interface CUISpriteBatch
  {
    public void Draw(CUITexture2D texture, Rectangle destinationRectangle, Color color);
    public Rectangle ScissorRect { get; }
    public void StopStart(Rectangle ScissorRect);
  }

}