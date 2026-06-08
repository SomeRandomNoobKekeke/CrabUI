using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  public abstract class CUIActionStyle : ICUIStyle
  {
    public string ID { get; set; }
    public Type TargetType { get; set; }
    public int Priority { get; set; }

    public abstract void Apply(CUIComponent component);
  }

  public class CUIActionStyle<ComponentT> : CUIActionStyle where ComponentT : CUIComponent
  {
    public Action<ComponentT> Action { get; set; }

    public override void Apply(CUIComponent component) => ApplyComponentSpecific((ComponentT)component);
    private void ApplyComponentSpecific(ComponentT component) => Action?.Invoke(component);
  }


}