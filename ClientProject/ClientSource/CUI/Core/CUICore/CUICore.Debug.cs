using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUICore
  {
    public DebugHub _DebugHub { get; } = new()
    {
      IsOpen = false,
    };

    public bool _Debug { get; set; } = true;
    public void InitDebug()
    {
      _DebugHub.Output.Add(HandleDebugEvent);
    }

    public void HandleDebugEvent(DebugEvent e)
    {
      if (!_Debug) return;
      CUI.Logger.Log(e);
    }


  }
}