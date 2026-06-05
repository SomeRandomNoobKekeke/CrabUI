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
      public partial class Layout_Adapter : CUIVerticalListLayout.Target
      {
        CUIRect CUIVerticalListLayout.Target.Rect
        {
          get => Self.Rect;
          set => Self.Rect = value;
        }
        CUINullRect CUIVerticalListLayout.Target.Absolute => Self.LayoutProps.Absolute.Value;
        CUINullRect CUIVerticalListLayout.Target.AbsoluteMin => Self.LayoutProps.AbsoluteMin.Value;
        CUINullRect CUIVerticalListLayout.Target.AbsoluteMax => Self.LayoutProps.AbsoluteMax.Value;
        CUINullRect CUIVerticalListLayout.Target.Relative => Self.LayoutProps.Relative.Value;
        CUINullRect CUIVerticalListLayout.Target.RelativeMin => Self.LayoutProps.RelativeMin.Value;
        CUINullRect CUIVerticalListLayout.Target.RelativeMax => Self.LayoutProps.RelativeMax.Value;
        CUINullRect CUIVerticalListLayout.Target.CrossRelative => Self.LayoutProps.CrossRelative.Value;

        CUINullVector2 CUIVerticalListLayout.Target.MinSize
        {
          get => Self.MinSizeOverride;
          set => Self.MinSize = value;
        }

        CUINullVector2 CUIVerticalListLayout.Target.MaxSize
        {
          get => Self.MaxSizeOverride;
          set => Self.MaxSize = value;
        }

        CUIBool2 CUIVerticalListLayout.Target.FitContent => Self.FitContent;


        CUIDirection CUIVerticalListLayout.Target.Direction
          => Self.LayoutProps.Direction.Value;

        float? CUIVerticalListLayout.Target.Flex => Self.LayoutProps.Flex.Value;

        IReadOnlyList<CUIVerticalListLayout.Target> CUIVerticalListLayout.Target.Children
          => new ListProxy<CUIComponent, CUIVerticalListLayout.Target>(
            Self.Tree.Children, c => c.Adapters.Layout
          );

        Vector2 CUIVerticalListLayout.Target.ChildrenOffset => Self.LayoutProps.ChildrenOffset.Value;
      }
    }
  }
}