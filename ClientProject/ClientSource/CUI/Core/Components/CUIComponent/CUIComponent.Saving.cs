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
  public partial class CUIComponent
  {
    public void SaveTo(string path)
    {
      XDocument xdoc = new XDocument();
      xdoc.Add(Serialize());
      CUICore.SaveXDoc(xdoc, path);
    }

    public static T LoadFrom<T>(string path) where T : CUIComponent => (T)LoadFrom(path);
    public static CUIComponent LoadFrom(string path)
    {
      XDocument xdoc = CUICore.LoadXDoc(path); ;
      return CUIComponent.Deserialize(xdoc.Root);
    }
  }
}