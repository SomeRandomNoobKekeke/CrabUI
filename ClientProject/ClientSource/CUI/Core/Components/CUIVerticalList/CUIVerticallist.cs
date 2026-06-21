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
  public partial class CUIVerticalList : CUIComponent, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIVerticalList>((c) =>
    {
      c.CullChildren = true;
    });

    public void Clear() => RemoveAllChildren();
    public void Add(CUIComponent child) => Append(child);

    [CUISerializable]
    public bool Scrollable { get; set; }
    [CUISerializable]
    public float TopGap { get; set; }
    public float BottomGap { get; set; }

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

    protected override void UpdateRect(CUIRect rect)
    {
      base.UpdateRect(rect);
    }

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