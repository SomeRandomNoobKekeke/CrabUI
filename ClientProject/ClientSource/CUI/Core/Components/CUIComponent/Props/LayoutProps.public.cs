using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using BaroJunk;

namespace CrabUI
{
  public partial class CUIComponent
  {
    [CUISerializableProp]
    public CUINullRect Absolute
    {
      get => LayoutProps.Absolute.Value;
      set => LayoutProps.Absolute.Value = value;
    }
    [CUISerializableProp]
    public CUINullRect AbsoluteMin
    {
      get => LayoutProps.AbsoluteMin.Value;
      set => LayoutProps.AbsoluteMin.Value = value;
    }

    [CUISerializableProp]
    public CUINullRect AbsoluteMax
    {
      get => LayoutProps.AbsoluteMax.Value;
      set => LayoutProps.AbsoluteMax.Value = value;
    }

    [CUISerializableProp]
    public CUINullRect Relative
    {
      get => LayoutProps.Relative.Value;
      set => LayoutProps.Relative.Value = value;
    }
    [CUISerializableProp]
    public CUINullRect RelativeMin
    {
      get => LayoutProps.RelativeMin.Value;
      set => LayoutProps.RelativeMin.Value = value;
    }
    [CUISerializableProp]
    public CUINullRect RelativeMax
    {
      get => LayoutProps.RelativeMax.Value;
      set => LayoutProps.RelativeMax.Value = value;
    }
    [CUISerializableProp]
    public CUINullRect CrossRelative
    {
      get => LayoutProps.CrossRelative.Value;
      set => LayoutProps.CrossRelative.Value = value;
    }
    [CUISerializableProp]
    public Vector2 Anchor
    {
      get => LayoutProps.Anchor.Value;
      set => LayoutProps.Anchor.Value = value;
    }
    [CUISerializableProp]
    public Vector2? ParentAnchor
    {
      get => LayoutProps.ParentAnchor.Value;
      set => LayoutProps.ParentAnchor.Value = value;
    }
    [CUISerializableProp]
    public CUIDirection Direction
    {
      get => LayoutProps.Direction.Value;
      set => LayoutProps.Direction.Value = value;
    }
    [CUISerializableProp]
    public float? Flex
    {
      get => LayoutProps.Flex.Value;
      set => LayoutProps.Flex.Value = value;
    }




    [CUISerializableProp]
    public Vector2 ChildrenOffset
    {
      get => LayoutProps.ChildrenOffset.Value;
      set => LayoutProps.ChildrenOffset.Value = value;
    }
    [CUISerializableProp]
    public CUIBoundaries ChildrenOffsetBounds
    {
      get => LayoutProps.ChildrenOffset.Bounds;
      set => LayoutProps.ChildrenOffset.Bounds = value;
    }

    [CUISerializableProp]
    public CUIBool2 FitContent
    {
      get => LayoutProps.FitContent.Value;
      set => LayoutProps.FitContent.Value = value;
    }

    [CUISerializableProp]
    public int GridRow
    {
      get => LayoutProps.GridRow.Value;
      set => LayoutProps.GridRow.Value = value;
    }

    [CUISerializableProp]
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