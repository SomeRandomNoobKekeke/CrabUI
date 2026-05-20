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
      ["Handle Grab"] = new DebugRelay(),
      ["Prop Set"] = new DebugRelay(),
      ["Child Added"] = new DebugRelay(),
      ["Layout Updated"] = new DebugRelay(),
    };
  }
}