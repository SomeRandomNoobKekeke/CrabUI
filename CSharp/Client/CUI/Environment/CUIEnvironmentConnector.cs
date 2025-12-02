using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  //TODO i should probably make a version with harmony, i don't like all that casting
  public class CUIEnvironmentConnector
  {
    private string BeforeDrawHook => $"{ModInfo.HookId}_CUI_BeforeDraw";
    private string AfterDrawHook => $"{ModInfo.HookId}_CUI_AfterDraw";
    private MethodBase GUIDrawMethod => typeof(GUI).GetMethod("Draw");

    private CUISpriteBatch CUISpriteBatch = new CUISpriteBatch();

    private CUIEnvironment Environment;

    public bool IsConnected { get; private set; }

    public void Connect()
    {
      ConnectLifeCycle();
      IsConnected = true;
    }

    public void Disconnect()
    {
      GameMain.LuaCs.Hook.RemovePatch(BeforeDrawHook, GUIDrawMethod, LuaCsHook.HookMethodType.Before);
      GameMain.LuaCs.Hook.RemovePatch(AfterDrawHook, GUIDrawMethod, LuaCsHook.HookMethodType.After);

      IsConnected = false;
    }

    private void ConnectLifeCycle()
    {
      GameMain.LuaCs.Hook.Patch(BeforeDrawHook, GUIDrawMethod, (object instance, LuaCsHook.ParameterTable ptable) =>
      {
        CUISpriteBatch.Use((SpriteBatch)ptable["spriteBatch"]);
        Environment.LifeCycle.RaiseBeforeDraw(CUISpriteBatch);
        return null;
      }, LuaCsHook.HookMethodType.Before);

      GameMain.LuaCs.Hook.Patch(AfterDrawHook, GUIDrawMethod, (object instance, LuaCsHook.ParameterTable ptable) =>
      {
        CUISpriteBatch.Use((SpriteBatch)ptable["spriteBatch"]);
        Environment.LifeCycle.RaiseAfterDraw(CUISpriteBatch);
        return null;
      }, LuaCsHook.HookMethodType.After);
    }

    public CUIEnvironmentConnector(CUIEnvironment environment)
    {
      ArgumentNullException.ThrowIfNull(environment);
      Environment = environment;
    }
  }
}