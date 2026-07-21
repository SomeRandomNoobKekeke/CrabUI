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

      public CUITextureManagerInternal TextureManager => Self.CUITextureManagerInternal;

      public void SaveXDoc(XDocument xDoc, string path, Assembly CallingAssembly)
      {
        if (Path.IsPathFullyQualified(path))
        {
          xDoc.Save(path);
        }
        else
        {
          string callerRoot = Self.DirLookup.GetPackage(Assembly.GetCallingAssembly()).Dir;
          xDoc.Save(Path.Combine(callerRoot, path));
        }
      }
      public XDocument LoadXDoc(string path, Assembly CallingAssembly)
      {
        if (Path.IsPathFullyQualified(path))
        {
          return XDocument.Load(path);
        }
        else
        {
          string callerRoot = Self._AssemblyPackageLookup.GetPackage(Assembly.GetCallingAssembly()).Dir;
          return XDocument.Load(Path.Combine(callerRoot, path));
        }
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