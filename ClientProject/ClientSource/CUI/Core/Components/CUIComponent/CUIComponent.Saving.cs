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
using System.IO;

namespace CrabUI
{
  public partial class CUIComponent
  {
    public void SaveTo(string path)
    {
      CUICore.ResourceIOContext.CallingAssembly = Assembly.GetCallingAssembly();

      XDocument xdoc = new XDocument();
      xdoc.Add(Serialize());
      CUICore.SaveXDoc(xdoc, path);

      CUICore.ResourceIOContext.CallingAssembly = null;
    }

    public static T LoadFrom<T>(string path) where T : CUIComponent
    {
      CUICore.ResourceIOContext.CallingAssembly = Assembly.GetCallingAssembly();
      CUICore.ResourceIOContext.FileDir = Path.GetDirectoryName(path);

      XDocument xdoc = CUICore.LoadXDoc(path);
      CUIComponent result = CUIComponent.Deserialize(xdoc.Root);

      CUICore.ResourceIOContext.CallingAssembly = null;
      CUICore.ResourceIOContext.FileDir = null;

      return (T)result;
    }
    public static CUIComponent LoadFrom(string path)
    {
      CUICore.ResourceIOContext.CallingAssembly = Assembly.GetCallingAssembly();
      CUICore.ResourceIOContext.FileDir = Path.GetDirectoryName(path);

      XDocument xdoc = CUICore.LoadXDoc(path);
      CUIComponent result = CUIComponent.Deserialize(xdoc.Root);

      CUICore.ResourceIOContext.CallingAssembly = null;
      CUICore.ResourceIOContext.FileDir = null;

      return result;
    }
  }
}