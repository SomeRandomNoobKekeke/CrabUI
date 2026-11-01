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
using HarmonyLib;
using Barotrauma.Networking;

namespace CUILibs
{

  public static class PluginCommands
  {
    static PluginCommands()
    {
      AddHooks();
    }

    public static Harmony Harmony { get; } = new Harmony($"{ModInfo.HookId}.PluginCommands");

    public static List<DebugConsole.Command> AddedCommands = new List<DebugConsole.Command>();

    private static bool PermitCommands(ref bool __result, Identifier command, GameClient client)
    {
      if (AddedCommands.Any(c => c.Names.Contains(command)))
      {
        __result = true;
        return false;
      }

      return true;
    }

    private static void AddHooks()
    {
      Harmony.Patch(
        original: typeof(DebugConsole).GetMethod("IsCommandPermitted", BindingFlags.NonPublic | BindingFlags.Static),
        prefix: new HarmonyMethod(typeof(PluginCommands).GetMethod("PermitCommands", BindingFlags.NonPublic | BindingFlags.Static))
      );

      PluginLifeCycle.Stop += () => Harmony.UnpatchSelf();
    }

    public static void Add(string name, Action<string[]> callback, Func<string[][]> hints = null, string help = "", bool addToStart = true)
    {
      DebugConsole.Command command = new DebugConsole.Command(name, help, (string[] args) =>
      {
        try
        {
          callback(args);
        }
        catch (Exception e)
        {
          Logger.Default.Error(e.Message);
        }
      }, hints);
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

    public static bool Exist(string name)
      => DebugConsole.Commands.Any(command => command.Names[0].Value == name);

    public static void PrintCommands()
    {
      foreach (DebugConsole.Command command in DebugConsole.Commands)
      {
        Logger.Default.Log(command.Names[0]);
      }
    }

    public static void RemoveCommands()
    {
      AddedCommands.ForEach(c => DebugConsole.Commands.Remove(c));
      AddedCommands.Clear();
    }
  }
}
