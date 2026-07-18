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

    public static ContentPackage Package => GetPackageForAssembly(Assembly.GetExecutingAssembly());

    // public static string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;
    public static string HookId => Package.Name;
    public static string BarotraumaPath => Path.GetFullPath("./");


    public static string Dir => Package.Dir;
    public static string Version => Package.ModVersion;
    public static string Name => Package.Name;
  }
}