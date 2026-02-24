using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;

namespace CrabUI
{
  public static partial class CUI
  {
    public static Logger Logger = new()
    {
      PrintFilePath = true,
    };

    private static CUIRunner _CUIRunner;
    public static CUIRunner CUIRunner
    {
      get
      {
        _CUIRunner ??= new CUIRunner();
        return _CUIRunner;
      }
    }

    public static CUICore CUICore => CUIRunner.CUICore;
    public static CUIMainComponent Main => CUICore.Main;

    public static void Dispose()
    {
      _CUIRunner?.Dispose();
      _CUIRunner = null;
    }
  }
}