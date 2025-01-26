using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;
using HarmonyLib;

namespace CrabUI
{
  public class CUISerializableAttribute : System.Attribute { }
  public class DontSerializeAttribute : System.Attribute { }
  public class CalculatedAttribute : System.Attribute { }

  public enum CUIAttribute
  {
    CUISerializable,
    DontSerialize,
    Calculated,
  }

  public class CUITypeMetaData
  {
    internal static void InitStatic()
    {
      CUI.OnInit += () => TypeMetaData = new Dictionary<Type, CUITypeMetaData>();
      CUI.OnDispose += () => TypeMetaData.Clear();
    }

    public static Dictionary<Type, CUITypeMetaData> TypeMetaData;

    public static CUITypeMetaData Get(Type type)
    {
      if (!TypeMetaData.ContainsKey(type)) TypeMetaData[type] = new CUITypeMetaData(type);
      return TypeMetaData[type];
    }

    public object Default;

    public SortedDictionary<string, PropertyInfo> Serializable = new();
    public SortedDictionary<string, PropertyInfo> Calculated = new();

    public CUITypeMetaData(Type type)
    {
      Default = Activator.CreateInstance(type);

      foreach (PropertyInfo pi in type.GetProperties(AccessTools.all))
      {
        if (Attribute.IsDefined(pi, typeof(CUISerializableAttribute)))
        {
          Serializable[pi.Name] = pi;
        }
      }

      foreach (PropertyInfo pi in type.GetProperties(AccessTools.all))
      {
        if (Attribute.IsDefined(pi, typeof(CalculatedAttribute)))
        {
          Calculated[pi.Name] = pi;
        }
      }

    }
  }

}