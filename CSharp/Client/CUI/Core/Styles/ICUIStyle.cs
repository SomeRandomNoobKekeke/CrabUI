using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  public interface ICUIStyle
  {
    public void Apply(CUIComponent component);
  }

  public interface ICUIStyle<ComponentT> : ICUIStyle where ComponentT : CUIComponent
  {
    void ICUIStyle.Apply(CUIComponent component) => Apply((ComponentT)component);
    public void Apply(ComponentT component);
  }
}