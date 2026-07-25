using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  public partial class CUIVisualComponent : CUISerializable
  {
    protected virtual void BeforeSerialization() { }
    protected virtual void AfterSerialization() { }

    [CUISerializableProp]
    public CUISerializationMode SerializationMode { get; set; }

    [CUISerializableProp] // BaroDev(wide)
    public bool Serializable { get; set; } = true;

    static object CUISerializable.Deserialize(XElement element) => Deserialize(element);
    public static T Deserialize<T>(XElement element) where T : CUIVisualComponent => (T)Deserialize(element);
    public static CUIVisualComponent Deserialize(XElement element)
    {
      CUIVisualComponent root = CreateEmptyComponent(element);
      CUIBasicSerializer.DeserializeProps(element, root);

      root.DeserializeChildren(element, root.SerializationMode);

      return root;
    }

    private static CUIVisualComponent CreateEmptyComponent(XElement element)
      => (CUIVisualComponent)Activator.CreateInstance(CUICore.Reflection.GetType(element.Name.ToString()));

    private void DeserializeChildren(XElement element, CUISerializationMode mode)
    {
      CUIVisualComponent AddNewChild(XElement element)
      {
        CUIVisualComponent child = CreateEmptyComponent(element);
        CUIBasicSerializer.DeserializeProps(element, child);
        Children.Add(child);
        return child;
      }
      CUIVisualComponent ReplaceWithANewChild(XElement element)
      {
        CUIVisualComponent child = CreateEmptyComponent(element);
        CUIBasicSerializer.DeserializeProps(element, child);
        this[child.AKA] = child;
        return child;
      }
      void MergeIntoExistingChild(CUIVisualComponent child, XElement element)
      {
        CUIBasicSerializer.DeserializeProps(element, child);
      }

      BeforeSerialization();
      foreach (XElement childElement in element.Elements())
      {
        string AKA = childElement.GetAttribute("AKA")?.Value;

        CUIVisualComponent child = null;
        if (AKA == null || !NamedComponents.ContainsKey(AKA))
        {
          child = AddNewChild(childElement);
        }
        else // There's a name conflict
        {
          if (mode == CUISerializationMode.Replace)
          {
            child = this[AKA];
            if (child.Serializable) child = ReplaceWithANewChild(childElement);
          }

          if (mode == CUISerializationMode.Merge)
          {
            child = this[AKA];
            if (child.Serializable) MergeIntoExistingChild(child, childElement);
          }

          if (mode == CUISerializationMode.Ignore)
          {
            child = this[AKA];
          }
        }

        child.DeserializeChildren(childElement, mode);
      }
      AfterSerialization();
    }




    public virtual XElement Serialize()
    {
      XElement element = CUIBasicSerializer.Serialize(this, Info.DefaultValue.As_Dictionary);

      foreach (CUIVisualComponent child in Children)
      {
        if (!child.Serializable) continue;
        element.Add(child.Serialize());
      }

      return element;
    }
  }
}