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
    public static Logger Logger = new()
    {
      PrintFilePath = false,
    };

    public static void Dispose()
    {

    }
  }
}