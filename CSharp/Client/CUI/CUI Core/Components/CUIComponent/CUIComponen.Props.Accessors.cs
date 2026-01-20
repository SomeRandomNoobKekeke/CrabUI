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
    protected CUIRect Rect
    {
      get => CUIProps.Rect.Value;
      set => CUIProps.Rect.Value = value;
    }

    public Color BackgroundColor
    {
      get => Background.Color;
      set => Background.Color = value;
    }

    public CUINullRect Relative
    {
      get => CUIProps.Relative.Value;
      set => CUIProps.Relative.Value = value;
    }

    public CUINullRect Absolute
    {
      get => CUIProps.Absolute.Value;
      set => CUIProps.Absolute.Value = value;
    }


  }
}