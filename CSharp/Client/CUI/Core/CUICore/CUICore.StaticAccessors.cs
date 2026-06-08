using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using ComponentGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

    public static CUIGraphicsDevice GraphicsDevice => Instance.Handles.GraphicsDevice;
    public static SamplerState SamplerState => Instance.Handles.GUI.SamplerState;
    public static RasterizerState RasterizerState => Instance.Handles.GUI.RasterizerState;

    public static CUIComponentTypeManager CUITypes => Instance.CUIComponentTypeManager;

  }
}