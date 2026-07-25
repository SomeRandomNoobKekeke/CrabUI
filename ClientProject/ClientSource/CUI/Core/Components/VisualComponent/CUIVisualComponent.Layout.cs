using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUIVisualComponent
  {
    protected CUINullVector2 MinSize { get; set; } = CUINullVector2.Null;
    protected CUINullVector2 MaxSize { get; set; } = CUINullVector2.Null;

    //CRINGE
    protected virtual CUINullVector2 MinSizeOverride => MinSize;
    protected virtual CUINullVector2 MaxSizeOverride => MaxSize;

    public abstract Layout? Layout { get; protected set; }

    protected LayoutMarker LayoutMarker { get; set; } = new();
  }
}