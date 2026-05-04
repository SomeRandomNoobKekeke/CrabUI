using System;
using Barotrauma;
using System.Linq;
using System.Collections.Generic;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUICore
  {
    [Local]
    protected Self_As_CUICore As_CUICore { get; } = new();
    void IComponent.InjectModules()
    {
    }

    void IComponent.InjectParts()
    {
      As_CUICore.Self = this;
      
      As_CUICore.Debugger.Self = this;
      As_CUICore.InitDebugChannels.Self = this;
      As_CUICore.CUIRunnerHandle.Self = this;
      As_CUICore.LifeCycle.Self = this;
    }

    void IComponent.InitParts()
    {
      As_CUICore.Debugger.Init();
      As_CUICore.InitDebugChannels.Init();
    }

    void IComponent.InitModules()
    {
    }

    void IComponent.InjectProps()
    {
    }

  protected class Self_As_CUICore : IAdapterPart
  {
    public CUICore.Debugger_Part Debugger => Self.Debugger;
    public CUICore.InitDebugChannels_Part InitDebugChannels => Self.InitDebugChannels;
    public CUICore.CUIRunnerHandle_Part CUIRunnerHandle => Self.CUIRunnerHandle;
    public CUICore.LifeCycle_Part LifeCycle => Self.LifeCycle;
    public CUICore Self { get; set; }
  }
  }
}
