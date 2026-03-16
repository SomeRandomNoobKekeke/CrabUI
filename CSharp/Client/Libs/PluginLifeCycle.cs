using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using System.IO;
using System.Text;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaroJunk
{
  /// <summary>
  /// i'm not drying it now, mb later
  /// </summary>
  public class PluginLifeCycle
  {
    /// <summary>
    /// Events can't be static because then there will be no way to clear callbacks
    /// So they must be mapped to some throwaway wrapper
    /// </summary>
    private static PluginLifeCycle Instance;

    public static MethodBase GUIDrawMethod => typeof(GUI).GetMethod("Draw");
    public static MethodBase UpdateMethod => typeof(GameMain).GetMethod("Update", AccessTools.all);
    static PluginLifeCycle()
    {
      Instance = new PluginLifeCycle();
      InstallClearHook();
    }

    private static void Clear()
    {
      Instance = null;
    }
    public static void InstallClearHook()
    {
      GameMain.LuaCs.Hook.Add("stop", $"[{ModInfo.AssemblyName}] PluginLifeCycle Clear", (object[] args) =>
      {
        Clear(); return null;
      });
    }

    private static bool StopHookInstalled = false;
    public static void TryInstallStopHook()
    {
      if (StopHookInstalled) return; StopHookInstalled = true;
      GameMain.LuaCs.Hook.Add("stop", $"[{ModInfo.AssemblyName}] PluginLifeCycle Clear", (object[] args) =>
      {
        Clear(); return null;
      });
    }
    private event Action _stop; public static event Action Stop
    {
      add
      {
        if (Instance is null) return;
        Instance._stop += value;
        TryInstallStopHook();
      }
      remove
      {
        if (Instance is null) return;
        Instance._stop -= value;
      }
    }


    private static bool BeforeGUIDrawHookInstalled = false;
    public static void TryInstallBeforeGUIDrawHook()
    {
      if (BeforeGUIDrawHookInstalled) return; BeforeGUIDrawHookInstalled = true;
      GameMain.LuaCs.Hook.Patch($"123", GUIDrawMethod, (instance, ptable) =>
      {
        Instance?._beforeGUIDraw?.Invoke((SpriteBatch)ptable["spriteBatch"]);
        return null;
      }, LuaCsHook.HookMethodType.Before);
    }
    private event Action<SpriteBatch> _beforeGUIDraw; public static event Action<SpriteBatch> BeforeGUIDraw
    {
      add
      {
        if (Instance is null) return;
        Instance._beforeGUIDraw += value;
        TryInstallBeforeGUIDrawHook();

      }
      remove
      {
        if (Instance is null) return;
        Instance._beforeGUIDraw -= value;
      }
    }


    private static bool AfterGUIDrawHookInstalled = false;
    public static void TryInstallAfterGUIDrawHook()
    {
      if (AfterGUIDrawHookInstalled) return; AfterGUIDrawHookInstalled = true;
      GameMain.LuaCs.Hook.Patch($"321", GUIDrawMethod, (instance, ptable) =>
      {
        Instance?._afterGUIDraw?.Invoke((SpriteBatch)ptable["spriteBatch"]);
        return null;
      }, LuaCsHook.HookMethodType.After);
    }
    private event Action<SpriteBatch> _afterGUIDraw; public static event Action<SpriteBatch> AfterGUIDraw
    {
      add
      {
        if (Instance is null) return;
        Instance._afterGUIDraw += value;
        TryInstallAfterGUIDrawHook();
      }
      remove
      {
        if (Instance is null) return;
        Instance._afterGUIDraw -= value;
      }
    }

    private static bool AfterUpdateHookInstalled = false;
    public static void TryInstallAfterUpdateHook()
    {
      if (AfterUpdateHookInstalled) return; AfterUpdateHookInstalled = true;
      GameMain.LuaCs.Hook.Patch($"[{ModInfo.AssemblyName}] PluginLifeCycle.AfterUpdate", UpdateMethod, (instance, ptable) =>
      {
        Instance?._afterUpdate?.Invoke();
        return null;
      }, LuaCsHook.HookMethodType.After);
    }
    private event Action _afterUpdate; public static event Action AfterUpdate
    {
      add
      {
        if (Instance is null) return;
        Instance._afterUpdate += value;
        TryInstallAfterUpdateHook();
      }
      remove
      {
        if (Instance is null) return;
        Instance._afterUpdate -= value;
      }
    }

  }
}
