using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;
namespace CrabUI
{

  public class CUIStyleLoader
  {
    internal static void InitStatic()
    {
      CUI.OnInit += () => LoadDefaultStyles();
    }

    public static string DefaultStylesPath => Path.Combine(CUI.CUIAssetsPath, "Default Styles.xml");


    public static void LoadDefaultStyles()
    {
      if (!File.Exists(DefaultStylesPath)) return;


      Dictionary<Type, CUIStyle> DefaultStyles = new();


      XDocument xdoc = XDocument.Load(DefaultStylesPath);
      XElement root = xdoc.Element("DefaultStyles");
      foreach (XElement componentStyle in root.Elements())
      {
        Type componentType = CUIReflection.GetComponentTypeByName(componentStyle.Name.ToString());
        if (componentType == null)
        {
          CUI.Warning($"Couldn't find type for default style {componentStyle.Name}");
          continue;
        }

        DefaultStyles[componentType] = CUIStyle.FromXML(componentStyle);
      }

      Stopwatch sw = new Stopwatch();
      sw.Start();
      foreach (Type T in DefaultStyles.Keys)
      {
        CUITypeMetaData.Get(T).DefaultStyle = DefaultStyles[T];
      }
      sw.Stop();
      //CUI.Log(sw.ElapsedMilliseconds);
    }
  }




}