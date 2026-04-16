using System;
using Barotrauma;
using System.Linq;
using System.Collections.Generic;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUIMainComponent
  {
    void IComponent.InjectModules()
    {
      LayoutSlot.Host = Access_CUIComponent.Layout;
      LayoutMarker.Host = Access_CUIComponent.LayoutMarker;
      Tree.MainComponentTracker = MainComponentTracker;
    }

    void IComponent.InjectParts()
    {
      LayoutFlattener.Self = this;
      InitDebugChannels.Self = this;
      Visual.Self = this;
      Access_CUIMainComponent.Self = this;
      GlobalEvents.Self = this;
      Access_CUIComponent.Self = this;
      Access_CUIComponent.IDragHandleHub.Self = this;
      Access_CUIComponent.IDraggable.Self = this;
      Access_CUIComponent.LayoutMarker.Self = this;
      Access_CUIComponent.Layout.Self = this;
      Events.Self = this;
      MainComponentTracker.Self = this;
      Tree.Self = this;
      FunnyProps.Self = this;
      LayoutProps.Self = this;
    }

    void IComponent.InitParts()
    {
      InitDebugChannels.Init();
      Access_CUIComponent.LayoutMarker.Init();
      Access_CUIComponent.Layout.Init();
      Events.Init();
      Tree.Init();
      FunnyProps.Init();
    }

    void IComponent.InjectProps()
    {
      LayoutProps.Absolute.Container = LayoutProps;
      LayoutProps.Relative.Container = LayoutProps;
    }
  }
}
