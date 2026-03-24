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
  public partial class CUIMainComponent
  {
    public new DebugChannels_Part DebugChannel { get; } = new();

    public class DebugChannels_Part : Part
    {
      public void Init()
      {
        (Self as CUIComponent).DebugChannel.AttachToMainComponent(Self);
      }

      public DebugNode<CUIComponent, CUIComponent> ChildAdded { get; } = new();
    }
  }
}