using System;
using System.Collections.Generic;
using CUICodeGenerator;
using CUILibs;
using Microsoft.Xna.Framework;


namespace CursedUI
{
  public partial class CUIVisualComponent
  {
    public VisualUnit.NestedVisualComponent VisualWrapper { get; }

    private bool _Displayed = true;
    [CUISerializableProp]
    public bool Displayed
    {
      get => _Displayed;
      set
      {
        if (_Displayed == value) return;
        _Displayed = value;
        NotifyVisualsRestructured();
      }
    }

    protected bool CulledOut { get; set; }


    [CUISerializableProp]
    public bool CullChildren { get; set; }


    [CUISerializableProp]
    public abstract bool Visible { get; set; }

    public abstract float Transparency { get; set; }

    public bool TransparencyImmunity { get; set; }
    public float DeepTransparency
    {
      get => Transparency;
      set
      {
        if (!TransparencyImmunity) Transparency = value;

        foreach (var child in Children)
        {
          child.DeepTransparency = value;
        }
      }
    }

    /// <summary>
    /// Half assed substitution for z-index, set to CUIDirection.Reverse to draw children in reverse order
    /// </summary>
    [CUISerializableProp]
    public CUIDirection VisualChildrenOrder { get; set; } = CUIDirection.Straight;

    public abstract IEnumerable<VisualUnit> VisualSplit();
  }
}