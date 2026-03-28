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
      LayoutSlot.Host = Access.Layout;
      LayoutMarker.Host = Access.LayoutMarker;
      Tree.MainComponentTracker = MainComponentTracker;
    }

    void IComponent.InjectParts()
    {
      Access.Self = this;
      Access.IDragHandleHub.Self = this;
      Access.IDraggable.Self = this;
      Access.LayoutMarker.Self = this;
      Access.Layout.Self = this;
      Events.Self = this;
      MainComponentTracker.Self = this;
      Tree.Self = this;
      Visual.Self = this;
      FunnyProps.Self = this;
      LayoutProps.Self = this;
    }

    void IComponent.InitParts()
    {
      Access.LayoutMarker.Init();
      Access.Layout.Init();
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
