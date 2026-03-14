using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public class SoloCUIRunner : ICUIRunner
  {
    public CUICore Core { get; set; }
    public ICUIRunnerDataSources DataSources { get; set; }

    private CUICore.UpdateConnectionHandle UpdateHandle => Core.UpdateHandle;
    private CUICore.DrawBeforeGUIConnectionHandle DrawBeforeGUIHandle => Core.DrawBeforeGUIHandle;
    private CUICore.DrawAfterGUIConnectionHandle DrawAfterGUIHandle => Core.DrawAfterGUIHandle;

    private IInputProvider Input => DataSources.Input;
    private IGameLifeCycleTracker LifeCycle => DataSources.LifeCycle;

    public void Connect()
    {
      ArgumentNullException.ThrowIfNull(Core);
      ArgumentNullException.ThrowIfNull(DataSources);

      DataSources.LifeCycle.AfterGUIDraw += (spritebatch) => DrawAfterGUIHandle.Draw();
    }
  }
}