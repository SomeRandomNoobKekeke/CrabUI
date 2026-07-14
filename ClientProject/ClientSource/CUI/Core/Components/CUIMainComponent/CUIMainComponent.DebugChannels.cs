using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
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

        Self.GrabbedHandleTracker.DebugRelay.Map(Self.DebugRelays[DebugCategory.HandleGrab]);

        Self.OnDebugOn += () => Self.DebugRelays.Open();
        Self.OnDebugOff += () => Self.DebugRelays.Close();
      }
    }

    public DebugRelayDict DebugRelays { get; } = new()
    {
      [DebugCategory.RoundedRect] = new DebugRelay(),
      [DebugCategory.HandleGrab] = new DebugRelay(),
      [DebugCategory.LayoutPropSet] = new DebugRelay(),
      [DebugCategory.RectSet] = new DebugRelay(),
      [DebugCategory.TreeChanged] = new DebugRelay(),
      [DebugCategory.LayoutUpdated] = new DebugRelay(),
      [DebugCategory.LayoutMarked] = new DebugRelay(),
    };
  }
}