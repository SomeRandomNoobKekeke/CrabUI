using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIHorizontalList : CUIComponent, IComponent
  {
    public void Clear() => RemoveAllChildren();
    public void Add(CUIComponent child) => Append(child);

    [CUISerializable]
    public bool Scrollable { get; set; }
    [CUISerializable]
    public float TopGap { get; set; }
    public float BottomGap { get; set; }

    public float Scroll
    {
      get => ChildrenOffset.X;
      set
      {
        if (!Scrollable) return;
        ChildrenOffset = ChildrenOffset with { X = value };
      }
    }

    protected CUIHorizontalListLayout ListLayout;

    protected override void UpdateRect(CUIRect rect)
    {
      base.UpdateRect(rect);

      // LayoutProps.ChildrenOffset.Bounds = new CUIBoundaries(
      //   minX: 0,
      //   maxX: 0,
      //   minY: Math.Min(Rect.Height - ListLayout.TotalHeight - BottomGap, 0),
      //   maxY: TopGap
      // );
    }

    private void ScrollHandle(CUIComponent c, CUIMouseScrollEvent e)
    {
      Scroll += e.Scroll;
    }

    protected override void SetupLayout()
    {
      ListLayout = new CUIHorizontalListLayout();
      Layout = ListLayout;
      Layout.ConnectTo(new CUIHorizontalListLayout_Host_Adapter_Part() { Self = this });
    }

    public CUIHorizontalList() : base()
    {
      MouseScroll += ScrollHandle;
    }
  }
}