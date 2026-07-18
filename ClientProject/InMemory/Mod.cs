using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using Barotrauma.LuaCs;
using CUILibs;
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

    public CUIDebugger CUIDebugger { get; } = new();
    public MGDebugTool MGDebugTool { get; } = new();
    public CUITest CUITest { get; set; }

    public void Initialize()
    {
      Instance = this;
      if (ModStorage.Has("ReloadRequest")) { return; }

      Logger.Log($"Compiled somehow");

      try
      {
        CUI.Start();

        CUICore.TextureManager.Load("Assets/PNG/For testing/Test Chamber Background.png", "Test Chamber Background");
        CUICore.TextureManager.Load("Assets/PNG/For testing/Icons.png", "Test Icons");

        UTestCommands.AddCommands();
        CUITest = new CUITest();
        CUITest.Init();

        CUIDebugger.Init();
        MGDebugTool.Init();

        // Utils.PrintAllHarmonyPatches();
        // CUIPalette.Preview();

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
      CUIDebugger.Dispose();
      MGDebugTool.Dispose();
      CUITest.Dispose();
    }
  }
}