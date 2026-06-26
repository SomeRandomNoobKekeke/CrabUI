using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    public CUIPaletteRank PaletteRank
    {
      get => Styles.PaletteRank;
      set => Styles.PaletteRank = value;
    }
    public CUIPalette Palette => Styles.PaletteSlot.Palette;

    protected Style_Part Styles { get; } = new();
    public class Style_Part : Part
    {
      public CUIStylePipeline TypeSpecificStyles { get; set; }


      private CUIPaletteRank _PaletteRank;
      public CUIPaletteRank PaletteRank
      {
        get => _PaletteRank;
        set
        {
          _PaletteRank = value;
          PaletteSlot = CUICore.Palettes.FromRank(value);
        }
      }


      private CUIPaletteSlot _PaletteSlot;
      public CUIPaletteSlot PaletteSlot
      {
        get => _PaletteSlot;
        set
        {
          if (_PaletteSlot == value) return;

          if (_PaletteSlot != null) _PaletteSlot.Changed.Remove(Self);
          _PaletteSlot = value;
          if (_PaletteSlot != null) _PaletteSlot.Changed.Add(Self, ApplyTypeStyles);
        }
      }

      public void Init()
      {
        PaletteSlot = CUICore.Palettes.Primary;
        TypeSpecificStyles = CUICore.Styles.Get(Self.GetType());

        TypeSpecificStyles.Changed.Add(Self, ApplyTypeStyles);
        ApplyTypeStyles();
      }

      public void ApplyTypeStyles()
      {
        TypeSpecificStyles.Apply(Self);
      }
    }
  }
}