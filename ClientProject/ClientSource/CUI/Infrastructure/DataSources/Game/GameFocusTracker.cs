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
  public class DummyIKeyboardSubscriber : IKeyboardSubscriber
  {
    public bool Selected { get; set; }
    public void ReceiveCommandInput(char command) { }
    public void ReceiveEditingInput(string text, int start, int length) { }
    public void ReceiveSpecialInput(Keys key) { }
    public void ReceiveTextInput(char inputChar) { }
    public void ReceiveTextInput(string text) { }
  }

  public class GameFocusTracker
  {
    public static GameFocusTracker Instance;


    public Harmony Harmony { get; } = new Harmony($"{ModInfo.HookId}.CUI.Focus");

    public DummyIKeyboardSubscriber DummyIKeyboardSubscriber { get; } = new();


    public event Action VanillaGUIElementFocused;
    public void GrabFocus(IFocusable focusable)
    {
      GUI.KeyboardDispatcher.Subscriber = focusable is null ? null : DummyIKeyboardSubscriber;
    }

    public void ConnectToGame()
    {
      Instance = this;

      Harmony.Patch(
        original: typeof(KeyboardDispatcher).GetMethod("set_Subscriber", AccessTools.all),
        prefix: new HarmonyMethod(GetType().GetMethod("KeyboardDispatcher_set_Subscriber_Replace"))
      );

      Harmony.Patch(
        original: typeof(KeyboardDispatcher).GetMethod("EventInput_TextEditing", AccessTools.all),
        prefix: new HarmonyMethod(GetType().GetMethod("BlockInputPatch"))
      );

      Harmony.Patch(
        original: typeof(KeyboardDispatcher).GetMethod("EventInput_KeyDown", AccessTools.all),
        prefix: new HarmonyMethod(GetType().GetMethod("BlockInputPatch"))
      );

      Harmony.Patch(
        original: typeof(KeyboardDispatcher).GetMethod("EventInput_CharEntered", AccessTools.all),
        prefix: new HarmonyMethod(GetType().GetMethod("BlockInputPatch"))
      );
    }

    public static bool KeyboardDispatcher_set_Subscriber_Replace(KeyboardDispatcher __instance, IKeyboardSubscriber value)
    {
      KeyboardDispatcher _ = __instance;

      if (_._subscriber == value) { return false; }

      if (_._subscriber is GUITextBox)
      {
        InputBlocked = true; //TextInput.StopTextInput();
        _._subscriber.Selected = false;
      }

      if (value is GUITextBox box)
      {
        TextInput.SetTextInputRect(box.MouseRect);
        TextInput.StartTextInput();
        TextInput.SetTextInputRect(box.MouseRect);
        InputBlocked = false;
      }

      _._subscriber = value;
      if (value != null)
      {
        value.Selected = true;
        Instance?.VanillaGUIElementFocused?.Invoke();
      }

      return false;
    }

    // instead of app wide TextInput.StopTextInput();
    public static bool InputBlocked { get; set; }
    public static bool BlockInputPatch(KeyboardDispatcher __instance)
    {
      return __instance is null || !InputBlocked;
    }




    public void DisconnectFromGame()
    {
      Harmony.UnpatchSelf();
      Instance = null;
    }
  }
}