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
      public void AttachToMainComponent(CUIMainComponent mainComponent)
      {
        ChildAdded.Map(mainComponent.DebugChannel.ChildAdded);
      }

      public void DetachFromMainComponent(CUIMainComponent mainComponent)
      {
        ChildAdded.Unmap(mainComponent.DebugChannel.ChildAdded);
      }



      public DebugNode<CUIComponent, CUIComponent> ChildAdded { get; } = new();
    }
  }
}