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
    public int Priority { get; set; } = ICUIStyle.DefaultPriority;
    public CUIStyleCategory Category { get; set; }

    public abstract void Apply(CUIComponent component);

    public CUIActionStyle(string id, Type targetType)
    {
      ID = id;
      TargetType = targetType;
    }

    public override string ToString() => $"CUIStyle [{ID}]";
  }

  public class CUIActionStyle<ComponentT> : CUIActionStyle where ComponentT : CUIComponent
  {
    public Action<ComponentT> Action { get; set; }

    public override void Apply(CUIComponent component)
    {
      Action?.Invoke((ComponentT)component);
      // CUI.Logger.Log($"Applying [{this}] to [{component}]"); // TODO this should be a debug event
    }


    public CUIActionStyle(string id, Action<ComponentT> action) : base(id, typeof(ComponentT))
    {
      Action = action;
    }
  }


}