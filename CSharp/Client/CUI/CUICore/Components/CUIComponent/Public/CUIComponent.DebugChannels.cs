using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentInjector;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    public DebugChannelsDict DebugChannels { get; } = new()
    {
      ["Child Added"] = new DebugNode<CUIComponent, CUIComponent>(),
    };
  }
}