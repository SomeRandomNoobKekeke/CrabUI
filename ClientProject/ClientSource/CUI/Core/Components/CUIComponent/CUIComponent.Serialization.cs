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
    /// <summary>
    /// Override this methods to setup component after deserialization
    /// </summary>
    protected virtual void WireUp() { }

    static object CUISerializable.Deserialize(XElement element) => Deserialize(element);
    public static CUIComponent Deserialize(XElement element)
    {
      CUIComponent component = (CUIComponent)CUIBasicSerializer.Deserialize(
        element,
        CUICore.Reflection.GetType(element.Name.ToString())
      );

      foreach (XElement child in element.Elements())
      {
        component.Children.Add(Deserialize(child));
      }

      component.WireUp();

      return component;
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