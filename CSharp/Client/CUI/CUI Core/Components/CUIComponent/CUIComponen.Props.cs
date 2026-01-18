using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIComponent
  {
    public CUIPropsWrapper CUIProps = new();

    public class CUIPropsWrapper
    {
      public CUILayoutProp<Rectangle?> Absolute = new();
    }

    private void InjectProps()
    {
      CUIProps.Absolute.Name = "Absolute";
      CUIProps.Absolute.Host = this;

      CUIProps.Absolute.LayoutHost = this;

      CUIProps.Absolute.OnValueSet = (value) =>
      {

      };
    }

    public Color BackgroundColor
    {
      get => Background.Color;
      set => Background.Color = value;
    }

    public Rectangle? Relative
    {
      get;
      set;
    }


    public Rectangle? Absolute { get => CUIProps.Absolute.Value; set => CUIProps.Absolute.Value = value; }
  }
}