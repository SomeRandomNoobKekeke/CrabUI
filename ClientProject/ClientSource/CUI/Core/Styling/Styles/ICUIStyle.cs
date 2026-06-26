using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  public interface ICUIStyle
  {
    public static int DefaultPriority = 100;

    public string ID { get; }
    public CUIStyleCategory Category { get; }
    public int Priority { get; }

    public Type TargetType { get; }

    public void Apply(CUIComponent component);
  }

}