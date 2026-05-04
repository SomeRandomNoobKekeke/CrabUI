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
  public partial class CUIComponent : IComponent
  {
    public CUIMainComponent MainComponent => MainComponentTracker.MainComponent;
    public CUIComponent Parent => Tree.Parent;


    public DragHandle DragHandle { get; } = new();

    public IReadOnlyList<CUIComponent> Children => Tree.ReadOnlyChildren;
    public void AddChild(CUIComponent child) => Tree.AddChild(child);
    public void RemoveChild(CUIComponent child) => Tree.RemoveChild(child);

    public Layout Layout
    {
      get => LayoutSlot.Layout;
      set => LayoutSlot.Layout = value;
    }

    public Color BackgroundColor
    {
      get => Visual.Background.Color;
      set => Visual.Background.Color = value;
    }

    public CUINullRect Absolute
    {
      get => LayoutProps.Absolute.Value;
      set => LayoutProps.Absolute.Value = value;
    }

    public CUINullRect Relative
    {
      get => LayoutProps.Relative.Value;
      set => LayoutProps.Relative.Value = value;
    }
  }
}