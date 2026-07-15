using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIPaletteSlot
  {
    private CUIPalette _Palette = CUIPalette.Default;
    public CUIPalette Palette
    {
      get => _Palette;
      set
      {
        _Palette = value;
        Changed.Raise();
      }
    }

    public SimpleWeakEvent Changed { get; } = new();
  }
}