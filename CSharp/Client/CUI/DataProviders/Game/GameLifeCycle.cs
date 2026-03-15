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
  public class GameLifeCycle : IGameLifeCycleTracker
  {
    private ClearableEvent<SpriteBatch> _BeforeGUIDraw = new();
    private ClearableEvent<SpriteBatch> _AfterGUIDraw = new();
    private ClearableEvent _Update = new();


    public event Action<SpriteBatch> BeforeGUIDraw
    {
      add => _BeforeGUIDraw.Add(value);
      remove => _BeforeGUIDraw.Remove(value);
    }
    public event Action<SpriteBatch> AfterGUIDraw
    {
      add => _AfterGUIDraw.Add(value);
      remove => _AfterGUIDraw.Remove(value);
    }
    public event Action Update
    {
      add => _Update.Add(value);
      remove => _Update.Remove(value);
    }

    public void ConnectToGame()
    {
      PluginLifeCycle.BeforeGUIDraw += (spritebatch) => _BeforeGUIDraw.Raise(spritebatch);
      PluginLifeCycle.AfterGUIDraw += (spritebatch) => _AfterGUIDraw.Raise(spritebatch);
      PluginLifeCycle.AfterUpdate += () => _Update.Raise();
    }

    public void UnsubEvents()
    {
      _BeforeGUIDraw.Clear();
      _AfterGUIDraw.Clear();
      _Update.Clear();
    }
  }
}