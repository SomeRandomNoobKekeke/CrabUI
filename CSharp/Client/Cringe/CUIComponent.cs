using System;
using Barotrauma;
using System.Linq;
using System.Collections.Generic;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUIComponent
  {
    void IComponent.InjectModules()
    {
      LayoutSlot.Host = Access_CUIComponent.Layout;
      LayoutMarker.Host = Access_CUIComponent.LayoutMarker;
      Tree.MainComponentTracker = MainComponentTracker;
      Visual.Tree = Tree;
    }

    void IComponent.InjectParts()
    {
      Access_CUIComponent.Self = this;
      Access_CUIComponent.IDragHandleHub.Self = this;
      Access_CUIComponent.IDraggable.Self = this;
      Access_CUIComponent.LayoutMarker.Self = this;
      Access_CUIComponent.Layout.Self = this;
      Events.Self = this;
      MainComponentTracker.Self = this;
      Tree.Self = this;
      Visual.Self = this;
      FunnyProps.Self = this;
      LayoutProps.Self = this;
    }

    void IComponent.InitParts()
    {
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
