using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIPalettes
  {
    private CUIPalette _Primary = CUIPalette.FromColor(new Color(200, 0, 0)); public CUIPalette Primary
    {
      get => _Primary;
      set => _Primary.Swap(value);
    }

    private CUIPalette _Secondary = CUIPalette.FromColor(new Color(200, 0, 0)); public CUIPalette Secondary
    {
      get => _Secondary;
      set => _Secondary.Swap(value);
    }
  }
}