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
    public void SaveTo(string path)
    {
      XDocument xdoc = new XDocument();
      xdoc.Add(ToXML());
      CUICore.SaveXDoc(xdoc, path);
    }

    public static T LoadFrom<T>(string path) where T : CUIComponent => (T)LoadFrom(path);
    public static CUIComponent LoadFrom(string path)
    {
      XDocument xdoc = CUICore.LoadXDoc(path); ;
      return CUIComponent.CreateFromXML(xdoc.Root);
    }


    public void DebugSaveTo(string path, CUICore.CUICoreIOHandle IOHandle)
    {
      XDocument xdoc = new XDocument();
      xdoc.Add(ToXML());
      IOHandle.SaveXDoc(xdoc, path);
    }

    public static T DebugLoadFrom<T>(string path, CUICore.CUICoreIOHandle IOHandle) where T : CUIComponent
       => (T)DebugLoadFrom(path, IOHandle);
    public static CUIComponent DebugLoadFrom(string path, CUICore.CUICoreIOHandle IOHandle)
    {
      XDocument xdoc = IOHandle.LoadXDoc(path);
      return CUIComponent.CreateFromXML(xdoc.Root);
    }

  }
}