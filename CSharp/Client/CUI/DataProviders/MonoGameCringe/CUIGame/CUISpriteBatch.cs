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

  public partial class PolyGame
  {
    private static Func<CUISpriteBatch, PSpriteBatch> CUISpriteBatch_Reflection;
    private static PSpriteBatch Reflect(CUISpriteBatch proxy) => CUISpriteBatch_Reflection(proxy);

    public class CUISpriteBatch(PSpriteBatch self) : ICUISpriteBatch
    {
      private PSpriteBatch Self { get; } = self;
      static CUISpriteBatch() { CUISpriteBatch_Reflection = proxy => proxy.Self; }

      void ICUISpriteBatch.Draw(ICUITexture texture, Rectangle destinationRectangle, Color color)
      {
        Draw(texture as CUITexture, destinationRectangle, color);
      }
      public void Draw(CUITexture texture, Rectangle destinationRectangle, Color color)
      {
        Self.Draw(Reflect(texture), destinationRectangle, color);
      }
    }
  }


}