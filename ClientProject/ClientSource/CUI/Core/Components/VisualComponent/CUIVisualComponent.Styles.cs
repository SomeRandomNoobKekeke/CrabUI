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
        _PersonalStyle.Apply(this); //HACK i don't need full ReapplyStyles() because PersonalStyle it always last
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

          if (!_UseReactiveStyles && value)
          {
            TypeSpecificStyles.Changed += ReapplyStyles;
            Palette.Changed += ReapplyStyles;
          }

          if (_UseReactiveStyles && !value)
          {
            TypeSpecificStyles.Changed -= ReapplyStyles;
            Palette.Changed -= ReapplyStyles;
          }

          _UseReactiveStyles = value;
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

        foreach (ICUIStyle style in CUICore.Styles.ContextStyles[Self.GetType()])
        {
          style.Apply(Self);
        }

        UseReactiveStyles = CUICore.Styles.UseReactiveStyles;
      }

      private int recDepth;
      public void ReapplyStyles()
      {
        recDepth++;
        if (recDepth > 2)  // this can happen e.g. if you set Palette in style
        {
          CUI.Logger.Warning($"Recursion in [{Self}] styles");
          CUI.Logger.PrintStackTrace();
          recDepth = 0;
          return;
        }

        try
        {
          TypeSpecificStyles.Apply(Self);
          Self.PersonalStyle?.Apply(Self);
        }
        finally
        {
          recDepth--;
        }
      }
    }
  }
}