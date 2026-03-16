using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public partial class CUISetup
  {
    public ICUIRunnerDataSources DataSources { get; set; }
    public ICUIRunner Runner { get; set; }
    public CUICore Core { get; set; }

    public void WireUp()
    {
      Runner.Core = Core;
      Runner.DataSources = DataSources;
      Runner.Connect();
    }
  }
}