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
  public partial class CUIVisualComponent
  {

    protected virtual void InitStyle() { }

    //TODO mb this should be deep by default
    public CUIPalette Palette
    {
      get => Styles.Palette;
      set => Styles.Palette = value;
    }

    public CUIPalette DeepPalette
    {
      get => Styles.Palette;
      set
      {
        Styles.Palette = value;
        foreach (CUIVisualComponent child in Children)
        {
          child.DeepPalette = value;
        }
      }
    }


    public Action<CUIVisualComponent> Style
    {
      set => PersonalStyle = new CUIActionStyle<CUIVisualComponent>("personal", value);
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
      public CUIStylePipeline TypeSpecificStyles { get; private set; }


      private bool _UseReactiveStyles; public bool UseReactiveStyles
      {
        get => _UseReactiveStyles;
        set
        {
          if (value == _UseReactiveStyles) return;

          bool prevValue = _UseReactiveStyles;
          _UseReactiveStyles = value;


          if (!prevValue && value)
          {
            TypeSpecificStyles.Changed += ReapplyStyles;
            Palette.Changed += ReapplyStyles;
          }

          if (prevValue && !value)
          {
            TypeSpecificStyles.Changed -= ReapplyStyles;
            Palette.Changed -= ReapplyStyles;
          }
        }
      }

      private CUIPalette _Palette; public CUIPalette Palette
      {
        get => _Palette;
        set
        {
          // if (_Palette == value) return;

          if (_Palette != null) _Palette.Changed -= ReapplyStyles;
          _Palette = value;
          if (UseReactiveStyles && _Palette != null) _Palette.Changed += ReapplyStyles;

          ReapplyStyles();
        }
      }



      public void Init()
      {
        _Palette = CUICore.Palettes.Primary;
        TypeSpecificStyles = CUICore.Styles.GetOrCreatePipeline(Self.GetType());

        Self.InitStyle(); // 1 time
        ReapplyStyles();

        UseReactiveStyles = CUICore.Styles.UseReactiveStyles;
      }

      public void ReapplyStyles()
      {
        TypeSpecificStyles.Apply(Self);
        Self.PersonalStyle?.Apply(Self);
      }
    }
  }
}