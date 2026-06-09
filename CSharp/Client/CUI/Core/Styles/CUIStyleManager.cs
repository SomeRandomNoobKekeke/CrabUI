using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  //TODO
  public class CUIStyleManager(CUIComponentTypeManager typeManager)
  {
    public CUIComponentTypeManager TypeManager { get; } = typeManager;


    public Dictionary<Type, List<ICUIStyle>> Styles { get; } = new();

    public List<ICUIStyle> GetAllStylesFor(Type targetType)
      => Styles.ContainsKey(targetType) ? Styles[targetType] : new List<ICUIStyle>();

    public bool HasStylesFor(Type T) => Styles.ContainsKey(T);
    public void AddStyle(ICUIStyle style)
    {
      foreach (Type T in TypeManager.GetDerivedTypes(style.TargetType))
      {
        if (!Styles.ContainsKey(T)) Styles[T] = new List<ICUIStyle>();
        Styles[T].Add(style);
      }
    }

    public void RemoveStyle(ICUIStyle style)
    {
      foreach (var list in Styles.Values) { list.Remove(style); }
    }

    public void RemoveStyle(string id)
    {
      foreach (var list in Styles.Values) { list.RemoveAll(style => style.ID == id); }
    }

  }


}