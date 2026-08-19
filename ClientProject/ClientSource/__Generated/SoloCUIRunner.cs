using System;
using Barotrauma;
using System.Linq;
using System.Collections.Generic;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class SoloCUIRunner
  {
    [Local]
    protected Self_As_SoloCUIRunner As_SoloCUIRunner { get; } = new();
    void IComponent.RunInitMethods()
    {
    }

    void IComponent.InjectModules()
    {
    }

    void IComponent.InjectParts()
    {
      As_SoloCUIRunner.Self = this;
      
      As_SoloCUIRunner.CUICoreHandles.Self = this;
      As_SoloCUIRunner.ResourceIOContext.Self = this;
      As_SoloCUIRunner.ResourceIOContextHandle.Self = this;
      As_SoloCUIRunner.CUITextureManagerPublic.Self = this;
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

  protected class Self_As_SoloCUIRunner : IAdapterPart
  {
    public SoloCUIRunner.CUICoreHandles_Part CUICoreHandles => Self.CUICoreHandles;
    public SoloCUIRunner.ResourceIOContext_Part ResourceIOContext => Self.ResourceIOContext;
    public SoloCUIRunner.ResourceIOContextHandle_Part ResourceIOContextHandle => Self.ResourceIOContextHandle;
    public SoloCUIRunner.CUITextureManager_PublicPart CUITextureManagerPublic => Self.CUITextureManagerPublic;
    public SoloCUIRunner Self { get; set; }
  }
  }
}
