using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Contains all logic for resolving styles in the framework
  /// </summary>
  public static class CUIGlobalStyleResolver
  {
    public static void OnComponentStyleChanged(CUIComponent host)
    {
      CUITypeMetaData meta = CUITypeMetaData.Get(host.GetType());
      host.ResolvedStyle = CUIStyle.Merge(meta.ResolvedDefaultStyle, host.Style);
      ApplyStyleOn(host.ResolvedStyle, host);
    }

    public static void OnComponentStylePropChanged(CUIComponent host, string key)
    {
      CUITypeMetaData meta = CUITypeMetaData.Get(host.GetType());

      if (meta.ResolvedDefaultStyle.Props.ContainsKey(key))
      {
        host.ResolvedStyle[key] = meta.ResolvedDefaultStyle[key];
      }

      if (host.Style.Props.ContainsKey(key))
      {
        host.ResolvedStyle[key] = host.Style[key];
      }

      ApplyStylePropOn(host.ResolvedStyle, key, host, meta);
    }

    public static void OnPaletteChange(CUIPalette palette)
    {
      foreach (Type CUIType in CUIReflection.CUITypes.Values)
      {
        foreach (CUIComponent c in CUIComponent.ComponentsByType.GetPage(CUIType))
        {
          ApplyStyleOn(c.ResolvedStyle, c);
        }
      }
    }

    public static void OnDefaultStyleChanged(Type CUIType)
    {
      // Merge default styles
      CUIReflection.CUITypeTree[CUIType].RunRecursive((node) =>
      {
        node.Meta.ResolvedDefaultStyle = CUIStyle.Merge(
          node.Parent?.Meta.ResolvedDefaultStyle,
          node.Meta.DefaultStyle
        );
      });

      // Apply default styles
      CUIReflection.CUITypeTree[CUIType].RunRecursive((node) =>
      {
        foreach (CUIComponent c in CUIComponent.ComponentsByType.GetPage(node.T))
        {
          OnComponentStyleChanged(c);
        }
      });
    }

    public static void OnDefaultStylePropChanged(Type CUIType, string key)
    {
      // Merge default styles
      CUIReflection.CUITypeTree[CUIType].RunRecursive((node) =>
      {
        if (node.Parent != null)
        {
          if (node.Parent.Meta.ResolvedDefaultStyle.Props.ContainsKey(key))
          {
            node.Meta.ResolvedDefaultStyle[key] = node.Parent.Meta.ResolvedDefaultStyle[key];
          }
        }

        if (node.Meta.DefaultStyle.Props.ContainsKey(key))
        {
          node.Meta.ResolvedDefaultStyle[key] = node.Meta.DefaultStyle[key];
        }
      });

      // Apply default styles
      CUIReflection.CUITypeTree[CUIType].RunRecursive((node) =>
      {
        foreach (CUIComponent c in CUIComponent.ComponentsByType.GetPage(node.T))
        {
          OnComponentStylePropChanged(c, key);
        }
      });
    }




    public static void ApplyStyleOn(CUIStyle style, CUIComponent target)
    {
      if (target == null)
      {
        CUI.Warning($"Style target is null");
        return;
      }

      CUITypeMetaData meta = CUITypeMetaData.Get(target.GetType());

      foreach (string name in style.Props.Keys)
      {
        ApplyStylePropOn(style, name, target, meta);
      }
    }


    public static string CUIPalettePrefix = "CUIPalette.";
    /// <summary>
    /// Applies 1 prop with name on the target
    /// </summary>
    public static void ApplyStylePropOn(CUIStyle style, string name, CUIComponent target, CUITypeMetaData meta = null)
    {
      if (target.Unreal) return;

      if (target == null)
      {
        CUI.Warning($"Style target is null");
        return;
      }

      meta ??= CUITypeMetaData.Get(target.GetType());

      PropertyInfo pi = meta.Assignable.GetValueOrDefault(name);

      if (pi == null) return;


      string raw = style[name];

      object value = null;
      if (raw.StartsWith(CUIPalettePrefix))
      {
        value = CUIPalette.Extract(raw.Substring(CUIPalettePrefix.Length));
        if (value == null)
        {
          CUI.Warning($"Can't find {raw.Substring(CUIPalettePrefix.Length)} in palette");
          return;
        }
      }
      else
      {
        MethodInfo parse = CUIExtensions.Parse.GetValueOrDefault(pi.PropertyType);

        parse ??= pi.PropertyType.GetMethod(
          "Parse",
          BindingFlags.Public | BindingFlags.Static,
          new Type[] { typeof(string) }
        );

        if (parse == null)
        {
          CUI.Warning($"Can't parse style prop {name} for {target} because it's type {pi.PropertyType.Name} is missing Parse method");
          return;
        }

        try
        {
          value = parse.Invoke(null, new object[] { raw });
        }
        catch (Exception e)
        {
          CUI.Warning($"Can't parse {raw} into {pi.PropertyType.Name} for {target}");
        }
      }

      try
      {
        pi.SetValue(target, value);
      }
      catch (Exception e)
      {
        CUI.Warning($"Can't set style prop {name} with value {value} on {target}");
      }
    }
  }



}