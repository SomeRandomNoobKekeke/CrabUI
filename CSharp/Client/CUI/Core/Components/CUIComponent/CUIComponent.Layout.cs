using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {
    //TODO these props should be separated, currently they can be set by component and by layout
    protected virtual CUINullVector2 MinSize => CUINullVector2.Null;
    protected CUINullVector2 MaxSize { get; set; } = CUINullVector2.Null;

    public Layout Layout
    {
      get => LayoutSlot.Layout;
      set => LayoutSlot.Layout = value;
    }

    protected LayoutSlot LayoutSlot { get; set; } = new();
    protected LayoutMarker LayoutMarker { get; set; } = new();
  }
}