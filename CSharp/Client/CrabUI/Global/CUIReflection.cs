using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;

namespace CrabUI
{
  [CUIInternal]
  public class CUIReflection
  {
    internal static void InitStatic()
    {
      CUI.OnInit += () => FindCUITypes();
      CUI.OnDispose += () => CUITypes.Clear();
    }

    public static Dictionary<string, Type> CUITypes;

    public static void FindCUITypes()
    {
      CUITypes = new Dictionary<string, Type>();

      Assembly CUIAssembly = Assembly.GetAssembly(typeof(CUI));
      Assembly CallingAssembly = Assembly.GetCallingAssembly();

      CUITypes["CUIComponent"] = typeof(CUIComponent);

      foreach (Type t in CallingAssembly.GetTypes())
      {
        if (t.IsSubclassOf(typeof(CUIComponent)))
        {
          CUITypes[t.Name] = t;
        }
      }

      foreach (Type t in CUIAssembly.GetTypes())
      {
        if (t.IsSubclassOf(typeof(CUIComponent)))
        {
          CUITypes[t.Name] = t;
        }
      }
    }

    public static Type GetComponentTypeByName(string name)
    {
      return CUITypes.GetValueOrDefault(name);
    }

    public static object GetDefault(object obj)
    {
      FieldInfo defField = obj.GetType().GetField("Default", BindingFlags.Static | BindingFlags.Public);
      if (defField == null) return null;
      return defField.GetValue(null);
    }

    public static object GetNestedValue(object obj, string nestedName)
    {
      string[] names = nestedName.Split('.');

      foreach (string name in names)
      {
        FieldInfo fi = obj.GetType().GetField(name, AccessTools.all);
        PropertyInfo pi = obj.GetType().GetProperty(name, AccessTools.all);

        if (fi != null)
        {
          obj = fi.GetValue(obj);
          continue;
        }

        if (pi != null)
        {
          obj = pi.GetValue(obj);
          continue;
        }

        return null;
      }

      return obj;
    }
  }
}