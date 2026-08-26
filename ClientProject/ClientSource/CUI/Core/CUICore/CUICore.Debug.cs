using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class CUICore
  {
    public DebugHub _DebugHub { get; } = new()
    {
      IsOpen = false,
    };

    public static CUIDebugger Debugger => Instance?._Debugger;
    public CUIDebugger _Debugger { get; private set; }

    public void InitDebug()
    {
      _Debugger = new();
      Main.DebugRelays.Map(_DebugHub);
    }
  }
}