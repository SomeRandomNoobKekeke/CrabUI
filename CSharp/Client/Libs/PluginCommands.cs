using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using System.IO;
using System.Text;

namespace BaroJunk
{

  public static class PluginCommands
  {
    static PluginCommands()
    {
      AddHooks();
    }

    public static List<DebugConsole.Command> AddedCommands = new List<DebugConsole.Command>();

    private static void AddHooks()
    {
      GameMain.LuaCs.Hook.Add("stop", $"[{ModInfo.AssemblyName}].RemoveCommands", (object[] args) =>
      {
        RemoveCommands();
        return null;
      });

      GameMain.LuaCs.Hook.Patch(
        $"{ModInfo.AssemblyName}.PermitCommands",
        typeof(DebugConsole).GetMethod("IsCommandPermitted", BindingFlags.NonPublic | BindingFlags.Static),
        (object instance, LuaCsHook.ParameterTable ptable) =>
        {
          if (AddedCommands.Any(c => c.Names.Contains(((Identifier)ptable["command"]).Value)))
          {
            ptable.ReturnValue = true;
            ptable.PreventExecution = true;
          }

          return null;
        }
      );
    }

    public static void Add(string name, Action<object[]> callback, Func<string[][]> hints = null, string help = "", bool addToStart = true)
    {
      DebugConsole.Command command = new DebugConsole.Command(name, help, callback, hints);
      AddedCommands.Add(command);

      if (addToStart)
      {
        DebugConsole.Commands.Insert(0, command);
      }
      else
      {
        DebugConsole.Commands.Add(command);
      }
    }

    public static void PrintCommands()
    {
      foreach (DebugConsole.Command command in DebugConsole.Commands)
      {
        Logger.Default.Log(command.Names[0]);
      }
    }

    public static void PrintHooks()
    {
      Logger.Default.Log(Logger.Wrap.IDictionary(
        GameMain.LuaCs.Hook.hookFunctions.ToDictionary(
          kvp => kvp.Key,
          kvp => Logger.Wrap.IEnumerable(kvp.Value.Keys)
        )
      ));
    }

    public static void RemoveCommands()
    {
      AddedCommands.ForEach(c => DebugConsole.Commands.Remove(c));
      AddedCommands.Clear();
    }
  }
}
