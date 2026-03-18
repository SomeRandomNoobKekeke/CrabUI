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
      LayoutMarker.Host = Access.LayoutMarker.CrabUI.LayoutMarker.Target.Parent;
      Tree.MainComponentTracker = MainComponentTracker;
    }

    void IComponent.InjectParts()
    {
      (this as CrabUI.CUIComponent).Access.Self = this;
      (this as CrabUI.CUIComponent+Access_Part).Access.LayoutMarker.Self = this;
      (this as CrabUI.CUIComponent+Access_Part).Access.Layout.Self = this;
      (this as CrabUI.CUIComponent).LayoutSlot.Self = this;
      (this as CrabUI.CUIComponent).MainComponentTracker.Self = this;
      (this as CrabUI.CUIComponent).Tree.Self = this;
      (this as CrabUI.CUIComponent).Visual.Self = this;
      (this as CrabUI.CUIComponent).FunnyProps.Self = this;
      (this as CrabUI.CUIComponent).LayoutProps.Self = this;
    }

    void IComponent.InitParts()
    {
      (this as CrabUI.CUIComponent+Access_Part).Access.LayoutMarker.Init();
      (this as CrabUI.CUIComponent+Access_Part).Access.Layout.Init();
      (this as CrabUI.CUIComponent).FunnyProps.Init();
    }

    void IComponent.InjectProps()
    {
      (this as CrabUI.CUIComponent).LayoutProps.Absolute.Container = (this as CrabUI.CUIComponent).LayoutProps;
      (this as CrabUI.CUIComponent).LayoutProps.Relative.Container = (this as CrabUI.CUIComponent).LayoutProps;
    }
  }
}
