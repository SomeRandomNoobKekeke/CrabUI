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
    private static MethodBase GUI_DrawCursor_Method => typeof(GUI).GetMethod("DrawCursor", AccessTools.all);


    private static string BeforeDrawHook => $"{ModInfo.HookId}_CUI_BeforeDraw";
    private static string AfterDrawHook => $"{ModInfo.HookId}_CUI_AfterDraw";
    private static string UpdateHook => $"{ModInfo.HookId}_CUI_Update";


    public void Patch(
      string identifier,
      MethodBase method,
      LuaCsPatchFunc patch,
      LuaCsHook.HookMethodType hookType = LuaCsHook.HookMethodType.Before
    ) => ((LuaCsSetup.Instance.EventService as EventService)
           ._luaPatcher as LuaPatcherService)
           .Patch(identifier, method, patch, hookType);

    public void ConnectToGame()
    {
      Patch(BeforeDrawHook, GUIDrawMethod, (instance, ptable) =>
      {
        _BeforeGUIDraw.Raise((SpriteBatch)ptable["spriteBatch"]);
        return null;
      }, LuaCsHook.HookMethodType.Before);

      Patch(AfterDrawHook, GUI_DrawCursor_Method, (instance, ptable) =>
      {
        _AfterGUIDraw.Raise((SpriteBatch)ptable["spriteBatch"]);
        return null;
      }, LuaCsHook.HookMethodType.Before);

      Patch(UpdateHook, UpdateMethod, (instance, ptable) =>
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