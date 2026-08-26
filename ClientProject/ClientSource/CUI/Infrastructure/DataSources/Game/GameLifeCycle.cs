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

namespace CursedUI
{
  public class GameLifeCycle
  {
    public static GameLifeCycle Instance;
    public Harmony Harmony { get; } = new Harmony($"{ModInfo.HookId}.CUI.LifeCycle");

    public event Action<SpriteBatch> BeforeGUIDraw;
    public event Action<SpriteBatch> AfterGUIDraw;
    public event Action<GameTime> Update;



    public static void BeforeGUIDrawHandler(Camera cam, SpriteBatch spriteBatch)
    {
      Instance?.BeforeGUIDraw?.Invoke(spriteBatch);
    }

    public static void AfterGUIDrawHandler(SpriteBatch spriteBatch)
    {
      Instance?.AfterGUIDraw?.Invoke(spriteBatch);
    }

    public static void UpdateHandler(GameTime gameTime)
    {
      Instance?.Update?.Invoke(gameTime);
    }

    public void ConnectToGame()
    {
      Instance = this;

      Harmony.Patch(
        original: typeof(GUI).GetMethod("Draw"),
        prefix: new HarmonyMethod(GetType().GetMethod("BeforeGUIDrawHandler"))
      );

      Harmony.Patch(
        original: typeof(GUI).GetMethod("DrawCursor", AccessTools.all),
        prefix: new HarmonyMethod(GetType().GetMethod("AfterGUIDrawHandler"))
      );

      Harmony.Patch(
        original: typeof(GameMain).GetMethod("Update", AccessTools.all),
        prefix: new HarmonyMethod(GetType().GetMethod("UpdateHandler"))
      );
    }

    public void DisconnectFromGame()
    {
      Instance = null;
      Harmony.UnpatchSelf();
    }
  }
}