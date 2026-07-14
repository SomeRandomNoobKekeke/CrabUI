using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using BaroJunk;

namespace CrabUI
{
  public class CUIVerticalListLayoutTest : CUILayoutTest
  {
    public class LayoutHost : CUIVerticalListLayout.Host
    {
      public LayoutHost()
      {
        Layout.ConnectTo(this);
      }

      public CUIVerticalListLayout Layout { get; } = new();

      public CUIDirection Direction { get; set; }
      public float TotalHeight { set { } }
      public IReadOnlyList<Layout.Child> Children { get; set; }
      public Vector2 ChildrenOffset { get; set; }
      public bool CullChildren { get; set; }
      public CUIRect Rect { get; set; }
      public CUIRect OuterRect { get; set; }
      public CUIRect ChildrenRect { get; set; }
      public CUIBool2 FitContent { get; set; }
      public CUINullVector2 MinSize { get; set; }
      public CUINullVector2 MaxSize { get; set; }

      public void NotifyVisualsRestructured() { }
    }
    public class LayoutChild : Layout.Child
    {
      public float? Flex { get; set; }
      public CUIRect Rect { get; set; }
      public CUIRect OuterRect { get; set; }
      public CUIRect ChildrenRect { get; }
      public bool CulledOut { get; set; }
      public CUINullVector2 MinSize { get; set; }
      public CUINullVector2 MaxSize { get; set; }
      public CUINullRect Absolute { get; set; }
      public CUINullRect AbsoluteMin { get; set; }
      public CUINullRect AbsoluteMax { get; set; }
      public CUINullRect Relative { get; set; }
      public CUINullRect RelativeMin { get; set; }
      public CUINullRect RelativeMax { get; set; }
      public CUINullRect CrossRelative { get; set; }
      public CUISizes OutToChildDiff { get; set; }
      public Vector2 Anchor { get; set; }
      public Vector2? ParentAnchor { get; set; }
      public int GridRow { get; set; }
      public int GridColumn { get; set; }
    }


    public LayoutHost Host { get; set; }

    public UListTest Test(LayoutHost host, List<LayoutChild> children, List<CUIRect> rects)
    {
      host.Children = children.AsReadOnly();

      host.Layout.RequireChildrenUpdate = true;
      host.Layout.UpdateChildren();

      return new UListTest(host.Children.Select(c => c.OuterRect), rects);
    }

    public UListTest Mixed() => Test(
      new LayoutHost() { ChildrenRect = new CUIRect(0, 0, 100, 100), },
      new List<LayoutChild>()
      {
        new LayoutChild(){Flex = 1, Absolute = new CUINullRect(w: 33)},
        new LayoutChild(){Relative = new CUINullRect(w: 0.5f, h: 0.25f)},
        new LayoutChild(){Flex = 2},
      },
      new List<CUIRect>()
      {
        new CUIRect(0,0,33,25),
        new CUIRect(0,25,50,25),
        new CUIRect(0,50,100,50),
      }
    );

    public UListTest RelativePrecision() => Test(
      new LayoutHost() { ChildrenRect = new CUIRect(0, 0, 100, 100), },
      new List<LayoutChild>()
      {
        new LayoutChild(){Relative = new CUINullRect(h: 1.0f/3.0f)},
        new LayoutChild(){Relative = new CUINullRect(h: 1.0f/3.0f)},
        new LayoutChild(){Relative = new CUINullRect(h: 1.0f/3.0f)},
      },
      new List<CUIRect>()
      {
        new CUIRect(0, 0                    , 100, 100.0f * (1.0f/ 3.0f)),
        new CUIRect(0, 100.0f * (1.0f/ 3.0f), 100, 100.0f * (1.0f/ 3.0f)),
        new CUIRect(0, 200.0f * (1.0f/ 3.0f), 100, 100.0f * (1.0f/ 3.0f)),
      }
    );

    public UListTest FlexPrecision() => Test(
      new LayoutHost() { ChildrenRect = new CUIRect(0, 0, 100, 100), },
      new List<LayoutChild>()
      {
        new LayoutChild(){ Flex = 1 },
        new LayoutChild(){ Flex = 1 },
        new LayoutChild(){ Flex = 1 },
      },
      new List<CUIRect>()
      {
        new CUIRect(0, 0          , 100, 100.0f/3.0f),
        new CUIRect(0, 100.0f/3.0f, 100, 100.0f/3.0f),
        new CUIRect(0, 200.0f/3.0f, 100, 100.0f - 100.0f/3.0f - 100.0f/3.0f),
      }
    );
  }
}