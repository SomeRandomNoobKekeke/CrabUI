using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected partial class Adapters_Part : Part
    {
      public partial class PlainLayout_Host_Part : PlainLayout.Host
      {
        public CUIComponent Self { get; set; }

        IReadOnlyList<Layout.Child> Layout.Host.Children
          => Self.Children.ReadOnlyAs<CUIComponent, Layout.Child>(c => c.Adapters.Layout_Child);

        Vector2 Layout.Host.ChildrenOffset => Self.LayoutProps.ChildrenOffset.Value;
        bool Layout.Host.CullChildren => Self.CullChildren;
        CUIRect Layout.Host.Rect
        {
          get => Self.Rect;
          set => Self.Rect = value;
        }
        CUIRect Layout.Host.OuterRect
        {
          get => Self.OuterRect;
          set => Self.OuterRect = value;
        }
        CUIRect Layout.Host.ChildrenRect
        {
          get => Self.ChildrenRect;
        }

        CUIBool2 Layout.Host.FitContent => Self.LayoutProps.FitContent.Value;
        CUINullVector2 Layout.Host.MinSize
        {
          get => Self.MinSizeOverride;
          set => Self.MinSize = value;
        }
        CUINullVector2 Layout.Host.MaxSize
        {
          get => Self.MaxSizeOverride;
          set => Self.MaxSize = value;
        }
        Func<CUIRect, CUIBoundaries> PlainLayout.Host.ChildrenBounds => Self.LayoutProps.ChildrenBounds.Value;

        void Layout.Host.NotifyVisualsRestructured() => Self.VisualRestructureNotifier.Notify();

        public override string ToString() => Self.ToString();
      }
    }
  }
}