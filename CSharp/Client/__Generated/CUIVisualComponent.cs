using System;
using Barotrauma;
using System.Linq;
using System.Collections.Generic;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIVisualComponent
  {
    [Local]
    protected Self_As_CUIVisualComponent As_CUIVisualComponent { get; } = new();
    void IComponent.InjectModules()
    {
    }

    void IComponent.InjectParts()
    {
      As_CUIVisualComponent.Self = this;
      
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

  protected class Self_As_CUIVisualComponent : IAdapterPart
  {
    public CUIVisualComponent Self { get; set; }
  }
  }
}
