using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Barotrauma;

namespace CrabUI
{
  public class RunnerMouseOnTracker : CUICore.IRunnerMouseOnTracker
  {
    public bool IsMouseOnVanillaGUIComponent { get; set; }
  }
}