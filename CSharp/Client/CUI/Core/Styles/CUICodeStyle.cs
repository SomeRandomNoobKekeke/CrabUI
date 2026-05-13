using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  public class CUICodeStyle : ICUIStyle
  {
    public Action<CUIComponent> ApplyAction { get; set; }

    public void Apply(CUIComponent component) => ApplyAction?.Invoke(component);
  }

  public class CUICodeStyle<ComponentT> : ICUIStyle<ComponentT> where ComponentT : CUIComponent
  {
    public Action<ComponentT> ApplyAction { get; set; }

    public void Apply(ComponentT component) => ApplyAction?.Invoke(component);
  }


}