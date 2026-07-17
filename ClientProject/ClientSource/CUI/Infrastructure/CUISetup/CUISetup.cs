using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

namespace CrabUI
{
  public partial class CUISetup
  {
    public ICUIRunnerDataSources DataSources { get; set; }
    public ICUIRunner Runner { get; set; }
    public CUICore Core { get; set; }

    public bool Started { get; private set; }

    public void Start(Assembly callingAssembly)
    {
      if (!Started)
      {
        Runner.Connect();
        Core.Activate();

        Started = true;
      }

      Runner.OnStartAttempt(callingAssembly);
    }

    public void Stop()
    {
      if (!Started) return;
      Runner.Disconnect();
      Started = false;
    }

    public void WireUp()
    {
      Runner.Core = Core;
      Runner.DataSources = DataSources;
    }
  }
}