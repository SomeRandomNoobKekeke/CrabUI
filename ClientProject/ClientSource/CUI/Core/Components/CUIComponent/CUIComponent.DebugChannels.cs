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
    // public DebugNode<CUIComponent, CUIRect> Debug_RectSet { get; } = new(
    //   DebugCategory.RectSet, CUI.DebugHub,
    //   (component, rect) => $"{component}.Rect = {rect}"
    // );
    // public DebugNode<Type, object, CUIComponent, string> Debug_PropSet { get; } = new(
    //   DebugCategory.FunnyPropSet, CUI.DebugHub,
    //   (propType, value, host, propName) => $"{host}.{propName} = {value}"
    // );


    [InitMethod]
    private void InitDebugChannels()
    {
      // Debug_RectSet.Map(DebugRelays[DebugCategory.RectSet]);
      // DebugRelays[DebugCategory.LayoutMarked].Route(Layout.Debug_LayoutMarked);

      // Tree.Debug_ChildAdded.Map(DebugRelays[DebugCategory.TreeChanged]);
      // Tree.Debug_ChildRemoved.Map(DebugRelays[DebugCategory.TreeChanged]);
      // Tree.Debug_LayoutMarked.Map(DebugRelays[DebugCategory.LayoutMarked]);

      // Absolute.Debug_ValueSet.Map(Self.DebugRelays[DebugCategory.LayoutPropSet]);
      // Relative.Debug_ValueSet.Map(Self.DebugRelays[DebugCategory.LayoutPropSet]);
    }


    public DebugRelayDict DebugRelays { get; } = new()
    {
      // [DebugCategory.RoundedRect] = new DebugRelay(),
      // [DebugCategory.LayoutPropSet] = new DebugRelay(),
      // [DebugCategory.RectSet] = new DebugRelay(),
      // [DebugCategory.TreeChanged] = new DebugRelay(),
      // [DebugCategory.LayoutUpdated] = new DebugRelay(),
      // [DebugCategory.LayoutMarked] = new DebugRelay(),
    };
  }
}