using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public partial class CUIRunner : IDisposable
  {
    private static MethodBase GUIDrawMethod => typeof(GUI).GetMethod("Draw");
    private static MethodBase UpdateMethod => typeof(GameMain).GetMethod("Update", AccessTools.all);

    private static string BeforeDrawHook => $"{ModInfo.HookId}_CUI_BeforeDraw";
    private static string AfterDrawHook => $"{ModInfo.HookId}_CUI_AfterDraw";
    private static string UpdateHook => $"{ModInfo.HookId}_CUI_Update";

    public CUICore CUICore { get; } = new();
    private CUISpriteBatch CUISpriteBatch = new CUISpriteBatch();

    public void Connect()
    {
      CUICore.InputProvider = new VanillaInputProvider();

      GameMain.LuaCs.Hook.Patch(BeforeDrawHook, GUIDrawMethod, (instance, ptable) =>
      {
        try
        {
          CUISpriteBatch.Use((SpriteBatch)ptable["spriteBatch"]);
          CUICore.LifeCycle.DrawBeforeGUI(CUISpriteBatch);
        }
        catch (Exception e)
        {
          CUI.Logger.Error(e);
        }

        return null;
      }, LuaCsHook.HookMethodType.Before);

      GameMain.LuaCs.Hook.Patch(AfterDrawHook, GUIDrawMethod, (instance, ptable) =>
      {
        try
        {
          CUISpriteBatch.Use((SpriteBatch)ptable["spriteBatch"]);
          CUICore.LifeCycle.DrawAfterGUI(CUISpriteBatch);
        }
        catch (Exception e)
        {
          CUI.Logger.Error(e);
        }

        return null;
      }, LuaCsHook.HookMethodType.After);

      GameMain.LuaCs.Hook.Patch(UpdateHook, UpdateMethod, (instance, ptable) =>
      {
        try
        {
          CUICore.LifeCycle.Update(Timing.TotalTime);
        }
        catch (Exception e)
        {
          CUI.Logger.Error(e);
        }

        return null;
      }, LuaCsHook.HookMethodType.After);
    }

    public void Disconnect()
    {
      GameMain.LuaCs.Hook.RemovePatch(BeforeDrawHook, GUIDrawMethod, LuaCsHook.HookMethodType.Before);
      GameMain.LuaCs.Hook.RemovePatch(AfterDrawHook, GUIDrawMethod, LuaCsHook.HookMethodType.After);
      GameMain.LuaCs.Hook.RemovePatch(UpdateHook, UpdateMethod, LuaCsHook.HookMethodType.After);
    }

    public CUIRunner()
    {
      Connect();
    }

    public void Dispose()
    {
      Disconnect();
    }
  }
}