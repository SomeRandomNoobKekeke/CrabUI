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
      AddedCommands.Add(new DebugConsole.Command("cuidebug", "", (string[] args) =>
      {
        if (CUIDebugWindow.Main == null)
        {
          CUIDebugWindow.Open();
        }
        else
        {
          CUIDebugWindow.Close();
        }
      }));

      AddedCommands.Add(new DebugConsole.Command("printsprites", "", (string[] args) =>
      {
        foreach (GUIComponentStyle style in GUIStyle.ComponentStyles)
        {
          CUI.Log($"{style.Name} {style.Sprites.Count}");
        }
      }));

      AddedCommands.Add(new DebugConsole.Command("printkeys", "", (string[] args) =>
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
      }));

      DebugConsole.Commands.InsertRange(0, AddedCommands);
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