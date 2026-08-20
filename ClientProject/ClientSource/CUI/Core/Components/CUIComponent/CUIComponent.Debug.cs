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
    public CUIDebugNode<CUIVisualComponent> Debug_ComponentCreated { get; }
      = new(DebugCategory.ComponentCreated) { IsOpen = true };

    public CUIDebugNode<CUIComponent, CUIRect> Debug_RectSet { get; } = new(DebugCategory.RectSet);

    public override void OnDebugOn()
    {
      DebugRelays.Open();
      base.OnDebugOn();
    }

    public override void OnDebugOff()
    {
      DebugRelays.Close();
      base.OnDebugOff();
    }



    [InitMethod]
    private void InitDebugChannels()
    {
      Debug_RectSet.Map(DebugRelays[DebugCategory.RectSet]);
    }

    public DebugRelayDict DebugRelays { get; } = new()
    {
      [DebugCategory.RectSet] = new DebugRelay(),
    };
  }
}