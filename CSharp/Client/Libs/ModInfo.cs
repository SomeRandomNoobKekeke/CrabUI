using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace BaroJunk
{

  /// <summary>
  /// Static class with some info about package
  /// Generally a wrapper around this magnificence
  /// public bool TryGetPackageForPlugin<T>(out ContentPackage package) where T : IAssemblyPlugin
  /// </summary>
  public static class ModInfo
  {
    public static string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;
    public static string HookId => Assembly.GetExecutingAssembly().GetName().Name;
    public static string BarotraumaPath => Path.GetFullPath("./");

    public static ContentPackage ModPackage<PluginType>() where PluginType : IAssemblyPlugin
    {
      GameMain.LuaCs.PluginPackageManager.TryGetPackageForPlugin<PluginType>(out ContentPackage package);
      return package;
    }
    public static string ModDir<PluginType>() where PluginType : IAssemblyPlugin => ModPackage<PluginType>().Dir;
    public static string ModVersion<PluginType>() where PluginType : IAssemblyPlugin => ModPackage<PluginType>().ModVersion;
    public static string ModName<PluginType>() where PluginType : IAssemblyPlugin => ModPackage<PluginType>().Name;
  }
}