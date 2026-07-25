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
  //Note: i usually test fixes and features on CUIVerticalList, so it might get outdated
  public partial class CUIHorizontalList : CUIComponent, IComponent
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

    private void ScrollHandle(CUIVisualComponent c, CUIMouseScrollEvent e)
    {
      Scroll += e.Scroll;
    }

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