using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CursedUI
{
  //THINK mb make styles aplicable to any object?
  public interface ICUIStyle
  {
    public string ID { get; }
    public Type TargetType { get; }

    public void Apply(CUIVisualComponent component);
  }

}