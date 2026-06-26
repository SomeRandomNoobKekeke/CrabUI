using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  public class CUIDefaultStyle<ComponentT> : CUIActionStyle<ComponentT> where ComponentT : CUIComponent
  {
    public CUIDefaultStyle(Action<ComponentT> action) : base($"Default for {typeof(ComponentT).Name}", action)
    {
      Category = CUIStyleCategory.Default;
    }
  }
}