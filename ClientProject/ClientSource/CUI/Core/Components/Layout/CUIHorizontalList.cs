using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{
  //Note: i usually test fixes and features on CUIVerticalList, so it might get outdated
  public partial class CUIHorizontalList : CUIComponent, IComponent
  {
    protected partial class CUIHorizontalListLayout_Host_Adapter_Part : Adapters_Part.CUIPlainLayout_Host_Part, CUIHorizontalListLayout.Host
    {
      private CUIHorizontalList _Self; public new CUIHorizontalList Self
      {
        get => _Self;
        set
        {
          _Self = value;
          base.Self = value;
        }
      }

      CUIDirection CUIHorizontalListLayout.Host.Direction => Self.LayoutProps.Direction.Value;
      float CUIHorizontalListLayout.Host.TotalWidth { set => Self.UpdateChildrenOffsetBounds(value); }
    }

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
    public float LeftGap { get; set; }
    [CUISerializableProp]
    public float RightGap { get; set; }

    [CUISerializableProp]
    public float Scroll
    {
      get => ChildrenOffset.X;
      set
      {
        if (!Scrollable) return;
        ChildrenOffset = ChildrenOffset with { X = value };
      }
    }

    protected void UpdateChildrenOffsetBounds(float totalChildrenHeight)
    {
      if (Direction == CUIDirection.Straight)
      {
        LayoutProps.ChildrenOffset.Bounds = new CUIBoundaries(
          minX: Math.Min(Rect.Height - totalChildrenHeight - LeftGap, 0),
          maxX: RightGap,
          minY: 0,
          maxY: 0
        );
      }
      else
      {
        LayoutProps.ChildrenOffset.Bounds = new CUIBoundaries(
          minX: -LeftGap,
          maxX: -Math.Min(Rect.Height - totalChildrenHeight - RightGap, 0),
          minY: 0,
          maxY: 0
        );
      }
    }

    private void ScrollHandle(CUIMouseScrollEvent e)
    {
      Scroll += e.Scroll;
    }

    protected CUIHorizontalListLayout ListLayout;

    [InitMethod]
    protected override void InitLayout()
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