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
  public partial class CUIVerticalList : CUIComponent, IComponent
  {
    protected Adapters_Part Adapters { get; } = new();

    protected partial class Adapters_Part : Part
    {
      public Layout_Adapter_Part LayoutAdapter { get; } = new();
      public partial class Layout_Adapter_Part : Part, CUIVerticalListLayout.Target
      {
        CUIRect Layout.Target.Rect
        {
          get => Self.Rect;
          set => Self.Rect = value;
        }

        bool Layout.Target.CullChildren => Self.CullChildren;
        bool Layout.Target.CulledOut
        {
          get => Self.CulledOut;
          set => Self.CulledOut = value;
        }

        IReadOnlyList<Layout.Target> Layout.Target.Children => new ListProxy<CUIComponent, Layout.Target>(
          Self.Tree.Children, c => c.Adapters.Layout
        );

        void Layout.Target.NotifyVisualsRestructured() => Self.VisualRestructureNotifier.Notify();



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