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

namespace CursedUI
{
  public partial class CUIVisualComponent
  {
    protected partial class Adapters_Part : Part
    {
      public Layout_Child_Part Layout_Child { get; } = new();

      public partial class Layout_Child_Part : Part, IAdapterPart, Layout.Child
      {
        float? CUIVerticalListLayout.Child.Flex => Self.LayoutProps.Flex.Value;
        CUINullRect CUIPlainLayout.Child.Absolute => Self.LayoutProps.Absolute.Value;
        CUINullRect CUIPlainLayout.Child.AbsoluteMin => Self.LayoutProps.AbsoluteMin.Value;
        CUINullRect CUIPlainLayout.Child.AbsoluteMax => Self.LayoutProps.AbsoluteMax.Value;
        CUINullRect CUIPlainLayout.Child.Relative => Self.LayoutProps.Relative.Value;
        CUINullRect CUIPlainLayout.Child.RelativeMin => Self.LayoutProps.RelativeMin.Value;
        CUINullRect CUIPlainLayout.Child.RelativeMax => Self.LayoutProps.RelativeMax.Value;
        CUINullRect CUIPlainLayout.Child.CrossRelative => Self.LayoutProps.CrossRelative.Value;
        Vector2 CUIPlainLayout.Child.Anchor => Self.LayoutProps.Anchor.Value;
        Vector2? CUIPlainLayout.Child.ParentAnchor => Self.LayoutProps.ParentAnchor.Value;

        CUIRect Layout.ChildBase.OuterRect
        {
          get => Self.OuterRect;
          set => Self.OuterRect = value;
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

        CUISizes CUIPlainLayout.Child.OutToChildDiff => Self.OutToChildDiff;
        public override string ToString() => Self.ToString();
      }
    }
  }
}