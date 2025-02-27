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
  /// Resizing components to it's Width and placing them sequentially
  /// </summary>
  public class CUIVerticalList : CUIComponent
  {
    [CUISerializable] public bool Scrollable { get; set; }
    [CUISerializable] public float ScrollSpeed { get; set; } = 1.0f;

    public float TopGap = 0;
    public float BottomGap = 10f;

    public override CUILayout Layout
    {
      get => layout;
      set
      {
        layout = new CUILayoutVerticalList();
        layout.Host = this;
      }
    }
    public CUILayoutVerticalList ListLayout => (CUILayoutVerticalList)Layout;


    [CUISerializable]
    public CUIDirection Direction
    {
      get => ListLayout.Direction;
      set => ListLayout.Direction = value;
    }

    //TODO test, sync with hlist
    [CUISerializable]
    public float Gap
    {
      get => ListLayout.Gap;
      set => ListLayout.Gap = value;
    }

    [CUISerializable]
    public bool ResizeToHostWidth
    {
      get => ListLayout.ResizeToHostWidth;
      set => ListLayout.ResizeToHostWidth = value;
    }

    public float Scroll
    {
      get => ChildrenOffset.Y;
      set
      {
        if (!Scrollable) return;
        CUIProps.ChildrenOffset.SetValue(
          ChildrenOffset with { Y = value }
        );
      }
    }

    internal override CUIBoundaries ChildOffsetBounds => new CUIBoundaries(
      minX: 0,
      maxX: 0,
      maxY: TopGap,
      minY: Math.Min(Real.Height - ListLayout.TotalHeight - BottomGap, 0)
    );


    public CUIVerticalList() : base()
    {
      CullChildren = true;


      OnScroll += (m) => Scroll += m.Scroll * ScrollSpeed;

      ChildrenBoundaries = CUIBoundaries.VerticalTube;
    }

  }
}