using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;
using CrabUI;

namespace SomeNamespace
{
  public partial class Mod : IAssemblyPlugin
  {
    public static string PackageName = "CrabUI";
    public static string ModDir;
    public static string AssetsPath => Path.Combine(ModDir, "Assets");

    public void Initialize()
    {
      FindModFolder();

      // This is necessary to find lua folder, if you want to use lua
      CUI.ModDir = ModDir;

      // Small optimization, if false CUI won't generate CUITypes.lua for you
      CUI.UseLua = false;

      // CUI can use Harmony patches or lua hooks
      // If true (default) CUI will use Harmony, but there might be mod conflicts with compiled mods, it will notify you if it happens
      // If false it will try to use lua hooks, but you'll need to set them up with provided lua script or a separate "in memory" plugin
      CUI.UseCursedPatches = true;

      CUI.AssetsPath = AssetsPath;

      CUI.Initialize();

      CUIFrame frame = new CUIFrame()
      {
        Relative = new CUINullRect(w: 0.2f, h: 0.2f),
        Anchor = CUIAnchor.Center,
      };

      frame["guh button"] = new CUIButton("Press me")
      {
        Anchor = CUIAnchor.Center,
        AddOnMouseDown = (e) => CUI.Log("Guh"),
      };

      CUI.Main.Append(frame);
    }

    public void OnLoadCompleted() { }
    public void PreInitPatching() { }

    public void Dispose()
    {
      CUI.Dispose();
    }

    public static void Log(object msg, Color? cl = null)
    {
      cl ??= Color.Cyan;
      LuaCsLogger.LogMessage($"{msg ?? "null"}", cl * 0.8f, cl);
    }

    public static void FindModFolder()
    {
      ContentPackage package = ContentPackageManager.EnabledPackages.All.ToList().Find(
        p => p.Name.Contains(PackageName)
      );

      if (package != null)
      {
        ModDir = Path.GetFullPath(package.Dir);
      }
      else
      {
        Log($"Couldn't find {PackageName} mod folder", Color.Orange);
      }
    }
  }
}