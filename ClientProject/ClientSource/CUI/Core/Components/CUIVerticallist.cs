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
    //GIGACRINGE (but at least it works, and gods of c# accesibility domains are not enraged)
    protected partial class CUIVerticalListLayout_Host_Adapter_Part : Adapters_Part.CUIPlainLayout_Host_Part, CUIVerticalListLayout.Host
    {
      private CUIVerticalList _Self; public new CUIVerticalList Self
      {
        get => _Self;
        set
        {
          _Self = value;
          base.Self = value;
        }
      }

      CUIDirection CUIVerticalListLayout.Host.Direction => Self.LayoutProps.Direction.Value;

      float CUIVerticalListLayout.Host.TotalHeight { set => Self.UpdateChildrenOffsetBounds(value); }
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
    public float TopGap { get; set; }
    [CUISerializableProp]
    public float BottomGap { get; set; }

    [CUISerializableProp]
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
      if (Direction == CUIDirection.Straight)
      {
        LayoutProps.ChildrenOffset.Bounds = new CUIBoundaries(
          minX: 0,
          maxX: 0,
          minY: Math.Min(Rect.Height - totalChildrenHeight - BottomGap, 0),
          maxY: TopGap
        );
      }
      else
      {
        LayoutProps.ChildrenOffset.Bounds = new CUIBoundaries(
          minX: 0,
          maxX: 0,
          minY: -TopGap,
          maxY: -Math.Min(Rect.Height - totalChildrenHeight - BottomGap, 0)
        );
      }
    }

    private void ScrollHandle(CUIVisualComponent c, CUIMouseScrollEvent e)
    {
      Scroll += e.Scroll;
    }

    protected CUIVerticalListLayout ListLayout;

    [InitMethod]
    protected override void InitLayout()
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