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
  public partial class CUIComponent : CUISerializable
  {
    protected virtual void BeforeSerialization() { }
    protected virtual void AfterSerialization() { }

    public CUISerializationMode SerializationMode { get; set; }

    static object CUISerializable.Deserialize(XElement element) => Deserialize(element);
    public static CUIComponent Deserialize(XElement element)
    {
      CUIComponent root = CreateEmptyComponent(element);
      CUIBasicSerializer.DeserializeProps(element, root);

      root._DeserializeChildren(element);

      return root;
    }

    private static CUIComponent CreateEmptyComponent(XElement element)
      => (CUIComponent)Activator.CreateInstance(CUICore.Reflection.GetType(element.Name.ToString()));
    private void _DeserializeChildren(XElement element)
    {
      CUIComponent AddNewChild(XElement element)
      {
        CUIComponent child = CreateEmptyComponent(element);
        CUIBasicSerializer.DeserializeProps(element, child);
        Children.Add(child);
        return child;
      }
      CUIComponent ReplaceWithANewChild(XElement element)
      {
        CUIComponent child = CreateEmptyComponent(element);
        CUIBasicSerializer.DeserializeProps(element, child);
        this[child.AKA] = child;
        return child;
      }
      void MergeIntoExistingChild(CUIComponent child, XElement element)
      {
        CUIBasicSerializer.DeserializeProps(element, child);
      }

      BeforeSerialization();
      foreach (XElement childElement in element.Elements())
      {
        string AKA = childElement.GetAttribute("AKA")?.Value;

        CUIComponent child = null;
        if (AKA == null || !NamedComponents.ContainsKey(AKA))
        {
          child = AddNewChild(childElement);
        }
        else // There's a name conflict
        {
          if (SerializationMode == CUISerializationMode.Replace)
          {
            child = ReplaceWithANewChild(childElement);
          }

          if (SerializationMode == CUISerializationMode.Merge)
          {
            child = this[AKA];
            MergeIntoExistingChild(child, childElement);
          }

          if (SerializationMode == CUISerializationMode.Ignore)
          {
            child = this[AKA];
          }
        }

        child._DeserializeChildren(childElement);
      }
      AfterSerialization();
    }




    public virtual XElement Serialize()
    {
      XElement element = CUIBasicSerializer.Serialize(this, Info.DefaultValue.As_Dictionary);

      foreach (CUIComponent child in Children)
      {
        element.Add(child.Serialize());
      }

      return element;
    }
  }
}