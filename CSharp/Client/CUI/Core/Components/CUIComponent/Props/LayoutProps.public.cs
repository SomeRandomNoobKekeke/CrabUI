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

    public Vector2 ChildrenOffset
    {
      get => LayoutProps.ChildrenOffset.Value;
      set => LayoutProps.ChildrenOffset.Value = value;
    }
  }
}