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

      CUIButton button = new()
      {
        BackgroundColor = Color.Lime,
        Absolute = new CUINullRect(1400, 300, 200, 100),
      };

      button.MouseDown += (e) =>
      {
        button.BackgroundColor = button.BackgroundColor == Color.Red ? Color.Lime : Color.Red;
      };

      CUI.Main.AddChild(button);

      TextBlock tb = new TextBlock()
      {
        Text = "bruh",
        Rect = new CUIRect(300, 200, 0, 0),
      };

      CUI.OnDrawAfterGUI += (sb) =>
      {
        tb.Draw(sb);
      };
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