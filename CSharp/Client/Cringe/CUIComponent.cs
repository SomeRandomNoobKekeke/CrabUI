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
      Tree.MainComponentTracker = MainComponentTracker;
    }

    void IComponent.InjectParts()
    {
      (this as CrabUI.CUIComponent).Visual.Self = this;
      (this as CrabUI.CUIComponent).MainComponentTracker.Self = this;
      (this as CrabUI.CUIComponent).Tree.Self = this;
    }
  }
}
