using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using Microsoft.Xna.Framework;
using Barotrauma;

namespace CrabUI
{
  public static class CUI
  {
    static CUI()
    {
      PluginLifeCycle.Stop += Dispose;

      Setup = CUISetup.Default();
    }

    public static GUIButton DummyComponent = new GUIButton(new RectTransform(new Point(0, 0)))
    {
      Text = "DUMMY",
    };

    public static Logger Logger = new()
    {
      PrintFilePath = false,
    };

    public static Rectangle GameScreenRect => Core.GameScreenRect;


    private static CUISetup _Setup; public static CUISetup Setup
    {
      get
      {
        return _Setup;
      }
      set
      {
        _Setup = value;
        _Setup?.Activate();
      }
    }
    public static CUICore Core => Setup.Core;
    public static void Start() => Setup.Start();
    public static void Stop() => Setup?.Stop();

    public static CUIMainComponent Main => Setup.Core.Main;
    public static DebugHub DebugHub => Core.DebugHub;

    public static event Action<double> OnUpdate
    {
      add => Setup.Core.LifeCycle.OnUpdate.Add(value);
      remove => Setup.Core.LifeCycle.OnUpdate.Remove(value);
    }

    public static event Action<CUISpriteBatch> OnDrawAfterGUI
    {
      add => Setup.Core.LifeCycle.OnDrawAfterGUI.Add(value);
      remove => Setup.Core.LifeCycle.OnDrawAfterGUI.Remove(value);
    }

    public static event Action<CUISpriteBatch> OnDrawBeforeGUI
    {
      add => Setup.Core.LifeCycle.OnDrawBeforeGUI.Add(value);
      remove => Setup.Core.LifeCycle.OnDrawBeforeGUI.Remove(value);
    }

    public static void Dispose()
    {
      try
      {
        Setup?.Stop();
        Setup = null;
      }
      catch (Exception e)
      {
        CUI.Logger.Error($"Error in CUI Dispose: {e}");
      }
    }
  }
}