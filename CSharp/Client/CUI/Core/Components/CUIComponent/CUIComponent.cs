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
  public partial class CUIComponent : CUIVisualComponent, IComponent
  {
    public class Part : IPart { public CUIComponent Self { get; set; } }


    protected LayoutSlot LayoutSlot { get; set; } = new();
    protected LayoutMarker LayoutMarker { get; set; } = new();
    public DragHandle DragHandle { get; } = new();

    public CUIComponent() : base()
    {
      LayoutSlot.Layout = new PlainLayout();
    }

    public override string ToString() => $"{this.GetType().Name}:{ID}:{AKA}";
  }
}