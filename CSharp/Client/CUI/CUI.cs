using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;



namespace CrabUI
{
  public static class CUI
  {
    static CUI()
    {
      PluginLifeCycle.Stop += Dispose;
    }

    public static CUISetup Setup { get; set; } = CUISetup.Default();
    public static void Start() => Setup.Start();
    public static void Stop() => Setup.Stop();

    public static Logger Logger = new()
    {
      PrintFilePath = false,
    };

    public static void Dispose()
    {
      Setup.Stop();
      Setup = null;
    }
  }
}