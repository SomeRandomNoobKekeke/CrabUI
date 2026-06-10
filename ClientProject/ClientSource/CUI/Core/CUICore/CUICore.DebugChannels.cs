using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUICore
  {
    public DebugRelayDict DebugRelays { get; } = new()
    {
      [DebugCategory.HandleGrab] = new DebugRelay(),
      [DebugCategory.PropSet] = new DebugRelay(),
      [DebugCategory.TreeChanged] = new DebugRelay(),
      [DebugCategory.LayoutUpdated] = new DebugRelay(),
    };
  }
}