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
using System.Text;
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

    public static bool AreEqual(ComponentSnapshot a, ComponentSnapshot b)
      => a.ToString() == b.ToString();

    public static string CreateDiffString(ComponentSnapshot a, ComponentSnapshot b)
      => CreateDiffString(a.ToString(), b.ToString());
    public static string CreateDiffString(string a, string b)
    {
      StringBuilder sb = new StringBuilder();

      Color cl = Color.White;

      void EnterColor(Color newCl)
      {
        if (newCl == cl) return;
        cl = newCl;

        sb.Append($"‖color:{cl.R},{cl.G},{cl.B}‖");
      }

      void ExitColor()
      {
        if (Color.White == cl) return;
        cl = Color.White;

        sb.Append($"‖end‖");
      }

      void ChangeColor(Color newCl)
      {
        ExitColor();
        EnterColor(newCl);
      }

      EnterColor(Color.Gray);

      int minLength = Math.Min(a.Length, b.Length);

      for (int i = 0; i < minLength; i++)
      {
        if (a[i] != b[i])
        {
          ChangeColor(Color.Cyan);
        }
        else
        {
          ChangeColor(Color.Gray);
        }

        sb.Append(a[i]);
      }

      ExitColor();

      return sb.ToString();
    }
  }
}