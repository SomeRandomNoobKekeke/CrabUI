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

namespace CrabUIUser
{
  public class CUIDebugConsoleInterface
  {
    public void Init()
    {
      PluginCommands.Add("cuidebug", CUIDebug_Command, () => new string[][] { CUI.DebugHub.Gates.Names.ToArray() });
    }

    public static void CUIDebug_Command(string[] args)
    {
      if (args.Length == 0) return;

      string gate = String.Join(' ', args);
      CUI.DebugHub.Gates[gate].Toggle();
    }
  }
}