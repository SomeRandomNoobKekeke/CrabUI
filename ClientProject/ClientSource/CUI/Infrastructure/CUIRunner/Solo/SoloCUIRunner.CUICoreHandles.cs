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
      public bool IsRelXMLPath(string path)
      {
        return path.EndsWith(".xml") && !Path.IsPathFullyQualified(path);
      }

      private DummyIKeyboardSubscriber DummyIKeyboardSubscriber = new();


      public SoloCUIRunner Self { get; set; }

      public CUIGraphicsDevice GraphicsDevice => Self.GraphicsDevice;
      public CUIGUI GUI => Self.CUIGUI;

      public CUITextureManager TextureManager => Self.TextureManager;
      public ResourceIOContextHandle ResourceIOContext => Self.ResourceIOContextHandle;

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

      public void GrabFocus()
      {
        Barotrauma.GUI.KeyboardDispatcher.Subscriber = DummyIKeyboardSubscriber;
      }

      public void ClearFocus()
      {
        Barotrauma.GUI.KeyboardDispatcher.Subscriber = null;
      }
    }
  }

}