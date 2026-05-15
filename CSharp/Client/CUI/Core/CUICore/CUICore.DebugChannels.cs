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
    protected InitDebugChannels_Part InitDebugChannels { get; } = new();
    public class InitDebugChannels_Part : Part
    {
      public void Init()
      {
        Self.DebugRelays.Route(Self.Main.DebugRelays);
        Self.DebugRelays.Map(Self.DebugHub);
      }
    }

    public DebugRelayDict DebugRelays { get; } = new()
    {
      ["Prop Set"] = new DebugRelay(),
      ["Child Added"] = new DebugRelay(),
      ["Layout Updated"] = new DebugRelay(),
    };


  }
}