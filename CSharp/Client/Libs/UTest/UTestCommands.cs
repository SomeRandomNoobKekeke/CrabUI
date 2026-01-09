using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
namespace BaroJunk
{
  public class UTestCommands
  {
    public static List<DebugConsole.Command> AddedCommands = new List<DebugConsole.Command>();
    //FIXME what if multiple mods have this command?
    //THINK Why this method is the main entry point?
    public static void AddCommands()
    {
      DebugConsole.Command utestCommand = new DebugConsole.Command("utest", "", UTest_Command, UTest_Hints);

#if CLIENT
      utestCommand.RelayToServer = false;
#endif

      AddedCommands.Add(utestCommand);
      DebugConsole.Commands.InsertRange(0, AddedCommands);

#if CLIENT
      PermitCommands();
#endif


#if CLIENT
      string lastCommand = ModStorage.Get<string>("lastUtestCommand");
      if (!string.IsNullOrEmpty(lastCommand))
      {
        DebugConsole.ExecuteCommand(lastCommand);
      }
#endif
    }
#if CLIENT
    private static void PermitCommands()
    {
      GameMain.LuaCs.Hook.Patch(
        "permit utest",
        typeof(DebugConsole).GetMethod("IsCommandPermitted", BindingFlags.NonPublic | BindingFlags.Static),
        (object instance, LuaCsHook.ParameterTable ptable) =>
        {
          if (AddedCommands.Any(command => ((Identifier)ptable["command"]).Value == command.Names[0]))
          {
            ptable.ReturnValue = true;
            ptable.PreventExecution = true;
          }

          return null;
        }
      );
    }
#endif
    //TODO remove hidden test from hints
    public static string[][] UTest_Hints()
      => new string[][] { UTestExplorer.TestNames.Append("all").Append("none").OrderBy(s => s.Length).ToArray() };

    public static int depth = 0;
    public static void UTest_Command(string[] args)
    {
      try
      {
#if CLIENT
        ModStorage.Set("lastUtestCommand", $"utest {(args.ElementAtOrDefault(0) ?? "none")}");
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
          UTestLogger.Warning($"Can't find [{args[0]}] test");
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