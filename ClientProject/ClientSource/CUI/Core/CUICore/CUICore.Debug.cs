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

    public DebugNode<IFocusable> Debug_FocusedChanged { get; private set; }

    public bool _Debug { get; set; } = true;
    public void InitDebug()
    {
      Debug_FocusedChanged = new(DebugCategory.FocusedChanged, _DebugHub,
        (focused) => $"Focused [{focused}]"
      )
      { IsOpen = true };

      Main.DebugRelays.Map(_DebugHub);
      Debug_FocusedChanged.Map(_DebugHub);

      _DebugHub.Output.Add(HandleDebugEvent);
    }

    public void HandleDebugEvent(DebugEvent e)
    {
      if (!_Debug) return;
      CUI.Logger.Log(e);
    }


  }
}