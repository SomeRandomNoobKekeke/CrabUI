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

    public abstract void Apply(CUIVisualComponent component);

    public CUIActionStyle(string id, Type targetType)
    {
      ID = id;
      TargetType = targetType;
    }

    public override string ToString() => $"CUIActionStyle [{ID}]";
  }

  public class CUIActionStyle<ComponentT> : CUIActionStyle where ComponentT : CUIVisualComponent
  {
    public CUIDebugNode<CUIVisualComponent, ICUIStyle> Debug_StyleApplied { get; } = new(DebugCategory.StyleApplied)
    {
      MsgFactory = (component, style) => $"{style} applied to {component}",
      IsOpen = true,
    };

    public Action<ComponentT> Action { get; set; }

    public override void Apply(CUIVisualComponent component)
    {
      Action?.Invoke((ComponentT)component);
      if (component.Debug) Debug_StyleApplied.Send(component, this);
    }


    public CUIActionStyle(string id, Action<ComponentT> action) : base(id, typeof(ComponentT))
    {
      Action = action;
    }
  }


}