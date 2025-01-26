using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Resizing components to it's Height and placing them sequentially
  /// </summary>
  public class CUIHorizontalList : CUIComponent
  {
    public bool Scrollable { get; set; }

    /// <summary>
    /// How far you can scroll beyond the edge
    /// </summary>
    public float LeftGap = 0f;
    /// <summary>
    /// How far you can scroll beyond the edge
    /// </summary>
    public float RightGap = 0f;

    private CUILayoutHorizontalList listLayout;

    public CUIDirection Direction
    {
      get => listLayout.Direction;
      set => listLayout.Direction = value;
    }


    /// <summary>
    /// Uses ChildrenOffset internally
    /// </summary>
    public float Scroll
    {
      get => ChildrenOffset.X;
      set
      {
        if (!Scrollable) return;
        CUIProps.ChildrenOffset.SetValue(
          ChildrenOffset with { X = value }
        );
      }
    }

    internal override CUIBoundaries ChildOffsetBounds => new CUIBoundaries(
      minY: 0,
      maxY: 0,
      minX: LeftGap,
      maxX: Math.Min(Real.Width - listLayout.TotalWidth - RightGap, 0)
    );
    public CUIHorizontalList() : base()
    {
      CullChildren = true;

      listLayout = new CUILayoutHorizontalList();
      Layout = listLayout;

      OnScroll += (m) => Scroll += m.Scroll;

      BackgroundColor = Color.Transparent;
      // BorderColor = Color.Transparent;

      ChildrenBoundaries = CUIBoundaries.HorizontalTube;
    }
  }
}