using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
namespace CrabUI
{
  public partial class CUIComponent
  {
    public CUIPaletteRank PaletteRank
    {
      get => Styles.PaletteRank;
      set => Styles.PaletteRank = value;
    }

    //TODO mb this should be deep by default
    public CUIPalette Palette
    {
      get => Styles.PaletteSlot.Palette;
      set => Styles.PaletteSlot.Palette = value;
    }


    public CUIPalette DeepPalette
    {
      get => Styles.PaletteSlot.Palette;
      set
      {
        Styles.PaletteSlot.Palette = value;
        foreach (CUIComponent child in Children)
        {
          child.DeepPalette = value;
        }
      }
    }


    public virtual Action<CUIComponent> Style
    {
      set => PersonalStyle = new CUIActionStyle<CUIComponent>("personal", value);
    }

    private ICUIStyle _PersonalStyle; public ICUIStyle PersonalStyle
    {
      get => _PersonalStyle;
      set
      {
        _PersonalStyle = value;
        _PersonalStyle.Apply(this);
      }
    }

    public bool UseReactiveStyles
    {
      get => Styles.UseReactiveStyles;
      set => Styles.UseReactiveStyles = value;
    }

    protected Style_Part Styles { get; } = new();
    public class Style_Part : Part
    {
      public CUIStylePipeline TypeSpecificStyles { get; set; }


      private bool _UseReactiveStyles; public bool UseReactiveStyles
      {
        get => _UseReactiveStyles;
        set
        {
          _UseReactiveStyles = value;
          if (UseReactiveStyles)
          {
            TypeSpecificStyles.Changed.Add(Self, ApplyTypeStyles);
            _PaletteSlot?.Changed.Add(Self, ApplyTypeStyles);
          }
          else
          {
            TypeSpecificStyles.Changed.Remove(Self);
            _PaletteSlot?.Changed.Remove(Self);
          }
        }
      }

      private CUIPaletteRank _PaletteRank; public CUIPaletteRank PaletteRank
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

          _PaletteSlot?.Changed.Remove(Self);

          _PaletteSlot = value;

          if (UseReactiveStyles)
          {
            _PaletteSlot?.Changed.Add(Self, ApplyTypeStyles);
          }
        }
      }

      public void Init()
      {
        _PaletteSlot = new CUIPaletteSlot() { Palette = CUIPalette.Default };//CUICore.Palettes.Primary;
        TypeSpecificStyles = CUICore.Styles.GetOrCreatePipeline(Self.GetType());

        Self.InitStyle();
        ApplyTypeStyles();

        UseReactiveStyles = CUICore.Styles.UseReactiveStyles;
      }

      public void ApplyTypeStyles()
      {
        TypeSpecificStyles.Apply(Self);
        Self.PersonalStyle?.Apply(Self);
      }
    }
  }
}