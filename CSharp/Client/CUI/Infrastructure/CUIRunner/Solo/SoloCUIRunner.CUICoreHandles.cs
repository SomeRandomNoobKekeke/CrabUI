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
    public class CUICoreHandlesImp(string modDir) : CUICore.CUICoreHandles
    {
      public string ModDir { get; set; } = modDir;

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
    }
  }

}