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

    public static Dictionary<int, WeakReference<CUIComponent>> ComponentsById = new();
    public static IEnumerable<CUIComponent> AllComponents => ComponentsById.Values
      .Select(wr =>
      {
        wr.TryGetTarget(out CUIComponent component);
        return component;
      }).Where(c => c != null);


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