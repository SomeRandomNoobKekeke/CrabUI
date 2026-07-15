using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
namespace CrabUI
{
  public partial class CUIComponent
  {
    public DebugNode<CUIComponent, CUIRect> Debug_RectSet { get; } = new(
      DebugCategory.RectSet, CUI.DebugHub,
      (component, rect) => $"{component}.Rect = {rect}"
    );
    public DebugNode<Type, object, CUIComponent, string> Debug_PropSet { get; } = new(
      DebugCategory.FunnyPropSet, CUI.DebugHub,
      (propType, value, host, propName) => $"{host}.{propName} = {value}"
    );

    protected InitDebugChannels_Part InitDebugChannels { get; } = new();
    public class InitDebugChannels_Part : Part
    {
      public void Init()
      {

        Self.Debug_RectSet.Map(Self.DebugRelays[DebugCategory.RectSet]);
        Self.DebugRelays[DebugCategory.LayoutMarked].Route(Self.Layout.Debug_LayoutMarked);

        Self.DebugRelays[DebugCategory.RoundedRect].Route(Self.Background.Debug_RoundedRect);

        Self.OnDebugOn += () => Self.DebugRelays.Open();
        Self.OnDebugOff += () => Self.DebugRelays.Close();
      }
    }



    public DebugRelayDict DebugRelays { get; } = new()
    {
      [DebugCategory.RoundedRect] = new DebugRelay(),
      [DebugCategory.LayoutPropSet] = new DebugRelay(),
      [DebugCategory.RectSet] = new DebugRelay(),
      [DebugCategory.TreeChanged] = new DebugRelay(),
      [DebugCategory.LayoutUpdated] = new DebugRelay(),
      [DebugCategory.LayoutMarked] = new DebugRelay(),
    };
  }
}