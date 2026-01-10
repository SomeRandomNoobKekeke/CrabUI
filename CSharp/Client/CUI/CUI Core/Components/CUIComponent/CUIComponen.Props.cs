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
    public Color BackgroundColor
    {
      get => Background.Color;
      set => Background.Color = value;
    }

    private Rectangle? _absolute;
    public Rectangle? Absolute
    {
      get => _absolute;
      set
      {
        _absolute = value;
        MainComponent?.LayoutChanged();
      }
    }
    public Rectangle? Relative
    {
      get;
      set;
    }
  }
}