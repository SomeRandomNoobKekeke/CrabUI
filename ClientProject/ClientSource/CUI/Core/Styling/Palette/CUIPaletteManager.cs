using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIPalettes
  {
    private CUIPalette _Primary = CUIPalette.FromColor(new Color(64, 64, 64)); public CUIPalette Primary
    {
      get => _Primary;
      set => _Primary.Swap(value);
    }

    private CUIPalette _Secondary = CUIPalette.FromColor(new Color(0, 128, 90)); public CUIPalette Secondary
    {
      get => _Secondary;
      set => _Secondary.Swap(value);
    }

    private CUIPalette _Tertiary = CUIPalette.FromColor(new Color(0, 128, 128)); public CUIPalette Tertiary
    {
      get => _Tertiary;
      set => _Tertiary.Swap(value);
    }

    private CUIPalette _Quaternary = CUIPalette.FromColor(new Color(0, 0, 128)); public CUIPalette Quaternary
    {
      get => _Quaternary;
      set => _Quaternary.Swap(value);
    }
  }
}