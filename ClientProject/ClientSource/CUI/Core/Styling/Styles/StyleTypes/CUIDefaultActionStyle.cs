using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CursedUI
{
  public class CUIDefaultStyle<ComponentT> : CUIActionStyle<ComponentT> where ComponentT : CUIVisualComponent
  {
    public CUIDefaultStyle(Action<ComponentT> action) : base($"Default for {typeof(ComponentT).Name}", action)
    {
      Category = CUIStyleCategory.Default;
    }
  }
}