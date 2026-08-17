using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;
using Barotrauma;
using Barotrauma.LuaCs.Compatibility;
using Barotrauma.LuaCs;

namespace CrabUI
{
  public class GameLifeCycle : IGameLifeCycleTracker
  {
    public Harmony Harmony { get; } = new Harmony($"{ModInfo.HookId}.CUI.LifeCycle");

    private ClearableEvent<SpriteBatch> _BeforeGUIDraw = new();
    private ClearableEvent<SpriteBatch> _AfterGUIDraw = new();
    private ClearableEvent<GameTime> _Update = new();
    private ClearableEvent _SyncMouseOn = new();

    public event Action<SpriteBatch> BeforeGUIDraw
    {
      add => _BeforeGUIDraw.Add(value);
      remove => _BeforeGUIDraw.Remove(value);
    }
    public event Action<SpriteBatch> AfterGUIDraw
    {
      add => _AfterGUIDraw.Add(value);
      remove => _AfterGUIDraw.Remove(value);
    }
    public event Action<GameTime> Update
    {
      add => _Update.Add(value);
      remove => _Update.Remove(value);
    }
    public event Action SyncMouseOn
    {
      add => _SyncMouseOn.Add(value);
      remove => _SyncMouseOn.Remove(value);
    }


    public static GameLifeCycle Instance;

    public static void BeforeGUIDrawHandler(Camera cam, SpriteBatch spriteBatch)
    {
      Instance?._BeforeGUIDraw.Raise(spriteBatch);
    }

    public static void AfterGUIDrawHandler(SpriteBatch spriteBatch)
    {
      Instance?._AfterGUIDraw.Raise(spriteBatch);
    }

    public static void UpdateHandler(GameTime gameTime)
    {
      Instance?._Update.Raise(gameTime);
    }

    public static void SyncMouseOnHandler()
    {
      // It can also be called from UpdateGUIMessageBoxesOnly
      if (CallFrom_GUI_Update) Instance?._SyncMouseOn.Raise();
    }

    public void ConnectToGame()
    {
      Instance = this;

      Harmony.Patch(
        original: typeof(GUI).GetMethod("Draw"),
        prefix: new HarmonyMethod(typeof(GameLifeCycle).GetMethod("BeforeGUIDrawHandler"))
      );

      Harmony.Patch(
        original: typeof(GUI).GetMethod("DrawCursor", AccessTools.all),
        prefix: new HarmonyMethod(typeof(GameLifeCycle).GetMethod("AfterGUIDrawHandler"))
      );

      Harmony.Patch(
        original: typeof(GameMain).GetMethod("Update", AccessTools.all),
        prefix: new HarmonyMethod(typeof(GameLifeCycle).GetMethod("UpdateHandler"))
      );

      Harmony.Patch(
        original: typeof(GUI).GetMethod("Update", AccessTools.all),
        prefix: new HarmonyMethod(typeof(GameLifeCycle).GetMethod("GUI_Update_Prefix")),
        postfix: new HarmonyMethod(typeof(GameLifeCycle).GetMethod("GUI_Update_Postfix"))
      );

      Harmony.Patch(
        original: typeof(GUI).GetMethod("UpdateMouseOn", AccessTools.all),
        postfix: new HarmonyMethod(typeof(GameLifeCycle).GetMethod("SyncMouseOnHandler"))
      );
    }

    public static bool CallFrom_GUI_Update; //HACK should use transpiler to hook that call
    public static void GUI_Update_Prefix() => CallFrom_GUI_Update = true;
    public static void GUI_Update_Postfix() => CallFrom_GUI_Update = false;

    public void DisconnectFromGame()
    {
      Instance = null;
      Harmony.UnpatchSelf();
    }
  }
}