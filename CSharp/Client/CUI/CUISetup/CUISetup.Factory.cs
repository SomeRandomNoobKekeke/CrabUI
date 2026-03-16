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
    public static CUISetup Default()
    {
      CUISetup setup = new CUISetup();

      setup.DataSources = new GameDataSources();
      setup.Runner = new SoloCUIRunner();
      setup.Core = new CUICore();
      setup.WireUp();

      return setup;
    }
  }
}