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
  public partial class CUIHorizontalList : CUIComponent, IComponent
  {
    public partial class CUIHorizontalListLayout_Host_Adapter_Part : Part, IAdapterPart, CUIHorizontalListLayout.Host
    {
      CUIDirection CUIHorizontalListLayout.Host.Direction => Self.LayoutProps.Direction.Value;

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
        get => Self.MinSize;
        set => Self.MinSize = value;
      }
      CUINullVector2 Layout.Host.MaxSize
      {
        get => Self.MaxSize;
        set => Self.MaxSize = value;
      }

      void Layout.Host.NotifyVisualsRestructured() => Self.VisualRestructureNotifier.Notify();
    }

  }
}