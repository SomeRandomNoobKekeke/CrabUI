using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;
using BaroJunk;

namespace CrabUI
{
  public class DefaultLifeCycleAdapter
  {
    private string BeforeDrawHook => $"{ModInfo.HookId}_CUI_BeforeDraw";
    private string AfterDrawHook => $"{ModInfo.HookId}_CUI_AfterDraw";
    private string UpdateHook => $"{ModInfo.HookId}_CUI_Update";
    private MethodBase GUIDrawMethod => typeof(GUI).GetMethod("Draw");
    private MethodBase UpdateMethod => typeof(GameMain).GetMethod("Update", AccessTools.all);

    private CUISpriteBatch CUISpriteBatch = new CUISpriteBatch();

    public void Connect(CUIEnvironment Environment)
    {
      GameMain.LuaCs.Hook.Patch(BeforeDrawHook, GUIDrawMethod, (object instance, LuaCsHook.ParameterTable ptable) =>
      {
        CUISpriteBatch.Use((SpriteBatch)ptable["spriteBatch"]);
        Environment.LifeCycle.RaiseDrawBeforeGUI(CUISpriteBatch);
        return null;
      }, LuaCsHook.HookMethodType.Before);

      GameMain.LuaCs.Hook.Patch(AfterDrawHook, GUIDrawMethod, (object instance, LuaCsHook.ParameterTable ptable) =>
      {
        CUISpriteBatch.Use((SpriteBatch)ptable["spriteBatch"]);
        Environment.LifeCycle.RaiseDrawAfterGUI(CUISpriteBatch);
        return null;
      }, LuaCsHook.HookMethodType.After);

      GameMain.LuaCs.Hook.Patch(UpdateHook, UpdateMethod, (object instance, LuaCsHook.ParameterTable ptable) =>
      {
        Environment.LifeCycle.RaiseUpdate();
        return null;
      }, LuaCsHook.HookMethodType.After);
    }

    public void Disconnect()
    {
      GameMain.LuaCs.Hook.RemovePatch(BeforeDrawHook, GUIDrawMethod, LuaCsHook.HookMethodType.Before);
      GameMain.LuaCs.Hook.RemovePatch(AfterDrawHook, GUIDrawMethod, LuaCsHook.HookMethodType.After);
      GameMain.LuaCs.Hook.RemovePatch(UpdateHook, UpdateMethod, LuaCsHook.HookMethodType.After);
    }
  }
}