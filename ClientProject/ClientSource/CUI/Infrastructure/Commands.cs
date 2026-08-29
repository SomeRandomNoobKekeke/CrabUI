using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;
using HarmonyLib;


namespace CursedUI
{
  public static class CUICommands
  {
    public static void Add()
    {
      PluginCommands.Add("cuiprinttree", CUIPrintTree_Command,
        () => new string[][] { new string[] { "Main", "TopMain" } }
      );

      PluginCommands.Add("cuipalettepreview", CUIPalettePreview_Command);

      PluginCommands.Add("cuidebug", CUIDebug_Command);
      PluginCommands.Add("printharmonypatches", PrintHarmonyPatches_Command, () => new string[][]{
        new string[]{ "nolua" },
        new string[]{ "deep" },
      });

      PluginCommands.Add("printcuitextures", PrintCUITextures_Command, PrintCUITextures_Hints);
      PluginCommands.Add("gc", GC_Command);
    }

    public static void GC_Command(string[] args)
    {
      GC.Collect();
      CUI.Logger.Print($"Process.PrivateMemorySize64: {LuaCsPerformanceCounter.MemoryUsage}MB", Color.Lime);
    }

    public static string[][] PrintCUITextures_Hints()
    {
      return new string[][]
      {
        (CUICore.TextureManager as __CUITextureManager).LoadedTextures.Keys.ToArray()
      };
    }
    public static void PrintCUITextures_Command(string[] args)
    {
      void PrintTexture(string key, CUITexture2D texture)
      {
        try
        {
          int size = texture.Width * texture.Height * 4;
          CUI.Logger.Log($"{(texture is CUIRenderTarget2D ? "RT " : "")}[{texture.Width}x{texture.Height}] {key} - {size / 1000}KB");
        }
        catch (Exception e)
        {
          CUI.Logger.Warning($"{key} - Disposed");
        }
      }

      __CUITextureManager textureManager = (CUICore.TextureManager as __CUITextureManager);

      if (args.Length == 0)
      {
        foreach (var (key, texture) in textureManager.LoadedTextures)
        {
          PrintTexture(key, texture);
        }
        return;
      }

      textureManager.PrintTrace(args[0]);
    }

    public static void CUIDebug_Command(string[] args)
    {
      CUICore.Debugger.Open();
    }

    public static void CUIPrintTree_Command(string[] args)
    {
      if (args.Length == 0 || args[0] == "Main")
      {
        CUI.Main.PrintTree();
      }
      else
      {
        CUI.TopMain.PrintTree();
      }
    }

    public static void CUIPalettePreview_Command(string[] args)
    {
      CUIPalette.Preview();
    }

    public static void PrintHarmonyPatches_Command(string[] args)
    {
      bool nolua = args.ElementAtOrDefault(0) == "nolua";
      bool deep = args.ElementAtOrDefault(1) == "deep";

      foreach (MethodBase mb in Harmony.GetAllPatchedMethods())
      {
        Func<Patch, bool> selector = nolua ? (p) => !p.owner.Contains("LuaCsForBarotrauma") : (p) => true;
        Func<Patch, string> toText = p => $"{p.owner}{(deep ? $" - {p.PatchMethod.GetFullMethodName()}" : "")}";

        bool PatchIsEmpty(Patches p) =>
          p.Prefixes.Count(selector) == 0 &&
          p.Postfixes.Count(selector) == 0 &&
          p.Transpilers.Count(selector) == 0 &&
          p.Finalizers.Count(selector) == 0 &&
          p.ILManipulators.Count(selector) == 0;

        Patches patches = Harmony.GetPatchInfo(mb);

        if (PatchIsEmpty(patches)) continue;

        CUI.Logger.Log($"========>>>  {mb}  <<<========");


        if (patches.Prefixes.Count(selector) > 0)
        {
          CUI.Logger.Log($"Prefixes - {Logger.Wrap.IEnumerable(patches.Prefixes.Where(selector).Select(toText), deep)}");
        }

        if (patches.Postfixes.Count(selector) > 0)
        {
          CUI.Logger.Log($"Postfixes - {Logger.Wrap.IEnumerable(patches.Postfixes.Where(selector).Select(toText), deep)}");
        }

        if (patches.Finalizers.Count(selector) > 0)
        {
          CUI.Logger.Log($"Finalizers - {Logger.Wrap.IEnumerable(patches.Finalizers.Where(selector).Select(toText), deep)}");
        }

        if (patches.Transpilers.Count(selector) > 0)
        {
          CUI.Logger.Log($"Transpilers - {Logger.Wrap.IEnumerable(patches.Transpilers.Where(selector).Select(toText), deep)}");
        }

        if (patches.ILManipulators.Count(selector) > 0)
        {
          CUI.Logger.Log($"ILManipulators - {Logger.Wrap.IEnumerable(patches.ILManipulators.Where(selector).Select(toText), deep)}");
        }
      }
    }
  }
}