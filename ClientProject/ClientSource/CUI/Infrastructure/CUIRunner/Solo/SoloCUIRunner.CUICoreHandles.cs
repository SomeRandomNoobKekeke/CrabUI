using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;


using System.IO;
using System.Xml;
using System.Xml.Linq;
namespace CrabUI
{
  public partial class SoloCUIRunner
  {
    public partial class CUICoreHandles_Part : Part, CUICore.CUICoreHandles
    {
      public bool IsRelXMLPath(string path)
      {
        return path.EndsWith(".xml") && !Path.IsPathFullyQualified(path);
      }

      public CUIGraphicsDevice GraphicsDevice => Self.GraphicsDevice;
      public CUIGUI GUI => Self.CUIGUI;

      public CUITextureManager TextureManager => Self.TextureManager;
      public ResourceIOContextHandle ResourceIOContext => Self.ResourceIOContextHandle;
      public CUICore.OtherCUICoreResources OtherResources => Self._OtherResources;


      public bool InputBlockingMenuOpen => Barotrauma.GUI.InputBlockingMenuOpen;
      public bool IsMouseOnVanillaGUIComponent { get; set; }
      public bool VanillaGUIComponentFocused { get; set; }


      public void SaveXDoc(XDocument xDoc, string path)
      {
        string realPath = Self.FilePathResolver.FindBestMatchForSaving(path);
        xDoc.Save(realPath);
      }
      public XDocument LoadXDoc(string path)
      {
        string realPath = Self.FilePathResolver.FindBestMatchForLoading(path);
        return XDocument.Load(realPath);
      }

      public void GrabFocus(IFocusable focusable)
      {
        throw new NotImplementedException();
      }
    }
  }

}