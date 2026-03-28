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
      LayoutSlot.Host = (this as CUIComponent).Access.Layout;
      LayoutMarker.Host = (this as CUIComponent).Access.LayoutMarker;
      Tree.MainComponentTracker = MainComponentTracker;
    }

    void IComponent.InjectParts()
    {
      LayoutFlattener.Self = this;
      InitDebugChannels.Self = this;
      Visual.Self = this;
      Access.Self = this;
      GlobalEvents.Self = this;
      (this as CrabUI.CUIComponent).Access.Self = this;
      (this as CUIComponent).Access.IDragHandleHub.Self = this;
      (this as CUIComponent).Access.IDraggable.Self = this;
      (this as CUIComponent).Access.LayoutMarker.Self = this;
      (this as CUIComponent).Access.Layout.Self = this;
      (this as CrabUI.CUIComponent).Events.Self = this;
      (this as CrabUI.CUIComponent).MainComponentTracker.Self = this;
      (this as CrabUI.CUIComponent).Tree.Self = this;
      (this as CrabUI.CUIComponent).FunnyProps.Self = this;
      (this as CrabUI.CUIComponent).LayoutProps.Self = this;
    }

    void IComponent.InitParts()
    {
      InitDebugChannels.Init();
      (this as CUIComponent).Access.LayoutMarker.Init();
      (this as CUIComponent).Access.Layout.Init();
      (this as CrabUI.CUIComponent).Events.Init();
      (this as CrabUI.CUIComponent).Tree.Init();
      (this as CrabUI.CUIComponent).FunnyProps.Init();
    }

    void IComponent.InjectProps()
    {
      LayoutProps.Absolute.Container = LayoutProps;
      LayoutProps.Relative.Container = LayoutProps;
    }
  }
}
