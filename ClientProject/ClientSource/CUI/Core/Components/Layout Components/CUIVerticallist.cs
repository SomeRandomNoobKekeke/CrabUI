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
    public float ScrollSpeed { get; set; } = 1.0f;

    [CUISerializableProp]
    public float Scroll
    {
      get => ChildrenOffset.Y;
      set
      {
        if (!Scrollable) return;
        ChildrenOffset = ChildrenOffset with { Y = value };
        ScrollChanged?.Invoke(Scroll);
      }
    }

    //TODO Cursed, temp event that also raises when scroll is validated
    public event Action<float> ScrollChanged;

    /// <summary>
    /// [0..1]
    /// </summary>
    public float RelativeScroll
    {
      get
      {
        float spaceToScroll = TotalContentHeight - Rect.Height;
        if (spaceToScroll == 0) return 0;
        return Math.Clamp(-Scroll / spaceToScroll, 0, 1);
      }
      set
      {
        float spaceToMove = TotalContentHeight - Rect.Height;
        Scroll = -value * spaceToMove;
      }
    }

    public float TotalContentHeight { get; private set; }

    protected void UpdateChildrenOffsetBounds(float totalContentHeight)
    {
      TotalContentHeight = totalContentHeight;

      if (Direction == CUIDirection.Straight)
      {
        LayoutProps.ChildrenOffset.Bounds = new CUIBoundaries(
          minX: 0,
          maxX: 0,
          minY: Math.Min(Rect.Height - totalContentHeight - BottomGap, 0),
          maxY: TopGap
        );
      }
      else
      {
        LayoutProps.ChildrenOffset.Bounds = new CUIBoundaries(
          minX: 0,
          maxX: 0,
          minY: -TopGap,
          maxY: -Math.Min(Rect.Height - totalContentHeight - BottomGap, 0)
        );
      }
    }

    private void HandleScroll(CUIMouseScrollEvent e)
    {
      Scroll += e.Scroll * ScrollSpeed;
    }

    private void ValidateScroll()
    {
      // if (Scroll > 0) Scroll = 0;
      if (Scroll < -TotalContentHeight)
      {
        Scroll = -TotalContentHeight;
      }
    }

    protected override void UpdateRects()
    {
      base.UpdateRects();
      ValidateScroll();
    }

    protected CUIVerticalListLayout ListLayout;

    [InitMethod]
    protected override void InitLayout()
    {
      ListLayout = new CUIVerticalListLayout();
      Layout = ListLayout;
      Layout.ConnectTo(new CUIVerticalListLayout_Host_Adapter_Part() { Self = this });

      MapLayoutDebugChannels();
    }


    public CUIVerticalList() : base()
    {
      MouseScroll += HandleScroll;
    }
  }
}