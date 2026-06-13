using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Barotrauma;
using HarmonyLib;
using EventInput;

namespace CrabUI
{
  public class GameInputProvider : IInputProvider
  {
    public Harmony Harmony { get; } = new Harmony($"{ModInfo.HookId}.CUI.Input");

    TextInputEventPackBuilder TextInputBuilder = new();


    public MouseState ScanMouse() => Mouse.GetState();
    public KeyboardState ScanKeyboard() => Keyboard.GetState();
    public TextInputEventPack ScanTextInput() => TextInputBuilder.Build();

    private void CaptureWindowTextInput(object sender, TextInputEventArgs args)
    {
      TextInputBuilder.TextInputEvents.Add(args);
    }

    private void CaptureWindowKeyDown(object sender, TextInputEventArgs args)
    {
      TextInputBuilder.KeyDownEvents.Add(args);
    }


    public static GameInputProvider Instance;

    public void ConnectToGame()
    {
      Instance = this;
      GameMain.Instance.Window.TextInput += CaptureWindowTextInput;
      GameMain.Instance.Window.KeyDown += CaptureWindowKeyDown;

      Harmony.Patch(
        original: typeof(KeyboardDispatcher).GetMethod("set_Subscriber", AccessTools.all),
        prefix: new HarmonyMethod(typeof(GameInputProvider).GetMethod("KeyboardDispatcher_set_Subscriber_Replace"))
      );

      Harmony.Patch(
        original: typeof(KeyboardDispatcher).GetMethod("EventInput_TextEditing", AccessTools.all),
        prefix: new HarmonyMethod(typeof(GameInputProvider).GetMethod("KeyboardDispatcher_EventInput_TextEditing"))
      );

      Harmony.Patch(
        original: typeof(KeyboardDispatcher).GetMethod("EventInput_KeyDown", AccessTools.all),
        prefix: new HarmonyMethod(typeof(GameInputProvider).GetMethod("KeyboardDispatcher_EventInput_KeyDown"))
      );

      Harmony.Patch(
        original: typeof(KeyboardDispatcher).GetMethod("EventInput_CharEntered", AccessTools.all),
        prefix: new HarmonyMethod(typeof(GameInputProvider).GetMethod("KeyboardDispatcher_EventInput_CharEntered"))
      );
    }

    public void DisconnectFromGame()
    {
      Instance = null;
      GameMain.Instance.Window.TextInput -= CaptureWindowTextInput;
      GameMain.Instance.Window.KeyDown -= CaptureWindowKeyDown;

      Harmony.UnpatchSelf();
    }

    // instead of app wide TextInput.StopTextInput();
    public static bool InputBlocked { get; set; }

    //Just to block input 
    public static bool KeyboardDispatcher_EventInput_TextEditing(KeyboardDispatcher __instance, object sender, TextEditingEventArgs e)
    {
      if (__instance is null) return true;
      if (InputBlocked) return false;

      __instance._subscriber?.ReceiveEditingInput(e.Text, e.Start, e.Length);
      return false;
    }
    //Just to block input 
    public static bool KeyboardDispatcher_EventInput_KeyDown(KeyboardDispatcher __instance, object sender, KeyEventArgs e)
    {
      if (__instance is null) return true;
      if (InputBlocked) return false;

      __instance._subscriber?.ReceiveSpecialInput(e.KeyCode);
      if (char.IsControl(e.Character))
      {
        __instance._subscriber?.ReceiveCommandInput(e.Character);
      }
      return false;
    }
    //Just to block input 
    public static bool KeyboardDispatcher_EventInput_CharEntered(KeyboardDispatcher __instance, object sender, CharacterEventArgs e)
    {
      if (__instance is null) return true;
      if (InputBlocked) return false;

      __instance._subscriber?.ReceiveTextInput(e.Character);
      return false;
    }

    /// <summary>
    /// This is where focus resolved
    /// </summary>
    public static bool KeyboardDispatcher_set_Subscriber_Replace(KeyboardDispatcher __instance, IKeyboardSubscriber value)
    {
      KeyboardDispatcher _ = __instance;
      TextInput.StartTextInput();

      if (_._subscriber == value) { return false; }

      if (_._subscriber is GUITextBox)
      {
        // TextInput.StopTextInput();
        InputBlocked = true;

        _._subscriber.Selected = false;
      }

      if (value is GUITextBox box)
      {
        TextInput.SetTextInputRect(box.MouseRect);
        TextInput.StartTextInput();
        InputBlocked = false;
        TextInput.SetTextInputRect(box.MouseRect);
        Instance?.TextInputBuilder.StealFocus();
      }

      _._subscriber = value;
      if (value != null)
      {
        value.Selected = true;
      }

      return false;
    }


  }
}