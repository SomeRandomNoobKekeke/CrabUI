using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  public interface ICUIStyle
  {
    public string ID { get; }
    public Type TargetType { get; }
    public int Priority { get; }

    public void Apply(CUIComponent component);
  }

}