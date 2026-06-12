using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

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
        postfix: new HarmonyMethod(typeof(GameLifeCycle).GetMethod("UpdateHandler"))
      );
    }

    public void DisconnectFromGame()
    {
      Instance = null;
      Harmony.UnpatchSelf();
    }
  }
}