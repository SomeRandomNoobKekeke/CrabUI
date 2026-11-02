using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;

using Barotrauma.LuaCs;
using CursedUI;
using System.IO;

namespace CUILibs
{
  /// <summary>
  /// Static class with some info about package
  /// Generally a wrapper around this magnificence
  /// public bool TryGetPackageForPlugin<T>(out ContentPackage package) where T : IAssemblyPlugin
  /// </summary>
  internal static class ModInfo
  {
    //https://steamcommunity.com/sharedfiles/filedetails/?id=3693624471
    public static string LuaCsForBarotraumaPluginID = "3693624471";
    public static string BarotraumaPath => Path.GetFullPath("./");

    public static ContentPackage GetPackageForAssembly(Assembly assembly)
    {
      if (assembly == typeof(GameMain).Assembly) return null;

      ContentPackage? package = null;

      package = TryFindPackageInVanillaPlugins(assembly);
      if (package != null) return package;

      package = TryFindPackageInLuaPlugin(assembly);
      if (package != null) return package;

      return null;
    }

    public static ContentPackage? TryFindPackageInVanillaPlugins(Assembly assembly)
    {
      foreach (var plugin in Barotrauma.Plugins.PluginLoader.LoadedPlugins)
      {
        if (plugin.Assembly == assembly) return plugin.Data.ContentPackage;
      }

      return null;
    }

    /// <summary>
    /// in old luatrauma simple TryFindPackageInVanillaPlugins wouldn't compile
    /// </summary>
    public static ContentPackage? TryFindPackageInVanillaPluginsWithReflection(Assembly assembly)
    {
      Type TPluginLoader = typeof(GameMain).Assembly.GetType("Barotrauma.Plugins.PluginLoader")!;

      if (TPluginLoader is null) return null;

      IList loadedPlugins = (IList)TPluginLoader.GetProperty("LoadedPlugins")!.GetValue(null)!;

      if (loadedPlugins.Count == 0) return null;

      Type TLoadedPlugin = loadedPlugins[0]!.GetType();

      PropertyInfo LoadedPlugin_Assembly = TLoadedPlugin.GetProperty("Assembly")!;
      PropertyInfo LoadedPlugin_Data = TLoadedPlugin.GetProperty("Data")!;
      FieldInfo LoadedPlugin_Data_ContentPackage = LoadedPlugin_Data.PropertyType.GetField("ContentPackage")!;

      foreach (object plugin in loadedPlugins)
      {
        if ((Assembly)LoadedPlugin_Assembly.GetValue(plugin)! == assembly)
        {
          return (ContentPackage)LoadedPlugin_Data_ContentPackage.GetValue(
            LoadedPlugin_Data.GetValue(plugin)
          )!;
        }
      }

      return null;
    }

    public static ContentPackage? TryFindPackageInLuaPlugin(Assembly assembly)
    {
      PluginManagementService pluginManagement = LuaCsSetup.Instance.PluginManagementService as PluginManagementService;

      foreach (var (package, asmLoader) in pluginManagement._assemblyLoaders)
      {
        if (asmLoader.Assemblies.Any(asm => asm == assembly))
        {
          return package;
        }
      }

      return null;
    }

    public static ContentPackage? TryFindPackageInLuaPluginWithReflection(Assembly assembly)
    {
      Assembly? luaPluginAssembly = null;

      // FirstOrDefault on record struct moment
      foreach (var plugin in Barotrauma.Plugins.PluginLoader.LoadedPlugins)
      {
        if (plugin.Data.ContentPackage.UgcId.ToString() == LuaCsForBarotraumaPluginID)
        {
          luaPluginAssembly = plugin.Assembly;
          break;
        }
      }

      if (luaPluginAssembly is null) return null;


      // Although luaPlugin is in "LoadedPlugins" apparently it doesn't mean that lua is loaded
      // and if you try to access Instance it'll trigger 2nd lua initialization which explodes the game
      object LuaCsSetup_Instance = luaPluginAssembly.GetType("Barotrauma.LuaCsSetup")!
        .GetField("_luaCsSetup", BindingFlags.NonPublic | BindingFlags.Static)!
        .GetValue(null)!;

      Logger.Default.LogVars(LuaCsSetup_Instance);

      if (LuaCsSetup_Instance is null) return null;

      byte runState = (byte)LuaCsSetup_Instance.GetType().GetProperty("CurrentRunState")!.GetValue(LuaCsSetup_Instance)!;

      Logger.Default.LogVars(runState);

      if (runState != 2) return null; // RunState.Running


      object pluginManagement = LuaCsSetup_Instance.GetType()
        .GetProperty("PluginManagementService")!
        .GetValue(LuaCsSetup_Instance)!;

      IDictionary _assemblyLoaders = (IDictionary)pluginManagement.GetType()
        .GetField("_assemblyLoaders", BindingFlags.Instance | BindingFlags.NonPublic)!
        .GetValue(pluginManagement)!;

      foreach (object package in _assemblyLoaders.Keys)
      {
        object asmLoader = _assemblyLoaders[package]!;

        IEnumerable<Assembly> assemblies = (IEnumerable<Assembly>)asmLoader.GetType()
          .GetProperty("Assemblies")!
          .GetValue(asmLoader)!;

        if (assemblies.Any(asm => asm == assembly))
        {
          return (ContentPackage)package;
        }
      }

      return null;
    }


    public static ContentPackage Package => GetPackageForAssembly(Assembly.GetCallingAssembly());

    public static string HookId => Package.Name;
    public static string Dir => Package.Dir;
    public static string Version => Package.ModVersion;
    public static string Name => Package.Name;
  }
}