using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIPalettes
  {
    private CUIPalette _Primary = CUIPalette.FromColor(new Color(0, 139, 246, 255)); public CUIPalette Primary
    {
      get => _Primary;
      set => _Primary.Swap(value);
    }

    private CUIPalette _Secondary = CUIPalette.FromColor(new Color(0, 99, 191, 255)); public CUIPalette Secondary
    {
      get => _Secondary;
      set => _Secondary.Swap(value);
    }

    private CUIPalette _Tertiary = CUIPalette.FromColor(new Color(0, 76, 199, 255)); public CUIPalette Tertiary
    {
      get => _Tertiary;
      set => _Tertiary.Swap(value);
    }

    private CUIPalette _Quaternary = CUIPalette.FromColor(new Color(106, 0, 255, 255)); public CUIPalette Quaternary
    {
      get => _Quaternary;
      set => _Quaternary.Swap(value);
    }
  }
}