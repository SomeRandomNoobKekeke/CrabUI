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

namespace CursedUI
{
  public partial class CUIVisualComponent : CUISerializable
  {
    protected virtual void BeforeSerialization() { }
    protected virtual void AfterSerialization() { }

    [CUISerializableProp]
    public CUISerializationMode SerializationMode { get; set; } = CUISerializationMode.Merge;

    public CUISerializationMode DeepSerializationMode
    {
      get => DeepSerializationMode;
      set
      {
        SerializationMode = value;
        foreach (var child in Children)
        {
          child.DeepSerializationMode = value;
        }
      }
    }

    public bool Serializable { get; set; } = true;

    [CUISerializableProp]
    public bool SerializeChildren { get; set; } = true;

    static object CUISerializable.Deserialize(XElement element) => Deserialize(element);
    public static T Deserialize<T>(XElement element) where T : CUIVisualComponent => (T)Deserialize(element);
    public static CUIVisualComponent Deserialize(XElement element)
    {
      CUIVisualComponent root = CreateEmptyComponent(element);
      CUIBasicSerializer.DeserializeProps(element, root);

      if (root.SerializeChildren)
      {
        root.DeserializeChildren(element);
      }

      return root;
    }

    private static CUIVisualComponent CreateEmptyComponent(XElement element)
      => (CUIVisualComponent)Activator.CreateInstance(CUICore.Reflection.GetType(element.Name.ToString()));

    private void DeserializeChildren(XElement element)
    {
      CUIVisualComponent CreateNewChild(XElement element)
      {
        CUIVisualComponent child = CreateEmptyComponent(element);
        CUIBasicSerializer.DeserializeProps(element, child);
        return child;
      }

      BeforeSerialization();
      foreach (XElement childElement in element.Elements())
      {
        string AKA = childElement.GetAttribute("AKA")?.Value;

        CUISerializationMode mode = CUICore.Parser.Parse<CUISerializationMode>(
          childElement.GetAttribute("SerializationMode")?.Value
        );

        CUIVisualComponent child = null;
        if (AKA == null || !NamedComponents.ContainsKey(AKA))
        {
          child = CreateNewChild(childElement);
          Children.Add(child);
        }
        else // There's a name conflict
        {
          if (mode == CUISerializationMode.Replace)
          {
            this[AKA] = child = CreateNewChild(childElement);
          }

          if (mode == CUISerializationMode.Merge)
          {
            child = this[AKA];
            CUIBasicSerializer.DeserializeProps(childElement, child);
          }

          if (mode == CUISerializationMode.Ignore)
          {
            child = this[AKA];
          }
        }

        if (child.SerializeChildren)
        {
          child.DeserializeChildren(childElement);
        }
      }
      AfterSerialization();
    }

    public virtual XElement Serialize()
    {
      XElement element = CUIBasicSerializer.Serialize(this, Info.DefaultValue.As_Dictionary);

      if (SerializeChildren)
      {
        foreach (CUIVisualComponent child in Children)
        {
          if (!child.Serializable) continue;
          element.Add(child.Serialize());
        }
      }

      return element;
    }
  }
}