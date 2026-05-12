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
  public class CUITestConsoleInterface
  {
    public Dictionary<string, MethodInfo> TestFactories { get; } = new();

    public void Add(IEnumerable<MethodInfo> factories)
    {
      foreach (MethodInfo mi in factories)
      {
        TestFactories[mi.Name] = mi;
      }
    }

    public void Init()
    {
      PluginCommands.Add("cuitest", CUITest_Command, () => new string[][]{
        TestFactories.Keys.ToArray()
      });

      if (ModStorage.Has("CUITest"))
      {
        Run((string)ModStorage.Get("CUITest"));
      }
    }

    public void Run(string name)
    {
      if (!TestFactories.ContainsKey(name)) return;

      try
      {
        CUIComponent child = (CUIComponent)TestFactories[name].Invoke(null, new object[] { });

        CUI.Main.RemoveAllChildren();
        CUI.Main.AddChild(child);
      }
      catch (Exception e)
      {
        CUI.Logger.Warning($"Error in CUITest [{name}]: {e.Message}");
      }
    }

    public void CUITest_Command(string[] args)
    {
      if (args.Length == 0)
      {
        ModStorage.Remove("CUITest");
        return;
      }

      Run(args[0]);
      ModStorage.Set("CUITest", args[0]);
    }
  }
}