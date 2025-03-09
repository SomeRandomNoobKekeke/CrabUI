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
using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{

  public class TypeTreeNode
  {
    public Type T;
    public TypeTreeNode Parent;
    public List<TypeTreeNode> Children = new();
    public CUITypeMetaData Meta => CUITypeMetaData.Get(T);
    public void Add(TypeTreeNode child)
    {
      child.Parent = this;
      Children.Add(child);
    }
    public TypeTreeNode(Type t) => T = t;
    public override string ToString() => T?.ToString() ?? "null";
    public void RunRecursive(Action<TypeTreeNode> action)
    {
      action(this);
      foreach (TypeTreeNode child in Children)
      {
        child.RunRecursive(action);
      }
    }

  }
  [CUIInternal]
  public class CUIReflection
  {
    internal static void InitStatic()
    {
      CUI.OnInit += () =>
      {
        Stopwatch sw = Stopwatch.StartNew();
        FindCUITypes();
        FormCUITypeTree();
        CUIDebug.Log($"CUIReflection.Initialize took {sw.ElapsedMilliseconds}ms");
      };
      CUI.OnDispose += () =>
      {
        CUITypes.Clear();
        CUILayoutTypes.Clear();
        CUITypeTree.Clear();
      };
    }



    public record TypePair(Type type, Type baseType);

    public static Dictionary<Type, TypeTreeNode> CUITypeTree = new();

    public static Dictionary<string, Type> CUILayoutTypes = new();
    public static Dictionary<string, Type> CUITypes = new Dictionary<string, Type>();

    public static void FormCUITypeTree()
    {
      List<TypePair> Pustoe = CUITypes.Values.Select(t => new TypePair(t, t.BaseType)).ToList();
      List<TypePair> Porojnee = new List<TypePair>();

      while (Pustoe.Count > 0)
      {
        Porojnee = new List<TypePair>();
        foreach (TypePair pair in Pustoe)
        {
          // Tree root CUIComponent
          if (pair.baseType == typeof(object))
          {
            CUITypeTree[pair.type] = new TypeTreeNode(pair.type);
            continue;
          }

          // Derived class
          if (CUITypeTree.ContainsKey(pair.baseType))
          {
            CUITypeTree[pair.type] = new TypeTreeNode(pair.type);
            CUITypeTree[pair.baseType].Add(CUITypeTree[pair.type]);
            continue;
          }

          // Base class not in tree yet
          Porojnee.Add(pair);
        }

        Pustoe.Clear();
        Pustoe = Porojnee;
      }

      //foreach (TypeTreeNode node in CUITypeTree.Values) CUI.Log(node);
    }

    public static void FindCUITypes()
    {
      Assembly CUIAssembly = Assembly.GetAssembly(typeof(CUI));
      Assembly CallingAssembly = Assembly.GetCallingAssembly();

      CUITypes["CUIComponent"] = typeof(CUIComponent);
      CUILayoutTypes["CUILayout"] = typeof(CUILayout);


      //TODO Nested types might have same name, think how to separate them
      // lua and style resolver uses them
      // string name = t.Name;
      // if (t.DeclaringType != null) name = $"{t.DeclaringType.Name}.{t.Name}";
      // CUITypes[name] = t;
      foreach (Type t in CallingAssembly.GetTypes())
      {
        if (t.IsSubclassOf(typeof(CUIComponent))) CUITypes[t.Name] = t;
        if (t.IsSubclassOf(typeof(CUILayout))) CUILayoutTypes[t.Name] = t;
      }

      foreach (Type t in CUIAssembly.GetTypes())
      {
        if (t.IsSubclassOf(typeof(CUIComponent))) CUITypes[t.Name] = t;
        if (t.IsSubclassOf(typeof(CUILayout))) CUILayoutTypes[t.Name] = t;
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