using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;


namespace CrabUI
{
  public static class CUICommands
  {
    public static void Add()
    {
      PluginCommands.Add("cuiprinttree", CUIPrintTree_Command,
        () => new string[][] { new string[] { "Main", "TopMain" } }
      );
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
  }
}