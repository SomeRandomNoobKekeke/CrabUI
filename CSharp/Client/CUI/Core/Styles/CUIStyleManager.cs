using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{

  //TODO
  public class CUIStyleManager
  {



    public Dictionary<Type, List<ICUIStyle>> Styles { get; } = new();

    public List<ICUIStyle> GetAllStylesFor(Type targetType)
      => Styles.ContainsKey(targetType) ? Styles[targetType] : new List<ICUIStyle>();

    public void AddStyle(ICUIStyle style)
    {
      //TODO atyatya, no, the styles should be applied to derived classes, not base, i need type tree first
      // foreach (Type T in CUIComponentAnalyzer.GetCUIComponentTypeChain(style.TargetType))
      // {
      //   if (!Styles.ContainsKey(T)) Styles[T] = new List<ICUIStyle>();
      //   Styles[T].Add(style);
      // }
    }

    public void RemoveStyle(ICUIStyle style) { }
    public void RemoveStyle(string id) { }

  }


}