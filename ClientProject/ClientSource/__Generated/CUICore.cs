using System;
using Barotrauma;
using System.Linq;
using System.Collections.Generic;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUICore
  {
    [Local]
    protected Self_As_CUICore As_CUICore { get; } = new();
    void IComponent.RunInitMethods()
    {
    }

    void IComponent.InjectModules()
    {
    }

    void IComponent.InjectParts()
    {
      As_CUICore.Self = this;
      
      As_CUICore.GlobalFocusTracker.Self = this;
      As_CUICore.CUIRunnerHandle.Self = this;
      As_CUICore.LifeCycle.Self = this;
      As_CUICore._Reflection.Self = this;
    }

    void IComponent.InitParts()
    {
    }

    void IComponent.InitModules()
    {
    }

    void IComponent.InjectProps()
    {
    }

    void IComponent.NotifyAwareObjects()
    {
    }

  protected class Self_As_CUICore : IAdapterPart
  {
    public CUICore.GlobalFocusTracker_Part GlobalFocusTracker => Self.GlobalFocusTracker;
    public CUICore.CUIRunnerHandle_Part CUIRunnerHandle => Self.CUIRunnerHandle;
    public CUICore.LifeCycle_Part LifeCycle => Self.LifeCycle;
    public CUICore.Reflection_Part _Reflection => Self._Reflection;
    public EventConstructor EventConstructor => Self.EventConstructor;
    public CUICore Self { get; set; }
  }
  }
}
