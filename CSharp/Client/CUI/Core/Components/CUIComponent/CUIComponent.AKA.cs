using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    /// <summary>
    /// Parent can memorize its children by their names, AKA
    /// </summary>
    public string AKA { get; set; }

    /// <summary>
    /// You can access NamedComponents with this indexer
    /// </summary>
    public CUIComponent this[string name]
    {
      get => Get(name);
      set
      {
        if (value is null) return;

        if (value.Parent != null)
        {
          Remember(value, name);
          return;
        }

        if (NamedComponents.ContainsKey(name))
        {
          int i = Tree.Children.IndexOf(NamedComponents[name]);
          Tree.RemoveChild(NamedComponents[name]); //TODO this should be a replace child method
          Tree.Insert(value, i, name);
          return;
        }

        Tree.Append(value, name);
      }
    }

    /// <summary>
    /// Gets the parent, like that A["^"] == A.Parent, A["B.^"] == A  
    /// I'm not sure about the symbol, suggestions are welcomed
    /// </summary>
    public static string GetParentLitteral { get; } = "^";

    /// <summary>
    /// All memorized components
    /// </summary>
    public Dictionary<string, CUIComponent> NamedComponents { get; } = new();

    public CUIComponent Remember(CUIComponent c, string name)
    {
      NamedComponents[name] = c;
      c.AKA = name;
      return c;
    }
    /// <summary>
    /// If it already has AKA
    /// </summary>
    public CUIComponent Remember(CUIComponent c)
    {
      if (c.AKA != null) NamedComponents[c.AKA] = c;
      return c;
    }

    public CUIComponent Forget(string name)
    {
      if (name == null) return null;
      if (!NamedComponents.ContainsKey(name)) return null;
      CUIComponent c = NamedComponents[name];
      NamedComponents.Remove(name);
      return c;
    }
    /// <summary>
    /// If it already has AKA
    /// </summary>
    public CUIComponent Forget(CUIComponent c)
    {
      if (c?.AKA != null) NamedComponents.Remove(c.AKA);
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
    public virtual CUIComponent Get(string name)
    {
      if (name == null || name == "") return null;
      name = name.Trim();

      if (NamedComponents.ContainsKey(name)) return NamedComponents[name];

      CUIComponent component = this;
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
    public T Get<T>(string name) where T : CUIComponent => (T)Get(name);

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