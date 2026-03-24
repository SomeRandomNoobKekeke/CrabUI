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
    public DebugChannels_Part DebugChannel { get; } = new();

    public class DebugChannels_Part : Part
    {
      public DebugNode<CUIComponent, CUIComponent> ChildAdded { get; } = new();

      public void AttachToMainComponent()
      {
        ChildAdded.Map(Self.MainComponent.DebugChannel.ChildAdded);
      }

      public void DetachFromMainComponent()
      {
        ChildAdded.Unmap(Self.MainComponent.DebugChannel.ChildAdded);
      }
    }
  }
}