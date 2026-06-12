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
        prefix: new HarmonyMethod(KeyboardDispatcher_set_Subscriber_Replace)
      );
    }
    public void DisconnectFromGame()
    {
      Instance = null;
      GameMain.Instance.Window.TextInput -= CaptureWindowTextInput;
      GameMain.Instance.Window.KeyDown -= CaptureWindowKeyDown;

      Harmony.UnpatchSelf();
    }


    public static bool KeyboardDispatcher_set_Subscriber_Replace(KeyboardDispatcher __instance, IKeyboardSubscriber value)
    {
      KeyboardDispatcher _ = __instance;


      if (_._subscriber == value) { return false; }

      if (_._subscriber is GUITextBox)
      {
        TextInput.StopTextInput();
        _._subscriber.Selected = false;
      }

      if (value is GUITextBox box)
      {
        TextInput.SetTextInputRect(box.MouseRect);
        TextInput.StartTextInput();
        TextInput.SetTextInputRect(box.MouseRect);
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