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

using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  public partial class CUIComponent
  {
    #region Debug --------------------------------------------------------
    /// <summary>
    /// Mark component and its children for debug  
    /// Used in debug interface
    /// </summary>
    private bool debug; public bool Debug
    {
      get => debug;
      set
      {
        debug = value;
        //foreach (CUIComponent c in Children) { c.Debug = value; }
      }
    }

    /// <summary>
    /// For debug frame itself
    /// </summary>
    private bool ignoreDebug; public bool IgnoreDebug
    {
      get => ignoreDebug;
      set
      {
        ignoreDebug = value;
        foreach (CUIComponent c in Children) { c.IgnoreDebug = value; }
      }
    }

    public void PrintTree(string offset = "")
    {
      CUI.Log($"{offset}{this}");
      foreach (CUIComponent child in Children)
      {
        child.PrintTree(offset + "|    ");
      }
    }

    /// <summary>
    /// Prints component and then message
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="source"></param>
    /// <param name="lineNumber"></param>
    public void Info(object msg, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    {
      var fi = new FileInfo(source);

      CUI.Log($"{fi.Directory.Name}/{fi.Name}:{lineNumber}", Color.Yellow * 0.5f);
      CUI.Log($"{this} {msg ?? "null"}", Color.Yellow);
    }

    #endregion
    #region AKA --------------------------------------------------------

    /// <summary>
    /// Parent can memorize it's children by their names, AKA
    /// </summary>
    [CUISerializable] public string AKA { get; set; }
    /// <summary>
    /// All memorized components
    /// </summary>
    public Dictionary<string, CUIComponent> NamedComponents { get; set; } = new();

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
      CUIComponent c = NamedComponents.GetValueOrDefault(name);
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

    /// <summary>
    /// You can access NamedComponents with this indexer
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public CUIComponent this[string name]
    {
      get => Get(name);
      set
      {
        if (value.Parent != null) Remember(value, name);
        else Append(value, name);
      }
    }

    /// <summary>
    /// Returns memorized component by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public virtual CUIComponent Get(string name)
    {
      if (name == null) return null;
      if (NamedComponents.ContainsKey(name)) return NamedComponents[name];

      CUIComponent component = this;
      string[] names = name.Split('.');

      foreach (string n in names)
      {
        component = component.NamedComponents.GetValueOrDefault(n);

        if (component == null)
        {
          CUI.Warning($"Failed to Get {name} from {this}, there's no {n}");
          break;
        }
      }

      return component;
    }
    public T Get<T>(string name) where T : CUIComponent => (T)Get(name);

    #endregion
  }
}