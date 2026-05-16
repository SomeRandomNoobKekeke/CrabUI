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

        Self.Debug_MouseEnter.Map(Self.DebugRelays["Mouse Enter / Leave"]);
        Self.Debug_MouseLeave.Map(Self.DebugRelays["Mouse Enter / Leave"]);
      }
    }

    public DebugNode<IEnumerable<IEventConsumer>> Debug_MouseEnter { get; } = new(
      "Mouse Enter / Leave", CUI.DebugHub,
      (list) => $"Mouse Enter: {Logger.Wrap.IEnumerable(list)}"
    );
    public DebugNode<IEnumerable<IEventConsumer>> Debug_MouseLeave { get; } = new(
      "Mouse Enter / Leave", CUI.DebugHub,
      (list) => $"Mouse Leave: {Logger.Wrap.IEnumerable(list)}"
    );

    public DebugRelayDict DebugRelays { get; } = new()
    {
      ["Mouse Enter / Leave"] = new DebugRelay(),
      ["Prop Set"] = new DebugRelay(),
      ["Child Added"] = new DebugRelay(),
      ["Layout Updated"] = new DebugRelay(),
    };
  }
}