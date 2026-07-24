using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  public class CUIPalettes
  {
    private CUIPalette _Primary = CUIPalette.Red; public CUIPalette Primary
    {
      get => _Primary;
      set => _Primary.Swap(value);
    }

    private CUIPalette _Secondary = CUIPalette.Red; public CUIPalette Secondary
    {
      get => _Secondary;
      set => _Secondary.Swap(value);
    }
  }
}