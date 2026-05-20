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

      public CUILayoutProp<CUINullRect> Relative { get; } = new()
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
    }
  }
}