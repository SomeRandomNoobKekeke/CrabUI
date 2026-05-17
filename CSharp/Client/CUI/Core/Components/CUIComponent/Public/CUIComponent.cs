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


    public CUIRect Rect => FunnyProps.Rect.Value;

    public bool Draggable
    {
      get => DragHandle.Active;
      set => DragHandle.Active = value;
    }

    public bool MouseOver => Events.MouseOver;
    public bool MousePressed => Events.MousePressed;


    public Layout Layout
    {
      get => LayoutSlot.Layout;
      set => LayoutSlot.Layout = value;
    }

    public Color BackgroundColor
    {
      get => Background.Color;
      set => Background.Color = value;
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

    public Vector2 Anchor
    {
      get => LayoutProps.Anchor.Value;
      set => LayoutProps.Anchor.Value = value;
    }

    public Vector2? ParentAnchor
    {
      get => LayoutProps.ParentAnchor.Value;
      set => LayoutProps.ParentAnchor.Value = value;
    }

    public float? Flex
    {
      get => LayoutProps.Flex.Value;
      set => LayoutProps.Flex.Value = value;
    }
  }
}