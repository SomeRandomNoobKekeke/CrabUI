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
      MainComponentTracker.Self = this;
      Tree.Self = this;
    }
  }
}
