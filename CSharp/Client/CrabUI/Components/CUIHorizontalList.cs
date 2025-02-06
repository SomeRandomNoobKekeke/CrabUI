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
    [CUISerializable] public bool Scrollable { get; set; }
    [CUISerializable] public float ScrollSpeed { get; set; } = 1.0f;

    public float LeftGap = 0f;
    public float RightGap = 0f;

    public override CUILayout Layout
    {
      get => layout;
      set
      {
        layout = new CUILayoutHorizontalList();
        layout.Host = this;
      }
    }
    public CUILayoutHorizontalList ListLayout => (CUILayoutHorizontalList)Layout;

    [CUISerializable]
    public CUIDirection Direction
    {
      get => ListLayout.Direction;
      set => ListLayout.Direction = value;
    }

    [CUISerializable]
    public bool ResizeToHostHeight
    {
      get => ListLayout.ResizeToHostHeight;
      set => ListLayout.ResizeToHostHeight = value;
    }

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
      maxX: Math.Min(Real.Width - ListLayout.TotalWidth - RightGap, 0)
    );
    public CUIHorizontalList() : base()
    {
      CullChildren = true;


      OnScroll += (m) => Scroll += m.Scroll * ScrollSpeed;
      ChildrenBoundaries = CUIBoundaries.HorizontalTube;
    }
  }
}