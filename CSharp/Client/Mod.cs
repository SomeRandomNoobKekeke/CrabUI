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

    public void Initialize()
    {
      Instance = this;
      if (ModStorage.Has("ReloadRequest"))
      {
        Logger.Log($"Reload requested, exiting");
        return;
      }

      Logger.Log($"Compiled somehow");
      UTestCommands.AddCommands();

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

      CUIComponent component = new CUIComponent()
      {
        BackgroundColor = Color.Lime,
        Absolute = new CUINullRect(1400, 300, 200, 100),
      };

      component.MouseDown += (e) =>
      {
        component.BackgroundColor = component.BackgroundColor == Color.Red ? Color.Lime : Color.Red;
      };

      CUI.Main.AddChild(component);
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