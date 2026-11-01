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
using Barotrauma.LuaCs;

namespace CUILibs
{

  /// <summary>
  /// Static class with some info about package
  /// Generally a wrapper around this magnificence
  /// public bool TryGetPackageForPlugin<T>(out ContentPackage package) where T : IAssemblyPlugin
  /// </summary>
  public static class ModInfo
  {
    public static ContentPackage GetPackageForAssembly(Assembly assembly)
    {
      if (assembly == typeof(GameMain).Assembly) return null;

      PluginManagementService pluginManagement = LuaCsSetup.Instance.PluginManagementService as PluginManagementService;

      foreach (var (package, asmLoader) in pluginManagement._assemblyLoaders)
      {
        if (asmLoader.Assemblies.Any(asm => asm == assembly))
        {
          return package;
        }
      }

      throw new UnreachableException();
    }

    public static ContentPackage Package => GetPackageForAssembly(Assembly.GetCallingAssembly());

    public static string HookId => GetPackageForAssembly(Assembly.GetCallingAssembly()).Name;
    public static string BarotraumaPath => Path.GetFullPath("./");


    public static string Dir => GetPackageForAssembly(Assembly.GetCallingAssembly()).Dir;
    public static string Version => GetPackageForAssembly(Assembly.GetCallingAssembly()).ModVersion;
    public static string Name => GetPackageForAssembly(Assembly.GetCallingAssembly()).Name;
  }
}