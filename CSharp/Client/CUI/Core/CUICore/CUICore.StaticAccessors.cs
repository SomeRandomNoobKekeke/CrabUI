using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using ComponentGenerator;
using Microsoft.Xna.Framework;

using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{

  public partial class CUICore
  {
    public static CUICore Instance => CUI.Core;

    public static void SaveXDoc(XDocument xDoc, string path)
      => Instance.Handles.SaveXDoc(xDoc, path);
    public static XDocument LoadXDoc(string path)
      => Instance.Handles.LoadXDoc(path);
    public static CUITexture2D GetTexture(string path)
      => Instance.Handles.GetTexture(path);
  }
}