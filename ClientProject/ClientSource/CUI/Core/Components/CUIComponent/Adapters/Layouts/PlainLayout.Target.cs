using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected partial class Adapters_Part : Part
    {
      public partial class Layout_Adapter : PlainLayout.Target
      {
        CUINullRect PlainLayout.Target.Absolute => Self.LayoutProps.Absolute.Value;
        CUINullRect PlainLayout.Target.AbsoluteMin => Self.LayoutProps.AbsoluteMin.Value;
        CUINullRect PlainLayout.Target.AbsoluteMax => Self.LayoutProps.AbsoluteMax.Value;
        CUINullRect PlainLayout.Target.Relative => Self.LayoutProps.Relative.Value;
        CUINullRect PlainLayout.Target.RelativeMin => Self.LayoutProps.RelativeMin.Value;
        CUINullRect PlainLayout.Target.RelativeMax => Self.LayoutProps.RelativeMax.Value;
        CUINullRect PlainLayout.Target.CrossRelative => Self.LayoutProps.CrossRelative.Value;

        CUINullVector2 PlainLayout.Target.MinSize
        {
          get => Self.MinSizeOverride;
          set => Self.MinSize = value;
        }

        CUINullVector2 PlainLayout.Target.MaxSize
        {
          get => Self.MaxSizeOverride;
          set => Self.MaxSize = value;
        }

        CUIBool2 PlainLayout.Target.FitContent => Self.FitContent;

        Vector2 PlainLayout.Target.Anchor => Self.LayoutProps.Anchor.Value;
        Vector2? PlainLayout.Target.ParentAnchor => Self.LayoutProps.ParentAnchor.Value;
        Vector2 PlainLayout.Target.ChildrenOffset => Self.LayoutProps.ChildrenOffset.Value;
        IReadOnlyList<PlainLayout.Target> PlainLayout.Target.Children
          => new ListProxy<CUIComponent, PlainLayout.Target>(
            Self.Tree.Children, c => c.Adapters.Layout
          );

      }
    }
  }
}