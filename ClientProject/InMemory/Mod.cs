using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using Barotrauma.LuaCs;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class Mod : IAssemblyPlugin
  {
    public static Mod Instance;
    public static Logger Logger { get; set; } = new()
    {
      PrintFilePath = false,
    };

    public CUITest CUITest { get; set; }


    public void Initialize()
    {
      Instance = this;
      if (ModStorage.Has("ReloadRequest")) { return; }

      Logger.Log($"Compiled somehow");

      try
      {
        CUI.Start();

        // CUI.TextureManager.LoadAs("Assets/PNG/For testing/Test Chamber Background.png", "Test Chamber Background");
        // CUI.TextureManager.LoadAs("Assets/PNG/For testing/Icons.png", "Test Icons");

        // UTest.CollapseTestPackIfSucceed = false;
        // UTest.Init();

        // // CUICore.Palettes.Primary = CUIPalette.FromColor(new Color(0, 0, 128));

        // CUITest = new CUITest();
        // CUITest.Init();

        // CUI.TopMain["debug button"] = new CUIButton("debug")
        // {
        //   Anchor = CUIAnchor.LeftCenter,
        //   OnMouseDown = (e) => CUICore.Debugger.Open(),
        // };

        // CUICore.Debugger.Open();
        // CUIPalette.Preview();
        // Utils.PrintAllHarmonyPatches();


        Experiment();
      }
      catch (Exception e) { Logger.Error(e); }
    }



    public void OnLoadCompleted() { }
    public void PreInitPatching() { }

    public void Dispose()
    {
      Instance = null;
      UTest.Dispose();
      // MGDebugTool?.Dispose();
      CUITest?.Dispose();

    }
  }
}