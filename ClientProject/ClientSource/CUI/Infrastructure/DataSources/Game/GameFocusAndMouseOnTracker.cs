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
  public class GameFocusAndMouseOnTracker : IFocusAndMouseOnTracker
  {

    public Harmony Harmony { get; } = new Harmony($"{ModInfo.HookId}.CUI.Focus");



    public void ConnectToGame()
    {

      Harmony.Patch(
        original: typeof(GUI).GetMethod("UpdateMouseOn", AccessTools.all),
        postfix: new HarmonyMethod(typeof(GameFocusAndMouseOnTracker).GetMethod("UpdateMouseOn_Postfix"))
      );
    }

    public static bool UpdateMouseOn_Postfix(ref GUIComponent __result)
    {
      return false;
    }

    public void DisconnectFromGame()
    {

      Harmony.UnpatchSelf();
    }
  }
}