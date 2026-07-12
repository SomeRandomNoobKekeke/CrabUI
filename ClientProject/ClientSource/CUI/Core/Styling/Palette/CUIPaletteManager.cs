using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  public class CUIPaletteManager
  {
    public CUIPaletteSlot FromRank(CUIPaletteRank rank) => rank switch
    {
      CUIPaletteRank.Primary => Primary,
      CUIPaletteRank.Secondary => Secondary,
      CUIPaletteRank.Personal => new CUIPaletteSlot(),
    };

    public CUIPaletteSlot Primary { get; } = new();
    public CUIPaletteSlot Secondary { get; } = new();
  }
}