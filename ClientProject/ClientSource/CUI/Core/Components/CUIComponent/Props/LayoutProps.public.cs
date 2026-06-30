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
    [CUISerializable]
    public CUINullRect Absolute
    {
      get => LayoutProps.Absolute.Value;
      set => LayoutProps.Absolute.Value = value;
    }
    [CUISerializable]
    public CUINullRect AbsoluteMin
    {
      get => LayoutProps.AbsoluteMin.Value;
      set => LayoutProps.AbsoluteMin.Value = value;
    }

    [CUISerializable]
    public CUINullRect AbsoluteMax
    {
      get => LayoutProps.AbsoluteMax.Value;
      set => LayoutProps.AbsoluteMax.Value = value;
    }

    [CUISerializable]
    public CUINullRect Relative
    {
      get => LayoutProps.Relative.Value;
      set => LayoutProps.Relative.Value = value;
    }
    [CUISerializable]
    public CUINullRect RelativeMin
    {
      get => LayoutProps.RelativeMin.Value;
      set => LayoutProps.RelativeMin.Value = value;
    }
    [CUISerializable]
    public CUINullRect RelativeMax
    {
      get => LayoutProps.RelativeMax.Value;
      set => LayoutProps.RelativeMax.Value = value;
    }
    [CUISerializable]
    public CUINullRect CrossRelative
    {
      get => LayoutProps.CrossRelative.Value;
      set => LayoutProps.CrossRelative.Value = value;
    }
    [CUISerializable]
    public CUINullRect InnerAbsolute
    {
      get => LayoutProps.InnerAbsolute.Value;
      set => LayoutProps.InnerAbsolute.Value = value;
    }
    [CUISerializable]
    public CUINullRect InnerAbsoluteMin
    {
      get => LayoutProps.InnerAbsoluteMin.Value;
      set => LayoutProps.InnerAbsoluteMin.Value = value;
    }
    [CUISerializable]
    public Vector2 Anchor
    {
      get => LayoutProps.Anchor.Value;
      set => LayoutProps.Anchor.Value = value;
    }
    [CUISerializable]
    public Vector2? ParentAnchor
    {
      get => LayoutProps.ParentAnchor.Value;
      set => LayoutProps.ParentAnchor.Value = value;
    }
    [CUISerializable]
    public CUIDirection Direction
    {
      get => LayoutProps.Direction.Value;
      set => LayoutProps.Direction.Value = value;
    }
    [CUISerializable]
    public float? Flex
    {
      get => LayoutProps.Flex.Value;
      set => LayoutProps.Flex.Value = value;
    }




    [CUISerializable]
    public Vector2 ChildrenOffset
    {
      get => LayoutProps.ChildrenOffset.Value;
      set => LayoutProps.ChildrenOffset.Value = value;
    }
    [CUISerializable]
    public CUIBoundaries ChildrenOffsetBounds
    {
      get => LayoutProps.ChildrenOffset.Bounds;
      set => LayoutProps.ChildrenOffset.Bounds = value;
    }

    [CUISerializable]
    public CUIBool2 FitContent
    {
      get => LayoutProps.FitContent.Value;
      set => LayoutProps.FitContent.Value = value;
    }

    [CUISerializable]
    public int GridRow
    {
      get => LayoutProps.GridRow.Value;
      set => LayoutProps.GridRow.Value = value;
    }

    [CUISerializable]
    public int GridColumn
    {
      get => LayoutProps.GridColumn.Value;
      set => LayoutProps.GridColumn.Value = value;
    }

    public (int, int) Grid
    {
      get => (GridColumn, GridRow);
      set => (GridColumn, GridRow) = value;
    }
  }
}