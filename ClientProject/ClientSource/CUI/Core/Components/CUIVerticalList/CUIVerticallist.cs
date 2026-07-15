using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUIVerticalList : CUIComponent, IComponent
  {
    protected override void InitStyle()
    {
      base.InitStyle();
      CullChildren = true;
    }

    public void Clear() => Children.Clear();
    public void Add(CUIComponent child) => Children.Add(child);

    [CUISerializableProp]
    public bool Scrollable { get; set; }
    [CUISerializableProp]
    public float TopGap { get; set; }
    public float BottomGap { get; set; }

    //FIXME doens't work with CUIDirection.Reverse
    public float Scroll
    {
      get => ChildrenOffset.Y;
      set
      {
        if (!Scrollable) return;
        ChildrenOffset = ChildrenOffset with { Y = value };
      }
    }

    protected void UpdateChildrenOffsetBounds(float totalChildrenHeight)
    {
      LayoutProps.ChildrenOffset.Bounds = new CUIBoundaries(
        minX: 0,
        maxX: 0,
        minY: Math.Min(Rect.Height - totalChildrenHeight - BottomGap, 0),
        maxY: TopGap
      );
    }

    protected CUIVerticalListLayout ListLayout;

    private void ScrollHandle(CUIComponent c, CUIMouseScrollEvent e)
    {
      Scroll += e.Scroll;
    }

    protected override void SetupLayout()
    {
      ListLayout = new CUIVerticalListLayout();
      Layout = ListLayout;
      Layout.ConnectTo(new CUIVerticalListLayout_Host_Adapter_Part() { Self = this });
    }

    public CUIVerticalList() : base()
    {
      MouseScroll += ScrollHandle;
    }
  }
}