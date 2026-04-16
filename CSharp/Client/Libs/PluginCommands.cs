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

    public static string AssemblyName => Assembly.GetCallingAssembly().GetName().Name;

    public static List<DebugConsole.Command> AddedCommands = new List<DebugConsole.Command>();

    private static void AddHooks()
    {
      LuaCsSetup.Instance.Hook.Add("stop", $"[{AssemblyName}].RemoveCommands", (object[] args) =>
      {
        RemoveCommands();
        return null;
      });

      ((LuaCsSetup.Instance.EventService as EventService)
           ._luaPatcher as LuaPatcherService)
           .Patch($"{AssemblyName}.PermitCommands",
              typeof(DebugConsole).GetMethod("IsCommandPermitted", BindingFlags.NonPublic | BindingFlags.Static),
              (object instance, LuaPatcherService.ParameterTable ptable) =>
              {
                if (AddedCommands.Any(c => c.Names.Contains(((Identifier)ptable["command"]))))
                {
                  ptable.ReturnValue = true;
                  ptable.PreventExecution = true;
                }

                return null;
              }
        );


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
