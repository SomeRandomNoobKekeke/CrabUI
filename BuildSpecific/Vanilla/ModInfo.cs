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
using Barotrauma.Plugins;

namespace CUILibs
{

  /// <summary>
  /// Static class with some info about package
  /// Generally a wrapper around this magnificence
  /// public bool TryGetPackageForPlugin<T>(out ContentPackage package) where T : IAssemblyPlugin
  /// </summary>
  public static class ModInfo
  {
    //https://steamcommunity.com/sharedfiles/filedetails/?id=3693624471
    public static string LuaCsForBarotraumaPluginID = "3693624471";

    public static ContentPackage GetPackageForAssembly(Assembly assembly)
    {
      if (assembly == typeof(GameMain).Assembly) return null;

      // Vanilla plugins
      foreach (var plugin in PluginLoader.LoadedPlugins)
      {
        if (plugin.Assembly == assembly) return plugin.Data.ContentPackage;
      }

      //TODO for some reason i can't check LuaCsForBarotraumaPlugins with reflection
      // when i try game just crashes

      return null;
    }

    public static ContentPackage Package => GetPackageForAssembly(Assembly.GetCallingAssembly());

    // idk, should this be available in other mods that uses the CUI?
    // GetPackageForAssembly is borked rn
    public static string HookId => GetPackageForAssembly(Assembly.GetCallingAssembly()).Name;
    public static string BarotraumaPath => Path.GetFullPath("./");


    public static string Dir => GetPackageForAssembly(Assembly.GetCallingAssembly()).Dir;
    public static string Version => GetPackageForAssembly(Assembly.GetCallingAssembly()).ModVersion;
    public static string Name => GetPackageForAssembly(Assembly.GetCallingAssembly()).Name;
  }
}