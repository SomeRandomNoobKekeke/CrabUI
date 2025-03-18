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

namespace CrabUI
{
  public partial class CUI
  {
    internal static List<DebugConsole.Command> AddedCommands = new List<DebugConsole.Command>();
    internal static void AddCommands()
    {
      AddedCommands.Add(new DebugConsole.Command("cuidebug", "", CUIDebug_Command));
      AddedCommands.Add(new DebugConsole.Command("cuicreatepalette", "cuicreatepalette name frontcolor [backcolor]", CUICreatePalette_Command));
      AddedCommands.Add(new DebugConsole.Command("cuimg", "", CUIMG_Command));
      AddedCommands.Add(new DebugConsole.Command("cuidraworder", "", CUIDrawOrder_Command));
      AddedCommands.Add(new DebugConsole.Command("cuiprinttree", "", CUIPrintTree_Command));
      AddedCommands.Add(new DebugConsole.Command("printsprites", "", PrintSprites_Command));
      AddedCommands.Add(new DebugConsole.Command("printkeys", "", PrintSprites_Command));
      AddedCommands.Add(new DebugConsole.Command("cuipalette", "load palette as primary", Palette_Command, () => new string[][] { CUIPalette.LoadedPalettes.Keys.ToArray() }));
      AddedCommands.Add(new DebugConsole.Command("cuipalettedemo", "", PaletteDemo_Command));
      AddedCommands.Add(new DebugConsole.Command("cuicreatepaletteset", "name primaty secondary tertiary quaternary", CUICreatePaletteSet_Command, () => new string[][] {
        new string[]{},
        CUIPalette.LoadedPalettes.Keys.ToArray(),
        CUIPalette.LoadedPalettes.Keys.ToArray(),
        CUIPalette.LoadedPalettes.Keys.ToArray(),
        CUIPalette.LoadedPalettes.Keys.ToArray(),
      }));
      AddedCommands.Add(new DebugConsole.Command("cuiloadpaletteset", "", CUILoadPaletteSet_Command));
      AddedCommands.Add(new DebugConsole.Command("cuicreateluatypesfile", "", CUICreateLuaTypesFile_Command));


      DebugConsole.Commands.InsertRange(0, AddedCommands);
    }

    public static void CUICreateLuaTypesFile_Command(string[] args)
    {
      CUI.LuaRegistrar.ConstructLuaStaticsFile();
    }

    public static void CUIDebug_Command(string[] args)
    {
      if (CUIDebugWindow.Main == null)
      {
        CUIDebugWindow.Open();
      }
      else
      {
        CUIDebugWindow.Close();
      }
    }

    public static void CUIDrawOrder_Command(string[] args)
    {
      foreach (CUIComponent c in CUI.Main.Flat)
      {
        CUI.Log(c);
      }
    }

    public static void CUIPrintTree_Command(string[] args)
    {
      CUI.Main?.PrintTree();
      CUI.TopMain?.PrintTree();
    }



    public static void CUICreatePalette_Command(string[] args)
    {
      string name = args.ElementAtOrDefault(0);
      Color colorA = CUIExtensions.ParseColor((args.ElementAtOrDefault(1) ?? "white"));
      Color colorB = CUIExtensions.ParseColor((args.ElementAtOrDefault(2) ?? "black"));
      CUIPalette palette = CUIPalette.CreatePaletteFromColors(name, colorA, colorB);
      CUIPalette.Primary = palette;
    }

    public static void CUICreatePaletteSet_Command(string[] args)
    {
      CUIPalette.SaveSet(
        args.ElementAtOrDefault(0),
        args.ElementAtOrDefault(1),
        args.ElementAtOrDefault(2),
        args.ElementAtOrDefault(3),
        args.ElementAtOrDefault(4)
      );
    }

    public static void CUILoadPaletteSet_Command(string[] args)
    {
      CUIPalette.LoadSet(Path.Combine(CUIPalette.PaletteSetsPath, args.ElementAtOrDefault(0)));
    }

    public static void CUIMG_Command(string[] args) => CUIMagnifyingGlass.ToggleEquip();

    public static void PrintSprites_Command(string[] args)
    {
      foreach (GUIComponentStyle style in GUIStyle.ComponentStyles)
      {
        CUI.Log($"{style.Name} {style.Sprites.Count}");
      }
    }

    public static void PrintKeysCommand(string[] args)
    {
      CUIDebug.PrintKeys = !CUIDebug.PrintKeys;

      if (CUIDebug.PrintKeys)
      {
        var values = typeof(Keys).GetEnumValues();
        foreach (var v in values)
        {
          Log($"{(int)v} {v}");
        }
        Log("---------------------------");
      }
    }

    public static void PaletteDemo_Command(string[] args)
    {
      try { CUIPalette.PaletteDemo(); } catch (Exception e) { CUI.Warning(e); }
    }

    public static void Palette_Command(string[] args)
    {
      try
      {
        CUIPalette palette = CUIPalette.LoadedPalettes?.GetValueOrDefault(args.ElementAtOrDefault(0) ?? "");
        if (palette != null) CUIPalette.Primary = palette;
      }
      catch (Exception e) { CUI.Warning(e); }
    }


    internal static void RemoveCommands()
    {
      AddedCommands.ForEach(c => DebugConsole.Commands.Remove(c));
      AddedCommands.Clear();
    }

    // public static void PermitCommands(Identifier command, ref bool __result)
    // {
    //   if (AddedCommands.Any(c => c.Names.Contains(command.Value))) __result = true;
    // }
  }
}