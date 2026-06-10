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

      public string ModDir => Self.ModDir;

      public CUIGraphicsDevice GraphicsDevice => Self.GraphicsDevice;
      public CUIGUI GUI => Self.CUIGUI;

      public string NormalizePath(string path)
        => Path.IsPathFullyQualified(path) ? path : Path.Combine(ModDir, path);


      public void SaveXDoc(XDocument xDoc, string path)
      {
        xDoc.Save(NormalizePath(path));
      }
      public XDocument LoadXDoc(string path)
      {
        return XDocument.Load(NormalizePath(path));
      }

      public CUITexture2D GetTexture(string path)
      {
        return Self.TextureManager.GetTexture(NormalizePath(path));
      }
    }
  }

}