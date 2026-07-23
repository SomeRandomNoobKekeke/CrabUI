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

    public DebugNode<string> Debug_Focus { get; private set; }

    public bool _Debug { get; set; } = true;
    public void InitDebug()
    {
      Debug_Focus = new(DebugCategory.Focus, _DebugHub, (msg) => msg) { IsOpen = true };

      Main.DebugRelays.Map(_DebugHub);
      Debug_Focus.Map(_DebugHub);

      _DebugHub.Output.Add(HandleDebugEvent);
    }

    public void HandleDebugEvent(DebugEvent e)
    {
      if (!_Debug) return;
      CUI.Logger.Log(e);
    }


  }
}