using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected LayoutProps_Part LayoutProps { get; } = new();
    public partial class LayoutProps_Part : Part, ICUILayoutProp.IContainer
    {
      // public DebugNode<object, string> Debug_LayoutMarked { get; } = new(
      //   DebugCategory.LayoutMarked, CUI.DebugHub,
      //   (host, propName) => $"{host}.{propName} = true"
      // );


      public void Init()
      {
        Absolute.Debug_ValueSet.Map(Self.DebugRelays[DebugCategory.LayoutPropSet]);
        Relative.Debug_ValueSet.Map(Self.DebugRelays[DebugCategory.LayoutPropSet]);
      }

      void ICUILayoutProp.IContainer.Mark(LayoutMarker.Pattern pattern)
      {
        Self.LayoutMarker.Mark(pattern);
      }

      public CUILayoutProp<CUINullRect> Absolute { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<CUINullRect> AbsoluteMin { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<CUINullRect> AbsoluteMax { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<CUINullRect> Relative { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<CUINullRect> RelativeMin { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<CUINullRect> RelativeMax { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<CUINullRect> CrossRelative { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };


      public CUILayoutProp<Vector2> Anchor { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
        DefaultValue = Vector2.Zero,
      };

      public CUILayoutProp<Vector2?> ParentAnchor { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      //TODO wait, isn't i List specific prop? why is it here?
      public CUILayoutProp<CUIDirection> Direction { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<float?> Flex { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutVectorProp ChildrenOffset { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<Func<CUIRect, CUIBoundaries>> ChildrenBounds { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<CUIBool2> FitContent { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.AbsoluteProp,
      };

      public CUILayoutProp<int> GridRow { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<int> GridColumn { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };
    }
  }
}