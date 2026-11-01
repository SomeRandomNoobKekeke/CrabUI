using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using HarmonyLib;
using Barotrauma.Networking;

namespace CUILibs
{
  public class UTestCommands
  {
    public static Harmony Harmony { get; } = new Harmony($"{ModInfo.HookId}.UTestCommands");


    public static List<DebugConsole.Command> AddedCommands = new List<DebugConsole.Command>();
    //FIXME what if multiple mods have this command?
    //THINK Why this method is the main entry point?
    public static void AddCommands()
    {
      if (DebugConsole.Commands.Any(c => c.Names[0].Value == "utest")) return;

      DebugConsole.Command utestCommand = new DebugConsole.Command("utest", "", UTest_Command, UTest_Hints);

#if CLIENT
      utestCommand.RelayToServer = false;
#endif

      AddedCommands.Add(utestCommand);
      DebugConsole.Commands.InsertRange(0, AddedCommands);

#if CLIENT
      Harmony.Patch(
        original: typeof(DebugConsole).GetMethod("IsCommandPermitted", BindingFlags.NonPublic | BindingFlags.Static),
        prefix: new HarmonyMethod(typeof(PluginCommands).GetMethod("PermitCommands", BindingFlags.NonPublic | BindingFlags.Static))
      );

      PluginLifeCycle.Stop += () => Harmony.UnpatchSelf();
#endif
    }


#if CLIENT
    private static bool PermitCommands(ref bool __result, Identifier command, GameClient client)
    {
      if (AddedCommands.Any(c => c.Names.Contains(command)))
      {
        __result = true;
        return false;
      }

      return true;
    }
#endif
    //TODO remove hidden test from hints
    public static string[][] UTest_Hints()
      => [UTestExplorer.TestNames.Append("all").Append("none").OrderBy(s => s.Length).ToArray()];

    public static int depth = 0;
    public static void UTest_Command(string[] args)
    {
      try
      {
#if CLIENT
        if (args.Length < 1 || args[0] == "none")
        {
          ModStorage.Remove("lastUtestCommand");
        }
        else
        {
          ModStorage.Set("lastUtestCommand", $"utest {args[0]}");
        }
#endif

        if (args.Length == 0)
        {
          UTestExplorer.PrintAllTests();
          return;
        }

        void RunTestPack(Type T)
        {
          List<UTestPack> results = UTestRunner.RunRecursive(T);

          foreach (UTestPack pack in results)
          {
            if (pack.NotEmpty) pack.Log();
          }
        }


        if (String.Equals(args[0], "none", StringComparison.OrdinalIgnoreCase))
        {
          return;
        }

        if (String.Equals(args[0], "all", StringComparison.OrdinalIgnoreCase))
        {
          foreach (Type T in UTestExplorer.TestTree.Roots.Select(root => root.Type))
          {
            RunTestPack(T);
          }
          return;
        }

        Type start = UTestExplorer.TestByName.GetValueOrDefault(args[0]);

        if (start is null)
        {
          if (UTestLogger.LogMissingTests)
          {
            UTestLogger.Warning($"Can't find [{args[0]}] test");
          }

          return;
        }

        RunTestPack(start);
      }
      catch (Exception e) { UTestLogger.Warning($"utest failed with: {e.Message}"); }
    }

    public static void RemoveCommands()
    {
      AddedCommands.ForEach(c => DebugConsole.Commands.Remove(c));
      AddedCommands.Clear();
    }
  }
}