using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Barotrauma;
using HarmonyLib;
using EventInput;

namespace CrabUI
{
  public class GameMouseOnTracker
  {
    public static GameMouseOnTracker Instance;
    public Harmony Harmony { get; } = new Harmony($"{ModInfo.HookId}.CUI.MouseOn");

    public event Action SyncMouseOn;

    public static void SyncMouseOnHandler()
    {
      // It can also be called from UpdateGUIMessageBoxesOnly
      if (CallFrom_GUI_Update) Instance?.SyncMouseOn?.Invoke();
    }

    public void ConnectToGame()
    {
      Instance = this;

      Harmony.Patch(
        original: typeof(GUI).GetMethod("UpdateMouseOn", AccessTools.all),
        postfix: new HarmonyMethod(GetType().GetMethod("SyncMouseOnHandler"))
      );

      Harmony.Patch(
        original: typeof(GUI).GetMethod("Update", AccessTools.all),
        prefix: new HarmonyMethod(GetType().GetMethod("GUI_Update_Prefix")),
        postfix: new HarmonyMethod(GetType().GetMethod("GUI_Update_Postfix"))
      );
    }

    public static bool CallFrom_GUI_Update; //HACK should use transpiler to hook that call
    public static void GUI_Update_Prefix() => CallFrom_GUI_Update = true;
    public static void GUI_Update_Postfix() => CallFrom_GUI_Update = false;

    public void DisconnectFromGame()
    {
      Harmony.UnpatchSelf();
      Instance = null;
    }
  }
}