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
  public class GameInputProvider
  {
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

    public void ConnectToGame()
    {
      GameMain.Instance.Window.TextInput += CaptureWindowTextInput;
      GameMain.Instance.Window.KeyDown += CaptureWindowKeyDown;
    }

    public void DisconnectFromGame()
    {
      GameMain.Instance.Window.TextInput -= CaptureWindowTextInput;
      GameMain.Instance.Window.KeyDown -= CaptureWindowKeyDown;
    }
  }
}