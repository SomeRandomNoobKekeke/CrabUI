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

namespace CursedUI
{
  public partial class CUIComponent
  {
    public new static CUIComponent LoadFrom(string path)
      => CUIVisualComponent.LoadFrom(path) as CUIComponent;
  }
}