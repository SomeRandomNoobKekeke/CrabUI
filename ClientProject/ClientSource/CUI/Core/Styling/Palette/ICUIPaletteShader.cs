using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public interface ICUIPaletteShader
  {
    public CUIPalette Shade1(CUIPalette palette);
    public CUIPalette Shade2(CUIPalette palette);
    public CUIPalette Shade3(CUIPalette palette);
    public CUIPalette Shade4(CUIPalette palette);
    public CUIPalette Shade5(CUIPalette palette);
  }
}