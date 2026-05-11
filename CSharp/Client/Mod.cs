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
    public CUITest CUITest { get; } = new();

    public static Logger Logger { get; set; } = new()
    {
      PrintFilePath = false,
    };

    public void Initialize()
    {
      Instance = this;
      if (ModStorage.Has("ReloadRequest")) { return; }

      Logger.Log($"Compiled somehow");
      // UTestCommands.AddCommands();

      try
      {
        Init();
        Experiment();
      }
      catch (Exception e) { Logger.Error(e); }
    }

    public void Init()
    {
      CUI.Start();

      CUIComponent frame = new()
      {
        BackgroundColor = Color.Gray,
        Draggable = true,
        Absolute = new CUINullRect(300, 300, 400, 600),
      };

      frame.AddChild(new CUIComponent()
      {
        BackgroundColor = Color.Yellow,
        Absolute = new CUINullRect(0, 0, 100, 100),
      });

      CUI.Main.AddChild(frame);
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