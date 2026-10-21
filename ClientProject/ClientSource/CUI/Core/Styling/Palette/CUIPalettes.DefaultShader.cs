using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public partial class CUIPalettes
  {
    public class DefaultShader : ICUIPaletteShader
    {
      public CUIPalette Shade1(CUIPalette palette) => palette;
      public CUIPalette Shade2(CUIPalette palette) => CUIPalette.Darken(palette, 0.2f);
      public CUIPalette Shade3(CUIPalette palette) => CUIPalette.Darken(palette, 0.4f);
      public CUIPalette Shade4(CUIPalette palette) => CUIPalette.Darken(palette, 0.6f);
      public CUIPalette Shade5(CUIPalette palette) => CUIPalette.Darken(palette, 0.8f);
    }
  }
}