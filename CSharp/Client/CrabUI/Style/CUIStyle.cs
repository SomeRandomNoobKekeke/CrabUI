using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{

  /// <summary>
  /// In Fact just an observable dict
  /// </summary>
  public partial class CUIStyle : IEnumerable<KeyValuePair<string, string>>, ICloneable
  {
    public static CUIStyle DefaultFor(Type T) => CUITypeMetaData.Get(T).DefaultStyle;
    public static CUIStyle DefaultFor<T>() where T : CUIComponent => CUITypeMetaData.Get(typeof(T)).DefaultStyle;

    IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
      => Props.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => Props.GetEnumerator();

    public void Add(string key, string value)
    {
      Props[key] = value;
      OnPropChanged?.Invoke(key, value);
    }

    public void Clear() => Props.Clear();

    /// <summary>
    /// Prop name -> value
    /// </summary>
    public Dictionary<string, string> Props = new();

    public event Action<string, string> OnPropChanged;
    public event Action<CUIStyle> OnUse;

    public virtual string this[string name]
    {
      get => Props.ContainsKey(name) ? Props[name] : "";
      set => Add(name, value);
    }

    public object Clone()
    {
      CUIStyle style = new CUIStyle();
      style.Props = new Dictionary<string, string>(Props);
      return style;
    }

    public static CUIStyle Merge(CUIStyle baseStyle, CUIStyle addedStyle)
    {
      CUIStyle result = new CUIStyle();

      if (baseStyle != null)
      {
        foreach (string key in baseStyle.Props.Keys)
        {
          result[key] = baseStyle.Props[key];
        }
      }

      if (addedStyle != null)
      {
        foreach (string key in addedStyle.Props.Keys)
        {
          result[key] = addedStyle.Props[key];
        }
      }

      return result;
    }

    public void Use(CUIStyle source)
    {
      Props = new Dictionary<string, string>(source.Props);
      OnUse?.Invoke(this);
    }

    public void UseString(string raw)
    {
      Clear();

      try
      {
        string content = raw.Split('{', '}')[1];
        if (content.Trim() == "") return;
        var pairs = content.Split(',').Select(s => s.Split(':').Select(sub => sub.Trim()).ToArray());

        foreach (var pair in pairs)
        {
          Props[pair[0]] = pair[1];
        }
      }
      catch (Exception e) { CUI.Warning($"Style.UseString failed: {e.Message}"); }
      OnUse?.Invoke(this);
    }

    public void UseXML(XElement element)
    {
      Clear();
      foreach (XElement prop in element.Elements())
      {
        Props[prop.Name.ToString()] = prop.Value;
      }
      OnUse?.Invoke(this);
    }

    public static CUIStyle FromXML(XElement element)
    {
      CUIStyle style = new CUIStyle();
      style.UseXML(element);
      return style;
    }

    public override string ToString()
    {
      return "{ " + String.Join(", ", Props.Select(kvp => $"{kvp.Key} : {kvp.Value}")) + " }";
    }

    public static CUIStyle Parse(string raw)
    {
      CUIStyle style = new CUIStyle();
      style.UseString(raw);
      return style;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is CUIStyle styleB)) return false;
      CUIStyle styleA = this;
      if (styleA is null && styleB is null) return true;
      if (styleA is null || styleB is null) return false;
      if (styleA.Props is null || styleB.Props is null) return false;
      if (styleA.Props.Count != styleB.Props.Count) return false;
      foreach (var (key, value) in styleA.Props)
      {
        if (!styleB.Props.ContainsKey(key)) return false;
        if (styleA[key] != styleB[key]) return false;
      }
      return true;
    }

    public static CUIStyle operator +(CUIStyle styleA, CUIStyle styleB) => Merge(styleA, styleB);

    public static bool operator ==(CUIStyle styleA, CUIStyle styleB)
    {
      if (styleA is null && styleB is null) return true;
      if (styleA is null || styleB is null) return false;
      if (styleA.Props is null || styleB.Props is null) return false;
      if (styleA.Props.Count != styleB.Props.Count) return false;
      foreach (var (key, value) in styleA.Props)
      {
        if (!styleB.Props.ContainsKey(key)) return false;
        if (styleA[key] != styleB[key]) return false;
      }
      return true;
    }

    public static bool operator !=(CUIStyle styleA, CUIStyle styleB)
    {
      if (styleA is null && styleB is null) return false;
      if (styleA is null || styleB is null) return true;
      if (styleA.Props is null || styleB.Props is null) return true;
      if (styleA.Props.Count != styleB.Props.Count) return true;
      foreach (var (key, value) in styleA.Props)
      {
        if (!styleB.Props.ContainsKey(key)) return true;
        if (styleA[key] != styleB[key]) return true;
      }
      return false;
    }



  }



}