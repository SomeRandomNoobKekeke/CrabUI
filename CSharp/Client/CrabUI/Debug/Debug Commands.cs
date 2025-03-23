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
    internal static void AddDebugCommands()
    {
      AddedCommands.Add(new DebugConsole.Command("cuidebug", "", CUIDebug_Command));
      AddedCommands.Add(new DebugConsole.Command("cuimg", "", CUIMG_Command));
      AddedCommands.Add(new DebugConsole.Command("cuidraworder", "", CUIDrawOrder_Command));
      AddedCommands.Add(new DebugConsole.Command("cuiprinttree", "", CUIPrintTree_Command));
      AddedCommands.Add(new DebugConsole.Command("printsprites", "", PrintSprites_Command));
      AddedCommands.Add(new DebugConsole.Command("cuidebugprops", "", CUIDebugProps_Command));

      DebugConsole.Commands.InsertRange(0, AddedCommands);
    }

    public static void CUIDebug_Command(string[] args)
    {
      if (CUIDebugWindow.Main == null) CUIDebugWindow.Open();
      else CUIDebugWindow.Close();
    }

    public static void CUIDebugProps_Command(string[] args)
    {
      if (CUIPropsDebug.Opened) CUI.Log($"Already opened");
      else CUIPropsDebug.Open();
    }
    public static void CUIMG_Command(string[] args) => CUIMagnifyingGlass.ToggleEquip();

    public static void CUIDrawOrder_Command(string[] args)
    {
      foreach (CUIComponent c in CUI.Main.Flat) { CUI.Log(c); }
    }

    public static void CUIPrintTree_Command(string[] args)
    {
      CUI.Main?.PrintTree();
      CUI.TopMain?.PrintTree();
    }

    public static void PrintSprites_Command(string[] args)
    {
      foreach (GUIComponentStyle style in GUIStyle.ComponentStyles)
      {
        CUI.Log($"{style.Name} {style.Sprites.Count}");
      }
    }
  }
}