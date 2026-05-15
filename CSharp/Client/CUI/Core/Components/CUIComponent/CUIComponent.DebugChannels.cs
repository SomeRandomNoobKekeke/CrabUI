using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    public DebugRelayDict DebugRelays { get; } = new()
    {
      ["Prop Set"] = new DebugRelay(),
      ["Child Added"] = new DebugRelay(),
      ["Child Removed"] = new DebugRelay(),
      ["Layout Updated"] = new DebugRelay(),
    };
  }
}