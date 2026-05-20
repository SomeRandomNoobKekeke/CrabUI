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
    protected InitDebugChannels_Part InitDebugChannels { get; } = new();
    public class InitDebugChannels_Part : Part
    {
      public void Init()
      {
        Self.OnDebugOn += () => Self.DebugRelays.Open();
        Self.OnDebugOff += () => Self.DebugRelays.Close();
      }
    }

    public DebugNode<Type, object, CUIComponent, string> Debug_PropSet { get; } = new(
      "Funny Prop Set", CUI.DebugHub,
      (propType, value, host, propName) => $"{host}.{propName} = {value}"
    )
    {
      IsOpen = true,
    };

    public DebugRelayDict DebugRelays { get; } = new()
    {
      ["Prop Set"] = new DebugRelay(),
      ["Child Added"] = new DebugRelay(),
      ["Child Removed"] = new DebugRelay(),
      ["Layout Updated"] = new DebugRelay(),
    };
  }
}