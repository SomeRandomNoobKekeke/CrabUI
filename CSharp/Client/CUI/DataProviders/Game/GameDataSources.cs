using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public class GameDataSources : ICUIRunnerDataSources
  {
    public GameInputProvider Input { get; } = new();
    public GameLifeCycle LifeCycle { get; } = new();

    IInputProvider ICUIRunnerDataSources.Input => Input;
    IGameLifeCycleTracker ICUIRunnerDataSources.LifeCycle => LifeCycle;

    public void UnsubFromEvents()
    {
      LifeCycle.UnsubEvents();
    }

    public GameDataSources()
    {
      LifeCycle.ConnectToGame();
    }
  }
}