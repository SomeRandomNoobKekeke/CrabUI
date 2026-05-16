using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;
using System.Xml.Linq;
using System.IO;

namespace CrabUIUser
{
  public class ComponentSnapshot
  {
    public static ComponentSnapshot Take(CUIComponent component, string name)
    {
      return new ComponentSnapshot()
      {
        Name = name,
        Root = TakeXML(component),
      };
    }

    private static XElement TakeXML(CUIComponent component)
    {
      XElement element = new XElement(component.TypeName);

      ExtractAttributes(element, component);

      foreach (CUIComponent child in component.Children)
      {
        element.Add(TakeXML(child));
      }

      return element;
    }

    private static void ExtractAttributes(XElement element, CUIComponent component)
    {
      element.Add(new XAttribute("Real", component.Rect.ToString()));
      element.Add(new XAttribute("AKA", component.AKA ?? ""));
    }


    public static ComponentSnapshot LoadSnapshot(string path)
    {
      if (!File.Exists(path)) return null;

      return new ComponentSnapshot()
      {
        Name = Path.GetFileNameWithoutExtension(path),
        Root = XElement.Parse(File.ReadAllText(path)),
      };
    }

    public string Name { get; private set; } = "";
    public XElement Root { get; private set; } = new XElement("GigaBruh");

    public void Save(string path)
    {
      File.WriteAllText(path, Root.ToString());
    }

    public override string ToString() => Root.ToString();

  }
}