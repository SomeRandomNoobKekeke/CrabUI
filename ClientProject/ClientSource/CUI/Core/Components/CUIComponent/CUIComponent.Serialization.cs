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
    static object CUISerializable.Deserialize(XElement element) => Deserialize(element);
    public static CUIComponent Deserialize(XElement element)
    {
      CUIComponent component = (CUIComponent)CUIDefaultSerializer.Deserialize(
        element,
        CUICore.Reflection.GetType(element.Name.ToString())
      );

      foreach (XElement child in element.Elements())
      {
        component.Children.Add(Deserialize(child));
      }

      return component;
    }



    public virtual XElement Serialize()
    {
      XElement element = CUIDefaultSerializer.Serialize(this, Info.DefaultValue.As_Dictionary);

      foreach (CUIComponent child in Children)
      {
        element.Add(child.Serialize());
      }

      return element;
    }
  }
}