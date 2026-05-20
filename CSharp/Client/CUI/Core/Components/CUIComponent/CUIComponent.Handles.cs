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
    public bool Draggable
    {
      get => DragHandle.Active;
      set => DragHandle.Active = value;
    }

    public bool Resizable
    {
      get => RightResizeHandle.Visible;
      set => RightResizeHandle.Visible = value;
    }

    public DragHandle DragHandle { get; } = new();
    public ResizeHandle RightResizeHandle { get; } = new();
  }
}