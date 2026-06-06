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
  public class __CUISpriteBatch : CUISpriteBatch
  {
    public SpriteBatch XNASpriteBatch { get; set; }
    public Rectangle ScissorRect => XNASpriteBatch.GraphicsDevice.ScissorRectangle;

    public void StopStart(Rectangle ScissorRect)
    {
      XNASpriteBatch.End();
      XNASpriteBatch.GraphicsDevice.ScissorRectangle = ScissorRect;
      XNASpriteBatch.Begin(SpriteSortMode.Deferred, rasterizerState: GameMain.ScissorTestEnable);
    }

    public void Draw(CUITexture2D texture, Rectangle destinationRectangle, Color color)
    {
      if (texture is __CUITexture2D)
      {
        XNASpriteBatch.Draw(((__CUITexture2D)texture).XNATexture, destinationRectangle, color);
      }

    }



    public __CUISpriteBatch() { }
    public __CUISpriteBatch(SpriteBatch spriteBatch) => XNASpriteBatch = spriteBatch;
  }
}