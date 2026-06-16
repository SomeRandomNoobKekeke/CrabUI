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
    public partial class CUIVerticalListLayout_Host_Adapter_Part : Part, IAdapterPart, CUIVerticalListLayout.Host
    {
      CUIDirection CUIVerticalListLayout.Host.Direction => Self.LayoutProps.Direction.Value;

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
    }

  }
}