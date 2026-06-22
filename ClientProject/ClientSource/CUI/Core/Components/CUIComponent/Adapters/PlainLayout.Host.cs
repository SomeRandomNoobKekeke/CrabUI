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
      public partial class PlainLayout_Host_Part : PlainLayout.Host
      {
        public CUIComponent Self { get; set; }

        IReadOnlyList<Layout.Child> Layout.Host.Children
          => new ListProxy<CUIComponent, Layout.Child>(
            Self.Tree.Children,
            c => c.Adapters.Layout_Child
          );

        Vector2 Layout.Host.ChildrenOffset => Self.LayoutProps.ChildrenOffset.Value;
        bool Layout.Host.CullChildren => Self.CullChildren;
        CUIRect Layout.Host.Rect
        {
          get => Self.Rect;
          set => Self.Rect = value;
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

        void Layout.Host.NotifyVisualsRestructured() => Self.VisualRestructureNotifier.Notify();

        public override string ToString() => Self.ToString();
      }
    }
  }
}