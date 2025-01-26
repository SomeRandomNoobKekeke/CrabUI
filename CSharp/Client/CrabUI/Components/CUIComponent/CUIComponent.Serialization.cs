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
using HarmonyLib;

namespace CrabUI
{
  public partial class CUIComponent
  {
    #region State --------------------------------------------------------

    /// <summary>
    /// State is just a clone component with copies of all props
    /// </summary>
    public Dictionary<string, CUIComponent> States { get; set; } = new();
    public CUIComponent Clone()
    {
      CUIComponent clone = new CUIComponent();
      clone.ApplyState(this);
      return clone;
    }

    public void SaveStateAs(string name) => States[name] = this.Clone();
    public void LoadState(string name) => ApplyState(States.GetValueOrDefault(name));
    public void ForgetState(string name) => States.Remove(name);

    //TODO think about edge cases (PassPropsToChild)
    public void ApplyState(CUIComponent state)
    {
      if (state == null) return;

      Type targetType = state.GetType() == GetType() ? GetType() : typeof(CUIComponent);

      CUITypeMetaData meta = CUITypeMetaData.Get(targetType);

      foreach (PropertyInfo pi in meta.Serializable.Values)
      {
        pi.SetValue(this, pi.GetValue(state));
      }
    }

    #endregion
    #region XML --------------------------------------------------------

    public virtual XElement ToXML(CUIAttribute propAttribute = CUIAttribute.CUISerializable)
    {
      if (Unserializable) return null;

      Type type = GetType();

      XElement e = new XElement(type.Name);

      PackProps(e, propAttribute);

      foreach (CUIComponent child in Children)
      {
        e.Add(child.ToXML(propAttribute));
      }

      return e;
    }


    public virtual void FromXML(XElement element)
    {
      foreach (XElement childElement in element.Elements())
      {
        Type childType = CUIReflection.GetComponentTypeByName(childElement.Name.ToString());
        if (childType == null) continue;

        CUIComponent child = (CUIComponent)Activator.CreateInstance(childType);
        child.FromXML(childElement);

        //CUI.log($"{this}[{child.AKA}] = {child} ");
        this.Append(child, child.AKA);
      }

      ExtractProps(element);
    }

    protected void ExtractProps(XElement element)
    {
      Type type = GetType();

      CUITypeMetaData meta = CUITypeMetaData.Get(type);

      foreach (XAttribute attribute in element.Attributes())
      {
        if (!meta.Serializable.ContainsKey(attribute.Name.ToString()))
        {
          CUIDebug.Error($"Can't parse prop {attribute.Name} in {type.Name} because type metadata doesn't contain that prop (is it a property? fields aren't supported yet)");
          continue;
        }

        PropertyInfo prop = meta.Serializable[attribute.Name.ToString()];

        MethodInfo parse = null;
        if (CUIExtensions.Parse.ContainsKey(prop.PropertyType))
        {
          parse = CUIExtensions.Parse[prop.PropertyType];
        }

        parse ??= prop.PropertyType.GetMethod(
          "Parse",
          BindingFlags.Public | BindingFlags.Static,
          new Type[] { typeof(string) }
        );


        if (parse == null)
        {
          CUIDebug.Error($"Can't parse prop {prop.Name} in {type.Name} because it's type {prop.PropertyType.Name} is missing Parse method");
          continue;
        }

        try
        {
          object result = parse.Invoke(null, new object[] { attribute.Value });
          prop.SetValue(this, result);
        }
        catch (Exception e)
        {
          CUIDebug.Error($"Can't parse {attribute.Value} into {prop.PropertyType.Name}\n{e}");
        }
      }
    }

    protected void PackProps(XElement element, CUIAttribute propAttribute = CUIAttribute.CUISerializable)
    {
      Type type = GetType();
      CUITypeMetaData meta = CUITypeMetaData.Get(type);

      SortedDictionary<string, PropertyInfo> props = propAttribute switch
      {
        CUIAttribute.CUISerializable => meta.Serializable,
        CUIAttribute.Calculated => meta.Calculated,
        _ => meta.Serializable,
      };

      foreach (string key in props.Keys)
      {


        //Note: GetNestedValue is some cryptic guh from first versions, i prob don't need it
        object value = CUIReflection.GetNestedValue(this, key);

        //Info($"{props[key]} {value}");

        // it's default value for this prop
        if (meta.Default != null && Object.Equals(value, CUIReflection.GetNestedValue(meta.Default, key)))
        {
          continue;
        }

        MethodInfo customToString = CUIExtensions.CustomToString.GetValueOrDefault(value.GetType());

        if (customToString != null)
        {
          element?.SetAttributeValue(key, customToString.Invoke(value, new object[] { }));
        }
        else
        {
          element?.SetAttributeValue(key, value);
        }
      }
    }


    public string Serialize(CUIAttribute propAttribute = CUIAttribute.CUISerializable)
    {
      try
      {
        XElement e = this.ToXML(propAttribute);
        return e.ToString();
      }
      catch (Exception e)
      {
        CUI.Error(e);
        return e.Message;
      }
    }
    public static CUIComponent Deserialize(string raw)
    {
      return Deserialize(XElement.Parse(raw));
    }

    public static CUIComponent Deserialize(XElement e)
    {
      try
      {
        Type type = CUIReflection.GetComponentTypeByName(e.Name.ToString());
        if (type == null) return null;

        CUIComponent c = (CUIComponent)Activator.CreateInstance(type);
        // c.RemoveAllChildren();
        c.FromXML(e);

        return c;
      }
      catch (Exception ex)
      {
        CUIDebug.Error(ex);
        return null;
      }
    }

    public static CUIComponent LoadFromFile(string path)
    {
      try
      {
        XDocument xdoc = XDocument.Load(path);
        return Deserialize(xdoc.Root);
      }
      catch (Exception ex)
      {
        CUIDebug.Error(ex);
        return null;
      }
    }

    public static T LoadFromFile<T>(string path) where T : CUIComponent
    {
      try
      {
        XDocument xdoc = XDocument.Load(path);
        return (T)Deserialize(xdoc.Root);
      }
      catch (Exception ex)
      {
        CUIDebug.Error(ex);
        return null;
      }
    }
    public void SaveToFile(string path, CUIAttribute propAttribute = CUIAttribute.CUISerializable)
    {
      XDocument xdoc = new XDocument();
      xdoc.Add(this.ToXML(propAttribute));
      xdoc.Save(path);
    }

    #endregion
  }
}