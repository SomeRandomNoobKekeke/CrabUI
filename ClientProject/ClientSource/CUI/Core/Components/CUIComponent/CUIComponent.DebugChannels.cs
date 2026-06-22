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
        Self.Tree.DebugRelay.Map(Self.DebugRelays[DebugCategory.TreeChanged]);
        Self.Debug_RectSet.Map(Self.DebugRelays[DebugCategory.RectSet]);

        Self.OnDebugOn += () => Self.DebugRelays.Open();
        Self.OnDebugOff += () => Self.DebugRelays.Close();
      }
    }

    public DebugNode<Type, object, CUIComponent, string> Debug_PropSet { get; } = new(
      DebugCategory.FunnyPropSet, CUI.DebugHub,
      (propType, value, host, propName) => $"{host}.{propName} = {value}"
    );

    public DebugRelayDict DebugRelays { get; } = new()
    {
      [DebugCategory.LayoutPropSet] = new DebugRelay(),
      [DebugCategory.RectSet] = new DebugRelay(),
      [DebugCategory.TreeChanged] = new DebugRelay(),
      [DebugCategory.LayoutUpdated] = new DebugRelay(),
      [DebugCategory.RectSet] = new DebugRelay(),
    };
  }
}