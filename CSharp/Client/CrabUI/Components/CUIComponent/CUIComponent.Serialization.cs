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
    public record CompareResult(bool equal, string firstMismatch = "")
    {
      public static implicit operator bool(CompareResult r) => r.equal;
    }

    public static bool DeepCompareVerbose(CUIComponent a, CUIComponent b)
    {
      CompareResult result = DeepCompare(a, b);
      if (result.equal) CUI.Log($"{a} == {b}");
      else CUI.Log($"{result.firstMismatch}");
      return result.equal;
    }
    public static CompareResult DeepCompare(CUIComponent a, CUIComponent b)
    {
      if (a.GetType() != b.GetType()) return new CompareResult(false, $"type mismatch: {a} | {b}");

      Type T = a.GetType();
      CUITypeMetaData meta = CUITypeMetaData.Get(T);

      foreach (var (key, pi) in meta.Serializable)
      {
        if (!object.Equals(pi.GetValue(a), pi.GetValue(b)))
        {
          return new CompareResult(false, $"{pi}: {a}{pi.GetValue(a)} | {b}{pi.GetValue(b)}");
        }
      }

      if (a.Children.Count != b.Children.Count)
      {
        return new CompareResult(false, $"child count mismatch: {a}{CUI.ArrayToString(a.Children)} | {b}{CUI.ArrayToString(b.Children)}");
      }

      for (int i = 0; i < a.Children.Count; i++)
      {
        CompareResult sub = DeepCompare(a.Children[i], b.Children[i]);
        if (!sub.equal) return sub;
      }

      return new CompareResult(true);
    }

    #region State --------------------------------------------------------

    /// <summary>
    /// State is just a clone component with copies of all props
    /// </summary>
    public Dictionary<string, CUIComponent> States { get; set; } = new();
    // TODO why all clones are unreal? this is sneaky, and i don't remember what's it for
    public CUIComponent Clone()
    {
      CUIComponent clone = new CUIComponent()
      {
        Unreal = true,
      };
      clone.ApplyState(this);
      return clone;
    }

    public void SaveStateAs(string name) => States[name] = this.Clone();
    public void LoadState(string name) => ApplyState(States.GetValueOrDefault(name));
    public void ForgetState(string name) => States.Remove(name);

    //TODO think about edge cases (PassPropsToChild)
    public void ApplyState(CUIComponent state)
    {
      Stopwatch sw = Stopwatch.StartNew();
      if (state == null) return;

      //TODO why not closest relative?
      Type targetType = state.GetType() == GetType() ? GetType() : typeof(CUIComponent);

      CUITypeMetaData meta = CUITypeMetaData.Get(targetType);

      //TODO Megacringe, fix it
      foreach (PropertyInfo pi in meta.Serializable.Values)
      {
        if (pi.PropertyType.IsValueType || pi.PropertyType == typeof(string))
        {
          pi.SetValue(this, pi.GetValue(state));
        }
        else
        {
          object value = pi.GetValue(state);
          if (value == null)
          {
            pi.SetValue(this, null);
            continue;
          }

          if (pi.PropertyType.IsAssignableTo(typeof(ICloneable)))
          {
            ICloneable cloneable = (ICloneable)pi.GetValue(state);
            object clone = cloneable.Clone();
            pi.SetValue(this, clone);
          }
          else
          {
            CUI.Info($"Ekhem, can't copy {pi} prop from {state} to {this} because it's not cloneable");
          }
        }
      }

      //TODO Megacringe, fix it
      foreach (PropertyInfo pi in meta.Serializable.Values)
      {
        if (pi.PropertyType.IsValueType && !object.Equals(pi.GetValue(state), pi.GetValue(this)))
        {
          pi.SetValue(this, pi.GetValue(state));
        }
      }

    }

    #endregion
    #region XML --------------------------------------------------------

    public static bool ForceSaveAllProps { get; set; } = false;
    public static bool SaveAfterLoad { get; set; } = true;

    public string SavePath { get; set; }

    public virtual XElement ToXML(CUIAttribute propAttribute = CUIAttribute.CUISerializable)
    {
      try
      {
        if (Unserializable) return null;

        Type type = GetType();

        XElement e = new XElement(type.Name);

        PackProps(e, propAttribute);

        foreach (CUIComponent child in Children)
        {
          if (!this.BreakSerialization)
          {
            e.Add(child.ToXML(propAttribute));
          }
        }

        return e;
      }
      catch (Exception e)
      {
        CUI.Warning(e);
        return new XElement("Error", e.Message);
      }
    }


    public virtual void FromXML(XElement element, string baseFolder = null)
    {
      foreach (XElement childElement in element.Elements())
      {
        Type childType = CUIReflection.GetComponentTypeByName(childElement.Name.ToString());
        if (childType == null) continue;

        CUIComponent child = (CUIComponent)Activator.CreateInstance(childType);
        child.FromXML(childElement, baseFolder);

        //CUI.Log($"{this}[{child.AKA}] = {child} ");
        this.Append(child, child.AKA);
      }

      ExtractProps(element, baseFolder);
    }

    protected void ExtractProps(XElement element, string baseFolder = null)
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


        Func<string, object> ParseWithContext = null;
        //HACK
        if (prop.PropertyType == typeof(CUISprite) && baseFolder != null)
        {
          ParseWithContext = (raw) => CUISprite.ParseWithContext(raw, baseFolder);
        }


        if (parse == null)
        {
          if (prop.PropertyType.IsEnum)
          {
            try
            {
              prop.SetValue(this, Enum.Parse(prop.PropertyType, attribute.Value));
            }
            catch (Exception e)
            {
              CUIDebug.Error($"Can't parse {attribute.Value} into {prop.PropertyType.Name}\n{e}");
            }
          }
          else
          {
            CUIDebug.Error($"Can't parse prop {prop.Name} in {type.Name} because it's type {prop.PropertyType.Name} is missing Parse method");
          }
        }
        else
        {
          try
          {
            object result = null;
            if (ParseWithContext != null)
            {
              result = ParseWithContext(attribute.Value);
            }
            else
            {
              result = parse.Invoke(null, new object[] { attribute.Value });
            }
            prop.SetValue(this, result);
          }
          catch (Exception e)
          {
            CUIDebug.Error($"Can't parse {attribute.Value} into {prop.PropertyType.Name}\n{e}");
          }
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
        try
        {
          object value = props[key].GetValue(this);
          // it's default value for this prop
          if (!ForceSaveAllProps && meta.Default != null && Object.Equals(value, CUIReflection.GetNestedValue(meta.Default, key)))
          {
            continue;
          }

          MethodInfo customToString = CUIExtensions.CustomToString.GetValueOrDefault(props[key].PropertyType);

          if (customToString != null)
          {
            element?.SetAttributeValue(key, customToString.Invoke(null, new object[] { value }));
          }
          else
          {
            element?.SetAttributeValue(key, value);
          }
        }
        catch (Exception e)
        {
          CUI.Warning($"Failed to serialize prop: {e.Message}");
          CUI.Warning($"{key} in {this}");
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
    public static CUIComponent Deserialize(string raw, string baseFolder = null)
    {
      return Deserialize(XElement.Parse(raw));
    }

    public static CUIComponent Deserialize(XElement e, string baseFolder = null)
    {
      try
      {
        Type type = CUIReflection.GetComponentTypeByName(e.Name.ToString());
        if (type == null) return null;

        CUIComponent c = (CUIComponent)Activator.CreateInstance(type);
        // c.RemoveAllChildren();
        c.FromXML(e, baseFolder);
        CUIComponent.RunRecursiveOn(c, (component) => component.Hydrate());

        return c;
      }
      catch (Exception ex)
      {
        CUIDebug.Error(ex);
        return null;
      }
    }

    public void LoadSelfFromFile(string path, bool searchForSpritesInTheSameFolder = true, bool saveAfterLoad = false)
    {
      try
      {
        XDocument xdoc = XDocument.Load(path);

        RemoveAllChildren();
        if (searchForSpritesInTheSameFolder) FromXML(xdoc.Root, Path.GetDirectoryName(path));
        else FromXML(xdoc.Root);

        CUIComponent.RunRecursiveOn(this, (component) => component.Hydrate());
        SavePath = path;

        if (SaveAfterLoad && saveAfterLoad) SaveToTheSamePath();
      }
      catch (Exception ex)
      {
        CUI.Warning(ex);
      }
    }

    public static CUIComponent LoadFromFile(string path, bool searchForSpritesInTheSameFolder = true, bool saveAfterLoad = false)
    {
      try
      {
        XDocument xdoc = XDocument.Load(path);
        CUIComponent result;
        if (searchForSpritesInTheSameFolder)
        {
          result = Deserialize(xdoc.Root, Path.GetDirectoryName(path));
        }
        else result = Deserialize(xdoc.Root);

        result.SavePath = path;

        if (SaveAfterLoad && saveAfterLoad) result.SaveToTheSamePath();

        return result;
      }
      catch (Exception ex)
      {
        CUIDebug.Error(ex);
        return null;
      }
    }

    public static T LoadFromFile<T>(string path, bool searchForSpritesInTheSameFolder = true, bool saveAfterLoad = false) where T : CUIComponent
    {
      try
      {
        XDocument xdoc = XDocument.Load(path);
        T result;
        if (searchForSpritesInTheSameFolder)
        {
          result = (T)Deserialize(xdoc.Root, Path.GetDirectoryName(path));
        }
        else result = (T)Deserialize(xdoc.Root);

        result.SavePath = path;

        if (SaveAfterLoad && saveAfterLoad) result.SaveToTheSamePath();

        return result;
      }
      catch (Exception ex)
      {
        CUIDebug.Error(ex);
        return null;
      }
    }

    public void LoadFromTheSameFile()
    {
      if (SavePath == null)
      {
        CUI.Warning($"Can't load {this} from The Same Path, SavePath is null");
        return;
      }
      LoadSelfFromFile(SavePath);
    }

    public void SaveToTheSamePath()
    {
      if (SavePath == null)
      {
        CUI.Warning($"Can't save {this} To The Same Path, SavePath is null");
        return;
      }
      SaveToFile(SavePath);
    }

    public void SaveToFile(string path, CUIAttribute propAttribute = CUIAttribute.CUISerializable)
    {
      try
      {
        XDocument xdoc = new XDocument();
        xdoc.Add(this.ToXML(propAttribute));
        xdoc.Save(path);
        SavePath = path;
      }
      catch (Exception e)
      {
        CUI.Warning(e);
      }
    }

    /// <summary>
    /// Experimental method  
    /// Here you can add data/ callbacks/ save stuff to variables  
    /// after loading a xml skeletom
    /// </summary>
    public virtual void Hydrate()
    {

    }

    #endregion
  }
}