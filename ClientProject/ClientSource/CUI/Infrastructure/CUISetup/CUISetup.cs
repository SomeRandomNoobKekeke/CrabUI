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

    public void Start()
    {
      Runner.Connect();
      Core.Activate();
      Started = true;
    }

    public void Stop()
    {
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