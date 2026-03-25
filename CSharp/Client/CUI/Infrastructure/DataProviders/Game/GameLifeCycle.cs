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

namespace CrabUI
{
  public class GameLifeCycle : IGameLifeCycleTracker
  {
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


    private static MethodBase GUIDrawMethod => typeof(GUI).GetMethod("Draw");
    private static MethodBase UpdateMethod => typeof(GameMain).GetMethod("Update", AccessTools.all);

    private static string BeforeDrawHook => $"{ModInfo.HookId}_CUI_BeforeDraw";
    private static string AfterDrawHook => $"{ModInfo.HookId}_CUI_AfterDraw";
    private static string UpdateHook => $"{ModInfo.HookId}_CUI_Update";
    public void ConnectToGame()
    {
      GameMain.LuaCs.Hook.Patch(BeforeDrawHook, GUIDrawMethod, (instance, ptable) =>
      {
        _BeforeGUIDraw.Raise((SpriteBatch)ptable["spriteBatch"]);
        return null;
      }, LuaCsHook.HookMethodType.Before);

      GameMain.LuaCs.Hook.Patch(AfterDrawHook, GUIDrawMethod, (instance, ptable) =>
      {
        _AfterGUIDraw.Raise((SpriteBatch)ptable["spriteBatch"]);
        return null;
      }, LuaCsHook.HookMethodType.After);

      GameMain.LuaCs.Hook.Patch(UpdateHook, UpdateMethod, (instance, ptable) =>
      {
        _Update.Raise((GameTime)ptable["gameTime"]);
        return null;
      }, LuaCsHook.HookMethodType.After);
    }

    public void UnsubEvents()
    {
      _BeforeGUIDraw.Clear();
      _AfterGUIDraw.Clear();
      _Update.Clear();
    }
  }
}