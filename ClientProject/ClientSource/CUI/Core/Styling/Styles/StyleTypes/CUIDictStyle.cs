using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  public class CUIDictStyle : Dictionary<string, object>, ICUIStyle
  {
    public static CUIDictStyle FromComponent(string id, CUIComponent component)
    {
      CUIDictStyle style = new CUIDictStyle(id, component.GetType());
      foreach (string key in component.As_Dictionary.Keys)
      {
        style[key] = component.As_Dictionary[key];
      }
      return style;
    }

    public string ID { get; }
    public Type TargetType { get; }
    public int Priority { get; set; } = ICUIStyle.DefaultPriority;
    public CUIStyleCategory Category { get; set; }

    public void Apply(CUIComponent component)
    {
      foreach (var (key, value) in this)
      {
        component.As_Dictionary[key] = value;
      }
    }


    public CUIDictStyle(string id, Type targetType)
    {
      ID = id;
      TargetType = targetType;
    }

    public override string ToString()
    {
      return BaroJunk.Logger.Wrap.IDictionary(this);
    }
  }


}