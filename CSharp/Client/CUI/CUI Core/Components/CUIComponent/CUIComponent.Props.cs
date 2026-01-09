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




    protected Rectangle? absolute; public Rectangle? Absolute
    {
      set
      {
        absolute = value;
        if (absolute.HasValue) Rect = absolute.Value;
      }
    }




  }
}