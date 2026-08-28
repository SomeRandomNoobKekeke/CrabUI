using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
namespace CursedUI
{
  public partial class CUIVisualComponent
  {
    /// <summary>
    /// Parent can memorize its children by their names, AKA
    /// </summary>
    [CUISerializableProp]
    public string AKA { get; set; } = "";

    /// <summary>
    /// You can access NamedComponents with this indexer
    /// </summary>
    public CUIVisualComponent this[string name]
    {
      get => Get(name);
      set
      {
        if (value is null)
        {
          if (NamedComponents.ContainsKey(name))
          {
            Children.Remove(NamedComponents[name]);
          }
          return;
        }

        value.RemoveSelf(); // ensure that parent forgets it by prev aka

        value.AKA = name;

        if (NamedComponents.ContainsKey(name))
        {
          int i = Children.IndexOf(NamedComponents[name]);
          Children[i] = value;
          return;
        }

        Children.Add(value);
      }
    }

    public T As<T>() where T : CUIVisualComponent => this as T;

    /// <summary>
    /// Gets the parent, like that A["^"] == A.Parent, A["B.^"] == A  
    /// I'm not sure about the symbol, suggestions are welcomed
    /// </summary>
    public static string GetParentLitteral { get; } = "^";

    /// <summary>
    /// All memorized components
    /// </summary>
    public Dictionary<string, CUIVisualComponent> NamedComponents { get; } = new();

    public CUIVisualComponent Remember(CUIVisualComponent c, string name)
    {
      NamedComponents[name] = c;
      c.AKA = name;
      return c;
    }
    /// <summary>
    /// If it already has AKA
    /// </summary>
    public CUIVisualComponent Remember(CUIVisualComponent c)
    {
      if (!String.IsNullOrEmpty(c.AKA)) NamedComponents[c.AKA] = c;
      return c;
    }

    public CUIVisualComponent Forget(string name)
    {
      if (name == null) return null;
      if (!NamedComponents.ContainsKey(name)) return null;
      CUIVisualComponent c = NamedComponents[name];
      NamedComponents.Remove(name);
      return c;
    }
    /// <summary>
    /// If it already has AKA
    /// </summary>
    public CUIVisualComponent Forget(CUIVisualComponent c)
    {
      if (!String.IsNullOrEmpty(c?.AKA)) NamedComponents.Remove(c.AKA);
      return c;
    }



    //TODO optimize
    // public string RelativeAKA(CUIComponent relativeTo)
    // {
    //   List<CUIComponent> parents = Parents.ToList();
    //   if (!parents.Contains(relativeTo))
    //   {
    //     CUI.Warning($"Can't get RelativeAKA from [{this}] to [{relativeTo}]: they are not relatives");
    //     return null;
    //   }

    //   if (string.IsNullOrEmpty(this.AKA))
    //   {
    //     CUI.Warning($"Can't get RelativeAKA from [{this}] to [{relativeTo}]: {this} doesn't have AKA");
    //     return null;
    //   }
    //   List<string> AKAs = parents.TakeWhile(c => c != relativeTo).Select(c => c.AKA).ToList();
    //   if (AKAs.Any(aka => string.IsNullOrEmpty(aka)))
    //   {
    //     CUI.Warning($"Can't get RelativeAKA from [{this}] to [{relativeTo}]: some components in the chain doesn't have aka");
    //     return null;
    //   }

    //   AKAs.Reverse();
    //   AKAs.Add(this.AKA);

    //   return string.Join('.', AKAs);
    // }

    /// <summary>
    /// Returns memorized component by name.  
    /// You can chain names with . and get parent with ^
    /// </summary>
    public CUIVisualComponent Get(string name)
    {
      if (name == null || name == "") return null;
      name = name.Trim();

      if (NamedComponents.ContainsKey(name)) return NamedComponents[name];

      CUIVisualComponent component = this;
      string[] commands = name.Split('.');

      foreach (string c in commands)
      {
        if (c == GetParentLitteral)
        {
          if (component.Parent == null)
          {
            CUI.Logger.Warning($"Failed to Get [{name}] from [{this}], [{component}] has no parent");
            return null;
          }

          component = component.Parent;
        }
        else
        {
          if (!component.NamedComponents.ContainsKey(c))
          {
            CUI.Logger.Warning($"Failed to Get [{name}] from [{this}], [{component}] has no [{c}]");
            return null;
          }

          component = component.NamedComponents[c];
        }
      }

      return component;
    }
    public T Get<T>(string name) where T : CUIVisualComponent => (T)Get(name);

    /// <summary>
    /// Prints named components recursivelly,  
    /// This is ridiculously unoptimized, but ok for 1 time use
    /// </summary>
    // public void Examine()
    // {
    //   CUI.Logger.Log($"this = {this}");
    //   foreach (string name in NamedComponents.Keys)
    //   {
    //     CUI.Logger.Log($"this[{Logger.WrapInColor(name, "pink")}] = {NamedComponents[name]}");
    //   }
    //   foreach (CUIComponent child1 in this.Children)
    //   {
    //     foreach (CUIComponent child2 in child1.DeepChildren)
    //     {
    //       CUI.Log($"this[{CUI.WrapInColor(child2.RelativeAKA(this), "pink")}] = {child2}");
    //     }
    //   }
    // }
  }
}