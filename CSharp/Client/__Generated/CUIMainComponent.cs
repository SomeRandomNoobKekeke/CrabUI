using System;
using Barotrauma;
using System.Linq;
using System.Collections.Generic;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIMainComponent
  {
    [Local]
    protected Self_As_CUIMainComponent As_CUIMainComponent { get; } = new();
    void IComponent.InjectModules()
    {
      As_CUIComponent.Visual.Tree = As_CUIMainComponent.Tree;
      As_CUIComponent.Tree.MainComponentTracker = As_CUIMainComponent.MainComponentTracker;
      As_CUIMainComponent.LayoutSlot.Host = As_CUIComponent.Adapters.Layout;
      As_CUIMainComponent.LayoutMarker.Host = As_CUIComponent.Adapters.LayoutMarker;
      As_CUIMainComponent.DragHandle.Hub = As_CUIMainComponent.Adapters.IDragHandleHub;
      As_CUIMainComponent.DragHandle.Host = As_CUIComponent.Adapters.IDraggable;
    }

    void IComponent.InjectParts()
    {
      As_CUIComponent.Self = this;
      As_CUIMainComponent.Self = this;
      
      As_CUIMainComponent.InitDebugChannels.Self = this;
      As_CUIComponent.Visual.Self = this;
      As_CUIMainComponent.Adapters.Self = this;
      As_CUIMainComponent.Adapters.IDragHandleHub.Self = this;
      As_CUIMainComponent.GlobalEvents.Self = this;
      As_CUIComponent.Adapters.Self = this;
      As_CUIComponent.Adapters.IDraggable.Self = this;
      As_CUIComponent.Adapters.IDragHandleHub.Self = this;
      As_CUIComponent.Adapters.Layout.Self = this;
      As_CUIComponent.Adapters.LayoutMarker.Self = this;
      As_CUIComponent.Events.Self = this;
      As_CUIComponent.MainComponentTracker.Self = this;
      As_CUIComponent.Tree.Self = this;
      As_CUIComponent.FunnyProps.Self = this;
      As_CUIComponent.LayoutProps.Self = this;
    }

    void IComponent.InitParts()
    {
      As_CUIMainComponent.InitDebugChannels.Init();
      As_CUIComponent.Adapters.Layout.Init();
      As_CUIComponent.Adapters.LayoutMarker.Init();
      As_CUIComponent.Events.Init();
      As_CUIComponent.Tree.Init();
      As_CUIComponent.FunnyProps.Init();
    }

    void IComponent.InitModules()
    {
      As_CUIMainComponent.Events.Init();
      As_CUIMainComponent.Tree.Init();
      As_CUIComponent.Adapters.Layout.Init();
      As_CUIComponent.Adapters.LayoutMarker.Init();
    }

    void IComponent.InjectProps()
    {
      As_CUIComponent.LayoutProps.Absolute.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.Relative.Container = As_CUIComponent.LayoutProps;
    }

  protected class Self_As_CUIMainComponent : IAdapterPart
  {
    public CUIMainComponent.InitDebugChannels_Part InitDebugChannels => Self.InitDebugChannels;
    public CUIMainComponent.Adapters_Part Adapters => Self.Adapters;
    public CUIMainComponent.GlobalEvents_Part GlobalEvents => Self.GlobalEvents;
    public VisualFlattener VisualFlattener => Self.VisualFlattener;
    public LayoutFlattener LayoutFlattener => Self.LayoutFlattener;
    public ChainDrawer ChainDrawer => Self.ChainDrawer;
    public EventDispatcher EventDispatcher => Self.EventDispatcher;
    public EventConstructor EventConstructor => Self.EventConstructor;
    public EventTargets EventTargets => Self.EventTargets;
    public CUIComponent.Visual_Part Visual => Self.Visual;
    public LayoutSlot LayoutSlot => Self.LayoutSlot;
    public LayoutMarker LayoutMarker => Self.LayoutMarker;
    public CUIComponent.Events_Part Events => Self.Events;
    public CUIComponent.MainComponentTracker_Part MainComponentTracker => Self.MainComponentTracker;
    public CUIComponent.Tree_Part Tree => Self.Tree;
    public DragHandle DragHandle => Self.DragHandle;
    public Layout Layout => Self.Layout;
    public CUIMainComponent Self { get; set; }
  }
  }
}
