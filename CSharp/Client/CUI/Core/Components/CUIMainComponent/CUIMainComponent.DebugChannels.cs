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
  public partial class CUIMainComponent
  {
    protected InitDebugChannels_Part InitDebugChannels { get; } = new();
    public class InitDebugChannels_Part : Part
    {
      public void Init()
      {
        (Self as CUIComponent).DebugRelays.Map(Self.DebugRelays);

        Self.GrabbedHandleTracker.DebugRelay.Map(Self.DebugRelays["Handle Grab"]);

        Self.OnDebugOn += () => Self.DebugRelays.Open();
        Self.OnDebugOff += () => Self.DebugRelays.Close();
      }
    }

    public DebugRelayDict DebugRelays { get; } = new()
    {
      ["Handle Grab"] = new DebugRelay(),
      ["Prop Set"] = new DebugRelay(),
      ["Child Added"] = new DebugRelay(),
      ["Layout Updated"] = new DebugRelay(),
    };
  }
}