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
    public class Hook
    {
      public bool IsInstalled { get; private set; }
      public Action InstallAction { get; set; }
      public Action RemoveAction { get; set; }

      public void TryInstall()
      {
        if (IsInstalled) return;
        InstallAction?.Invoke();
      }

      public void TryRemove()
      {
        if (!IsInstalled) return;
        RemoveAction?.Invoke();
      }
    }

    /// <summary>
    /// Events can't be static because then there will be no way to clear callbacks
    /// So they must be mapped to some throwaway wrapper
    /// </summary>
    private static PluginLifeCycle Instance;

    private static MethodBase GUIDrawMethod => typeof(GUI).GetMethod("Draw");
    private static MethodBase UpdateMethod => typeof(GameMain).GetMethod("Update", AccessTools.all);

    public static Dictionary<string, Hook> Hooks = new()
    {
      ["Stop"] = new Hook()
      {
        InstallAction = () =>
        {
          GameMain.LuaCs.Hook.Add("stop", $"[{ModInfo.AssemblyName}] PluginLifeCycle.stop", (object[] args) =>
          {
            Instance?._stop?.Invoke(); return null;
          });
        },
      },

      ["BeforeGUIDraw"] = new Hook()
      {
        InstallAction = () =>
        {
          GameMain.LuaCs.Hook.Patch($"[{ModInfo.AssemblyName}] PluginLifeCycle.BeforeGUIDraw", GUIDrawMethod, (instance, ptable) =>
          {
            Instance?._beforeGUIDraw?.Invoke((SpriteBatch)ptable["spriteBatch"]);
            return null;
          }, LuaCsHook.HookMethodType.Before);
        },
      },

      ["AfterGUIDraw"] = new Hook()
      {
        InstallAction = () =>
        {
          GameMain.LuaCs.Hook.Patch($"[{ModInfo.AssemblyName}] PluginLifeCycle.AfterGUIDraw", GUIDrawMethod, (instance, ptable) =>
          {
            Instance?._afterGUIDraw?.Invoke((SpriteBatch)ptable["spriteBatch"]);
            return null;
          }, LuaCsHook.HookMethodType.After);
        },
      },

      ["AfterUpdate"] = new Hook()
      {
        InstallAction = () =>
        {
          GameMain.LuaCs.Hook.Patch($"[{ModInfo.AssemblyName}] PluginLifeCycle.AfterUpdate", UpdateMethod, (instance, ptable) =>
          {
            Instance?._afterUpdate?.Invoke();
            return null;
          }, LuaCsHook.HookMethodType.After);
        },
      },
    };




    static PluginLifeCycle()
    {
      Instance = new PluginLifeCycle();
      InstallClearHook();
    }

    private static void Clear()
    {
      Instance = null;
      Hooks = null;
      // remove hooks?
    }
    private static void InstallClearHook()
    {
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
        Hooks["Stop"].TryInstall();
      }
      remove
      {
        if (Instance is null) return;
        Instance._stop -= value;
        Hooks["Stop"].TryRemove();
      }
    }

    private event Action<SpriteBatch> _beforeGUIDraw; public static event Action<SpriteBatch> BeforeGUIDraw
    {
      add
      {
        if (Instance is null) return;
        Instance._beforeGUIDraw += value;
        Hooks["BeforeGUIDraw"].TryInstall();
      }
      remove
      {
        if (Instance is null) return;
        Instance._beforeGUIDraw -= value;
        Hooks["BeforeGUIDraw"].TryRemove();
      }
    }

    private event Action<SpriteBatch> _afterGUIDraw; public static event Action<SpriteBatch> AfterGUIDraw
    {
      add
      {
        if (Instance is null) return;
        Instance._afterGUIDraw += value;
        Hooks["AfterGUIDraw"].TryInstall();
      }
      remove
      {
        if (Instance is null) return;
        Instance._afterGUIDraw -= value;
        Hooks["AfterGUIDraw"].TryRemove();
      }
    }

    private event Action _afterUpdate; public static event Action AfterUpdate
    {
      add
      {
        if (Instance is null) return;
        Instance._afterUpdate += value;
        Hooks["AfterUpdate"].TryInstall();
      }
      remove
      {
        if (Instance is null) return;
        Instance._afterUpdate -= value;
        Hooks["AfterUpdate"].TryRemove();
      }
    }

  }
}
