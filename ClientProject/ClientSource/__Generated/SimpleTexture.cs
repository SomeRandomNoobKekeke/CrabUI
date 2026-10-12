using System;
using Barotrauma;
using System.Linq;
using System.Collections.Generic;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class SimpleTexture
  {
    [Local]
    protected Self_As_SimpleTexture As_SimpleTexture { get; } = new();
    void IComponent.RunInitMethods()
    {
      InitAnimatedProps();
    }

    void IComponent.InjectModules()
    {
    }

    void IComponent.InjectParts()
    {
      As_SimpleTexture.Self = this;
      
      As_SimpleTexture.Animations.Self = this;
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
      Animations.Self.HostComponent = this;
      Animations.Self.HostPropName = "Self";
    }

  protected class Self_As_SimpleTexture : IAdapterPart
  {
    public SimpleTexture.Animations_Part Animations => Self.Animations;
    public SimpleTexture Self { get; set; }
  }
  }
}
