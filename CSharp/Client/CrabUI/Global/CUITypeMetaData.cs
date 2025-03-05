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
  public class NoDefaultAttribute : System.Attribute { }

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
      CUI.OnInit += () =>
      {
        TypeMetaData = new Dictionary<Type, CUITypeMetaData>();
      };
      CUI.OnDispose += () =>
      {
        TypeMetaData.Clear();
      };
    }

    public static Dictionary<Type, CUITypeMetaData> TypeMetaData;

    public static CUITypeMetaData Get(Type type)
    {
      if (!TypeMetaData.ContainsKey(type)) new CUITypeMetaData(type);
      return TypeMetaData[type];
    }

    public Type CUIType;

    public CUIComponent Default;

    public CUIStyle defaultStyle; public CUIStyle DefaultStyle
    {
      get => defaultStyle;
      set
      {
        if (defaultStyle == value) return;

        if (defaultStyle != null)
        {
          defaultStyle.OnUse -= HandleStyleChange;
          defaultStyle.OnPropChanged -= HandleStylePropChange;
        }

        defaultStyle = value;

        if (defaultStyle != null)
        {
          defaultStyle.OnUse += HandleStyleChange;
          defaultStyle.OnPropChanged += HandleStylePropChange;
        }

        HandleStyleChange(defaultStyle);
      }
    }

    private void HandleStylePropChange(string key, string value)
    {
      CUIGlobalStyleResolver.OnDefaultStylePropChanged(CUIType, key);
    }
    private void HandleStyleChange(CUIStyle s)
    {
      CUIGlobalStyleResolver.OnDefaultStyleChanged(CUIType);
    }

    public CUIStyle ResolvedDefaultStyle { get; set; }


    public SortedDictionary<string, PropertyInfo> Serializable = new();
    public SortedDictionary<string, PropertyInfo> Calculated = new();
    public SortedDictionary<string, PropertyInfo> Assignable = new();

    public CUITypeMetaData(Type type)
    {
      TypeMetaData[type] = this; // !!!
      CUIType = type;

      foreach (PropertyInfo pi in type.GetProperties(AccessTools.all))
      {
        if (Attribute.IsDefined(pi, typeof(CUISerializableAttribute)))
        {
          Serializable[pi.Name] = pi;
        }

        if (Attribute.IsDefined(pi, typeof(CalculatedAttribute)))
        {
          Calculated[pi.Name] = pi;
        }

        if (pi.CanWrite)
        {
          Assignable[pi.Name] = pi;
        }
      }
      try
      {
        DefaultStyle = new CUIStyle();
        if (!Attribute.IsDefined(type, typeof(NoDefaultAttribute)))
        {
          Default = (CUIComponent)Activator.CreateInstance(type);
        }
      }
      catch (Exception e)
      {
        if (e is System.MissingMethodException) return;

        CUI.Warning($"In CUITypeMetaData {type}\n{e}\n");
      }
    }
  }

}