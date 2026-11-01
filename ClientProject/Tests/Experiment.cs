using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using System.Xml.Linq;
using CUICodeGenerator;
using Microsoft.Xna.Framework;
using Barotrauma.Plugins;

namespace CUITests
{
  public partial class CUITestPlugin
  {
    public void Experiment()
    {
      Logger.LogVars(PluginLoader.loadedPlugins.First().Data.ContentPackage);
    }
  }
}