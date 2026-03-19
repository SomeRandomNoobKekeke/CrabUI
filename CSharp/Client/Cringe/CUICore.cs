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
      UpdateHandle.Self = this;
      DrawBeforeGUIHandle.Self = this;
      DrawAfterGUIHandle.Self = this;
      LifeCycle.Self = this;
    }

    void IComponent.InitParts()
    {
    }

    void IComponent.InjectProps()
    {
    }
  }
}
