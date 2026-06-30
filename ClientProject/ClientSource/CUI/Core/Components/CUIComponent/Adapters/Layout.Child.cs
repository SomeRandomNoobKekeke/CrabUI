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
      public Layout_Child_Part Layout_Child { get; } = new();

      public partial class Layout_Child_Part : Part, IAdapterPart, Layout.Child
      {
        float? CUIVerticalListLayout.Child.Flex => Self.LayoutProps.Flex.Value;
        CUINullRect PlainLayout.Child.Absolute => Self.LayoutProps.Absolute.Value;
        CUINullRect PlainLayout.Child.AbsoluteMin => Self.LayoutProps.AbsoluteMin.Value;
        CUINullRect PlainLayout.Child.AbsoluteMax => Self.LayoutProps.AbsoluteMax.Value;
        CUINullRect PlainLayout.Child.Relative => Self.LayoutProps.Relative.Value;
        CUINullRect PlainLayout.Child.RelativeMin => Self.LayoutProps.RelativeMin.Value;
        CUINullRect PlainLayout.Child.RelativeMax => Self.LayoutProps.RelativeMax.Value;
        CUINullRect PlainLayout.Child.CrossRelative => Self.LayoutProps.CrossRelative.Value;
        Vector2 PlainLayout.Child.Anchor => Self.LayoutProps.Anchor.Value;
        Vector2? PlainLayout.Child.ParentAnchor => Self.LayoutProps.ParentAnchor.Value;
        CUIRect Layout.ChildBase.Rect
        {
          get => Self.Rect;
          set => Self.Rect = value;
        }
        CUIRect Layout.ChildBase.OuterRect
        {
          get => Self.OuterRect;
          set => Self.OuterRect = value;
        }
        CUIRect Layout.ChildBase.ChildrenRect
        {
          get => Self.ChildrenRect;
        }

        bool Layout.ChildBase.CulledOut
        {
          get => Self.CulledOut;
          set => Self.CulledOut = value;
        }
        CUINullVector2 Layout.ChildBase.MinSize
        {
          get => Self.MinSizeOverride;
          set => Self.MinSize = value;
        }
        CUINullVector2 Layout.ChildBase.MaxSize
        {
          get => Self.MaxSizeOverride;
          set => Self.MaxSize = value;
        }
        int CUIGridLayout.Child.GridRow => Self.GridRow;
        int CUIGridLayout.Child.GridColumn => Self.GridColumn;

        CUISizes PlainLayout.Child.ChildRectDiff => Self.ChildRectDiff;
        CUISizes PlainLayout.Child.InnerRectDiff => Self.InnerRectDiff;

        public override string ToString() => Self.ToString();
      }
    }
  }
}