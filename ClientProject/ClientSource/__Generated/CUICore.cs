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
      
      As_CUICore.FocusHandle.Self = this;
      As_CUICore.CUIRunnerHandle.Self = this;
      As_CUICore.LifeCycle.Self = this;
      As_CUICore.MainComponents.Self = this;
      As_CUICore._Reflection.Self = this;
      As_CUICore.VanillaGUILayer.Self = this;
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
    public CUICore.FocusHandle_Part FocusHandle => Self.FocusHandle;
    public CUICore.CUIRunnerHandle_Part CUIRunnerHandle => Self.CUIRunnerHandle;
    public CUICore.LifeCycle_Part LifeCycle => Self.LifeCycle;
    public CUICore.MainComponents_Part MainComponents => Self.MainComponents;
    public CUICore.Reflection_Part _Reflection => Self._Reflection;
    public CUICore.VanillaGUILayerImage_Part VanillaGUILayer => Self.VanillaGUILayer;
    public CUICore Self { get; set; }
  }
  }
}
