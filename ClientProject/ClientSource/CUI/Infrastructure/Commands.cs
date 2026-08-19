using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;
using HarmonyLib;


namespace CrabUI
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
        Func<Patch, string> toText = p => $"{p.owner}{(deep ? $" - {Utils.GetFullMethodName(p.PatchMethod)}" : "")}";

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