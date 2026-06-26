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
    protected Style_Part Styles { get; } = new();
    public class Style_Part : Part
    {
      public CUIStylePipeline TypeSpecificStyles { get; set; }

      public void Init()
      {
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