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
    public DebugRelayDict DebugRelays { get; } = new()
    {
      [DebugCategory.RoundedRect] = new DebugRelay(),
      [DebugCategory.HandleGrab] = new DebugRelay(),
      [DebugCategory.LayoutPropSet] = new DebugRelay(),
      [DebugCategory.RectSet] = new DebugRelay(),
      [DebugCategory.TreeChanged] = new DebugRelay(),
      [DebugCategory.TreeChanged] = new DebugRelay(),
      [DebugCategory.LayoutUpdated] = new DebugRelay(),
      [DebugCategory.LayoutMarked] = new DebugRelay(),
    };
  }
}