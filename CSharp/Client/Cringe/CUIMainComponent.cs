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
    }

    void IComponent.InjectParts()
    {
      Tree.Self = this;
      Protected.Self = this;
      Protected.MainComponentTracker.Self = this;
    }
  }
}
