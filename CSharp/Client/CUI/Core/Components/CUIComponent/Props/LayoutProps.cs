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
  public partial class CUIComponent
  {
    protected LayoutProps_Part LayoutProps { get; } = new();
    public class LayoutProps_Part : Part, ICUILayoutProp.IContainer
    {
      public void Init()
      {
        ChildrenOffset.ValueSet += (value) => RecalcRealChildrenOffset();
        ChildrenOffsetBounds.ValueSet += (value) => RecalcRealChildrenOffset();

        ChildrenOffset.Validate = ValidateChildrenOffset;

        Absolute.Debug_ValueSet.Map(Self.DebugRelays[DebugCategory.PropSet]);
        Relative.Debug_ValueSet.Map(Self.DebugRelays[DebugCategory.PropSet]);
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

      public CUILayoutProp<CUIDirection> Direction { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<float?> Flex { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<Vector2> ChildrenOffset { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public CUILayoutProp<CUIBoundaries> ChildrenOffsetBounds { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.FromParentAndDown,
      };

      public Vector2 RealChildrenOffset { get; private set; }
      private Vector2 ValidateChildrenOffset(Vector2 offset)
        => ChildrenOffsetBounds.Value.Check(offset);
      private void RecalcRealChildrenOffset()
      {
        RealChildrenOffset = ChildrenOffsetBounds.Value.Check(ChildrenOffset.Value);
      }


      public CUILayoutProp<CUIBool2> FitContent { get; } = new()
      {
        Pattern = LayoutMarker.Pattern.AbsoluteProp,
      };
    }
  }
}