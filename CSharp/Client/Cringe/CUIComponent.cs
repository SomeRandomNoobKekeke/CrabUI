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
      Access.LayoutMarker.Self = this;
      Access.Layout.Self = this;
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
      FunnyProps.Init();
    }

    void IComponent.InjectProps()
    {
      LayoutProps.Absolute.Container = LayoutProps;
      LayoutProps.Relative.Container = LayoutProps;
    }
  }
}
