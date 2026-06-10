using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public interface ICUIRunnerDataSources
  {
    public IInputProvider Input { get; }
    public IGameLifeCycleTracker LifeCycle { get; }
  }
}