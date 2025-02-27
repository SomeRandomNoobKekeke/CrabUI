using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;
using EventInput;

namespace CrabUI
{
  public partial class CUI
  {
    private static void PatchAll()
    {
      harmony.Patch(
        original: typeof(GUI).GetMethod("Draw", AccessTools.all),
        prefix: new HarmonyMethod(typeof(CUI).GetMethod("GUI_Draw_Prefix", AccessTools.all))
      );

      harmony.Patch(
        original: typeof(GUI).GetMethod("DrawCursor", AccessTools.all),
        prefix: new HarmonyMethod(typeof(CUI).GetMethod("GUI_DrawCursor_Prefix", AccessTools.all))
      );

      harmony.Patch(
        original: typeof(GameMain).GetMethod("Update", AccessTools.all),
        postfix: new HarmonyMethod(typeof(CUI).GetMethod("CUIUpdate", AccessTools.all))
      );

      harmony.Patch(
        original: typeof(GUI).GetMethod("UpdateMouseOn", AccessTools.all),
        prefix: new HarmonyMethod(typeof(CUI).GetMethod("GUI_UpdateMouseOn_Prefix", AccessTools.all))
      );

      harmony.Patch(
        original: typeof(GUI).GetMethod("UpdateMouseOn", AccessTools.all),
        postfix: new HarmonyMethod(typeof(CUI).GetMethod("GUI_UpdateMouseOn_Postfix", AccessTools.all))
      );

      harmony.Patch(
        original: typeof(Camera).GetMethod("MoveCamera", AccessTools.all),
        prefix: new HarmonyMethod(typeof(CUI).GetMethod("CUIBlockScroll", AccessTools.all))
      );

      harmony.Patch(
        original: typeof(KeyboardDispatcher).GetMethod("set_Subscriber", AccessTools.all),
        prefix: new HarmonyMethod(typeof(CUI).GetMethod("KeyboardDispatcher_set_Subscriber_Replace", AccessTools.all))
      );

      harmony.Patch(
        original: typeof(GUI).GetMethod("TogglePauseMenu", AccessTools.all, new Type[] { }),
        postfix: new HarmonyMethod(typeof(CUI).GetMethod("GUI_TogglePauseMenu_Postfix", AccessTools.all))
      );

      harmony.Patch(
        original: typeof(GUI).GetMethod("get_InputBlockingMenuOpen", AccessTools.all),
        postfix: new HarmonyMethod(typeof(CUI).GetMethod("GUI_InputBlockingMenuOpen_Postfix", AccessTools.all))
      );
    }

    public static void GUI_InputBlockingMenuOpen_Postfix(ref bool __result)
    {
      __result = __result || CUI.InputBlockingMenuOpen;
    }

    public static void GUI_TogglePauseMenu_Postfix()
    {
      try
      {
        if (GUI.PauseMenu != null)
        {
          GUIFrame frame = GUI.PauseMenu;
          GUIComponent pauseMenuInner = frame.GetChild(1);
          GUIComponent list = frame.GetChild(1).GetChild(0);
          GUIButton resumeButton = (GUIButton)list.GetChild(0);

          GUIButton.OnClickedHandler oldHandler = resumeButton.OnClicked;

          resumeButton.OnClicked = (GUIButton button, object obj) =>
          {
            bool guh = oldHandler(button, obj);
            CUI.InvokeOnPauseMenuToggled();
            return guh;
          };
        }
      }
      catch (Exception e) { CUI.Warning(e); }

      CUI.InvokeOnPauseMenuToggled();
    }

    private static void CUIUpdate(GameTime gameTime)
    {
      try
      {
        CUI.Input?.Scan(gameTime.TotalGameTime.TotalSeconds);
        TopMain?.Update(gameTime.TotalGameTime.TotalSeconds);
        Main?.Update(gameTime.TotalGameTime.TotalSeconds);
      }
      catch (Exception e) { CUI.Warning($"CUI: {e}"); }
    }

    private static void GUI_Draw_Prefix(SpriteBatch spriteBatch)
    {
      try { Main?.Draw(spriteBatch); }
      catch (Exception e) { CUI.Warning($"CUI: {e}"); }
    }

    private static void GUI_DrawCursor_Prefix(SpriteBatch spriteBatch)
    {
      try { TopMain?.Draw(spriteBatch); }
      catch (Exception e) { CUI.Warning($"CUI: {e}"); }
    }

    private static void GUI_UpdateMouseOn_Prefix(ref GUIComponent __result)
    {
      //if (TopMain.MouseOn != null && TopMain.MouseOn != TopMain) GUI.MouseOn = CUIComponent.dummyComponent;
    }

    private static void GUI_UpdateMouseOn_Postfix(ref GUIComponent __result)
    {
      if (GUI.MouseOn == null && Main.MouseOn != null && Main.MouseOn != Main) GUI.MouseOn = CUIComponent.dummyComponent;
      if (TopMain.MouseOn != null && TopMain.MouseOn != TopMain) GUI.MouseOn = CUIComponent.dummyComponent;

    }

    private static void CUIBlockScroll(float deltaTime, ref bool allowMove, ref bool allowZoom, bool allowInput, bool? followSub)
    {
      if (GUI.MouseOn == CUIComponent.dummyComponent) allowZoom = false;
    }

    private static bool KeyboardDispatcher_set_Subscriber_Replace(IKeyboardSubscriber value, KeyboardDispatcher __instance)
    {
      if (FocusResolver == null) return true;
      FocusResolver.OnVanillaIKeyboardSubscriberSet(value);
      return false;
    }

  }
}