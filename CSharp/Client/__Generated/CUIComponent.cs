using System;
using Barotrauma;
using System.Linq;
using System.Collections.Generic;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {
    [Local]
    protected Self_As_CUIComponent As_CUIComponent { get; } = new();
    void IComponent.InjectModules()
    {
      As_CUIComponent.Tree.MainComponentTracker = As_CUIComponent.MainComponentTracker;
      As_CUIComponent.Visual.Tree = As_CUIComponent.Tree;
      As_CUIComponent.LayoutSlot.Host = As_CUIComponent.Adapters.Layout;
      As_CUIComponent.LayoutMarker.Host = As_CUIComponent.Adapters.LayoutMarker;
      As_CUIComponent.DragHandle.Hub = As_CUIComponent.Adapters.IDragHandleHub;
      As_CUIComponent.DragHandle.Host = As_CUIComponent.Adapters.IDraggable;
    }

    void IComponent.InjectParts()
    {
      As_CUIComponent.Self = this;

      As_CUIComponent.Adapters.Self = this;
      As_CUIComponent.Adapters.IDraggable.Self = this;
      As_CUIComponent.Adapters.IDragHandleHub.Self = this;
      As_CUIComponent.Adapters.Layout.Self = this;
      As_CUIComponent.Adapters.LayoutMarker.Self = this;
      As_CUIComponent.Events.Self = this;
      As_CUIComponent.MainComponentTracker.Self = this;
      As_CUIComponent.Tree.Self = this;
      As_CUIComponent.Visual.Self = this;
      As_CUIComponent.FunnyProps.Self = this;
      As_CUIComponent.LayoutProps.Self = this;
    }

    void IComponent.InitParts()
    {
      As_CUIComponent.Adapters.Layout.Init();
      As_CUIComponent.Adapters.LayoutMarker.Init();
      As_CUIComponent.Events.Init();
      As_CUIComponent.Tree.Init();
      As_CUIComponent.FunnyProps.Init();
    }

    void IComponent.InitModules()
    {
      As_CUIComponent.Events.Init();
      As_CUIComponent.Tree.Init();
      As_CUIComponent.Adapters.Layout.Init();
      As_CUIComponent.Adapters.LayoutMarker.Init();
    }

    void IComponent.InjectProps()
    {
      As_CUIComponent.LayoutProps.Absolute.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.Relative.Container = As_CUIComponent.LayoutProps;
    }

    protected class Self_As_CUIComponent : IAdapterPart
    {
      public CUIComponent.Adapters_Part Adapters => Self.Adapters;
      public CUIComponent.FunnyProps_Part FunnyProps => Self.FunnyProps;
      public CUIComponent.LayoutProps_Part LayoutProps => Self.LayoutProps;
      public LayoutSlot LayoutSlot => Self.LayoutSlot;
      public LayoutMarker LayoutMarker => Self.LayoutMarker;
      public CUIComponent.Events_Part Events => Self.Events;
      public CUIComponent.MainComponentTracker_Part MainComponentTracker => Self.MainComponentTracker;
      public CUIComponent.Tree_Part Tree => Self.Tree;
      public CUIComponent.Visual_Part Visual => Self.Visual;
      public DragHandle DragHandle => Self.DragHandle;
      public Layout Layout => Self.Layout;
      public CUIComponent Self { get; set; }
    }
  }
}
