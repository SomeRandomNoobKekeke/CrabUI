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
    public partial class CUICoreHandles_Part() : CUICore.CUICoreHandles
    {
      public SoloCUIRunner Self { get; set; }

      public CUIGraphicsDevice GraphicsDevice => Self.GraphicsDevice;
      public CUIGUI GUI => Self.CUIGUI;

      public CUITextureManager CUITextureManager => Self.CUITextureManager;

      public void SaveXDoc(XDocument xDoc, string path)
      {
        xDoc.Save(Self.PathManager.Normalize(path));
      }
      public XDocument LoadXDoc(string path)
      {
        return XDocument.Load(Self.PathManager.Normalize(path));
      }

      public void GrabFocus()
      {
        Barotrauma.GUI.KeyboardDispatcher.Subscriber = null;
      }
    }
  }

}