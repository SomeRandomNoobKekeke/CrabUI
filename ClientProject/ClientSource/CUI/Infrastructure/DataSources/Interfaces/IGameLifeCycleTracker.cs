using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public interface IGameLifeCycleTracker
  {
    public event Action<SpriteBatch> BeforeGUIDraw;
    public event Action<SpriteBatch> AfterGUIDraw;
    public event Action Update;
    public event Action SyncMouseOn;
  }
}