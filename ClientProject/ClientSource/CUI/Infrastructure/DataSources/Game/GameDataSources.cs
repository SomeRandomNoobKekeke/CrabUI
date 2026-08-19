using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public class GameDataSources : ICUIRunnerDataSources
  {
    public GameLifeCycle GameLifeCycle { get; } = new();
    public GameInputProvider GameInputProvider { get; } = new();
    public GameMouseOnTracker GameMouseOnTracker { get; } = new();

    public void ConnectToGame()
    {
      GameLifeCycle.ConnectToGame();
      GameInputProvider.ConnectToGame();
      GameMouseOnTracker.ConnectToGame();
    }

    public void DisconnectFromGame()
    {
      GameLifeCycle.DisconnectFromGame();
      GameInputProvider.DisconnectFromGame();
      GameMouseOnTracker.DisconnectFromGame();
    }

    public event Action<SpriteBatch> BeforeGUIDraw
    {
      add => GameLifeCycle.BeforeGUIDraw += value;
      remove => GameLifeCycle.BeforeGUIDraw -= value;
    }
    public event Action<SpriteBatch> AfterGUIDraw
    {
      add => GameLifeCycle.AfterGUIDraw += value;
      remove => GameLifeCycle.AfterGUIDraw -= value;
    }
    public event Action<GameTime> Update
    {
      add => GameLifeCycle.Update += value;
      remove => GameLifeCycle.Update -= value;
    }


    public event Action SyncMouseOn
    {
      add => GameMouseOnTracker.SyncMouseOn += value;
      remove => GameMouseOnTracker.SyncMouseOn -= value;
    }

    public KeyboardState ScanKeyboard() => GameInputProvider.ScanKeyboard();
    public MouseState ScanMouse() => GameInputProvider.ScanMouse();
    public TextInputEventPack ScanTextInput() => GameInputProvider.ScanTextInput();




  }
}