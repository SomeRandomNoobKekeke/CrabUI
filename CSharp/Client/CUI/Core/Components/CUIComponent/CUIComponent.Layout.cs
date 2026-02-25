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
    private LayoutSlot LayoutSlot { get; } = new();
    private LayoutMarker LayoutMarker { get; } = new();
    public Layout Layout
    {
      get => LayoutSlot?.Layout;
      set => LayoutSlot.Layout = value;
    }


  }
}