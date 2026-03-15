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
    private static Func<CUITexture, PTexture> CUITexture_Reflection;
    private static PTexture Reflect(CUITexture proxy) => CUITexture_Reflection(proxy);

    public class CUITexture(PTexture self) : ICUITexture
    {
      private PTexture Self { get; } = self;
      static CUITexture() { CUITexture_Reflection = proxy => proxy.Self; }

      public static CUITexture White { get; } = new CUITexture(PTexture.White);
    }
  }


}