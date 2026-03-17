using System;
using Barotrauma;
using System.Linq;
using System.Collections.Generic;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUICore
  {
    void IComponent.InjectModules()
    {
    }

    void IComponent.InjectParts()
    {
      (this as CrabUI.CUICore).UpdateHandle.Self = this;
      (this as CrabUI.CUICore).DrawBeforeGUIHandle.Self = this;
      (this as CrabUI.CUICore).DrawAfterGUIHandle.Self = this;
      (this as CrabUI.CUICore).LifeCycle.Self = this;
    }
  }
}
