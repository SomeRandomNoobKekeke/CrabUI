using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;
using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  public partial class CUIComponent
  {

    public static CUIComponent CreateByName(string name)
    {
      return (CUIComponent)Activator.CreateInstance(CUI.CUITypes.ByName(name));
    }

    public static CUIComponent CreateFromXML(XElement element)
    {
      CUIComponent component = CreateByName(element.Name.ToString());

      component.ApplyXMLAttributes(element);

      foreach (XElement childElement in element.Elements())
      {
        component.Append(CreateFromXML(childElement));
      }

      return component;
    }


    public void ApplyXML(XElement element)
    {
      ApplyXMLAttributes(element);

      foreach (XElement childElement in element.Elements())
      {

      }
    }

    public void ApplyXMLAttributes(XElement element)
    {
      foreach (XAttribute attribute in element.Attributes())
      {
        this.As_Dictionary[attribute.Name.ToString()] = attribute.Value;
      }
    }


    public XElement ToXML()
    {
      XElement element = new XElement(this.GetType().Name);

      foreach (var (key, value) in this.As_Dictionary)
      {
        element.Add(new XAttribute(key, value));
      }

      foreach (CUIComponent child in Tree.Children)
      {
        element.Add(child.ToXML());
      }

      return element;
    }
  }
}