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
  public partial class Mod : IAssemblyPlugin
  {
    public static Mod Instance;
    public static Logger Logger { get; set; } = new()
    {
      PrintFilePath = false,
    };

    public CUIDebugConsoleInterface CUIDebugConsoleInterface { get; } = new();
    public CUITestConsoleInterface CUITestConsoleInterface { get; } = new();
    // Dictionary<string, MethodInfo> factories = CUIFactories.AllFactoryMethods()
    //   .ToDictionary(mi => mi.Name, mi => mi);

    public void Initialize()
    {
      Instance = this;
      if (ModStorage.Has("ReloadRequest")) { return; }

      Logger.Log($"Compiled somehow");
      // UTestCommands.AddCommands();

      try
      {
        CUI.Start();
        CUIDebugConsoleInterface.Init();

        CUITestConsoleInterface.Add(CUITestFactories.AllFactoryMethods());
        CUITestConsoleInterface.Init();
        Experiment();
      }
      catch (Exception e) { Logger.Error(e); }
    }



    public void OnLoadCompleted() { }
    public void PreInitPatching() { }

    public void Dispose()
    {
      Instance = null;
      UTestCommands.RemoveCommands();
    }
  }
}