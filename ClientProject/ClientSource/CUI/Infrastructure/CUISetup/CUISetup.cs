using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

namespace CursedUI
{
  public partial class CUISetup
  {
    public ICUIRunnerDataSources DataSources { get; set; }
    public ICUIRunner Runner { get; set; }
    public CUICore Core { get; set; }

    public bool Started { get; private set; }

    public void Start()
    {
      if (!Started)
      {
        DataSources.ConnectToGame();
        Runner.Connect();
        Core.Activate();
        Started = true;
      }
    }

    public void Stop()
    {
      if (!Started) return;

      Runner.Disconnect();
      DataSources.DisconnectFromGame();
      Started = false;
    }

    public void WireUp()
    {
      Runner.Core = Core;
      Runner.DataSources = DataSources;
    }

    public void AnalyzeAssembly(Assembly assembly)
    {
      Runner.AnalyzeAssembly(assembly);
    }
  }
}