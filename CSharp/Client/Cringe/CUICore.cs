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
      Debugger.Self = this;
      DebugChannel.Self = this;
      UpdateHandle.Self = this;
      DrawBeforeGUIHandle.Self = this;
      DrawAfterGUIHandle.Self = this;
      LifeCycle.Self = this;
    }

    void IComponent.InitParts()
    {
      Debugger.Init();
      DebugChannel.Init();
    }

    void IComponent.InjectProps()
    {
    }
  }
}
