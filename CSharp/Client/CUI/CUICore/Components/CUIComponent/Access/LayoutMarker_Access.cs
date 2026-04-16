using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUIComponent
  {
    public partial class Access_Part : Part
    {
      public LayoutMarker_Access LayoutMarker { get; } = new();
      public class LayoutMarker_Access : Part, IAccess, LayoutMarker.Target
      {
        public void Init()
        {
          //TODO init Children here
        }

        LayoutMarker.Target LayoutMarker.Target.Parent => Self.Tree.Parent?.Access_CUIComponent.LayoutMarker;
        Layout LayoutMarker.Target.Layout => Self.LayoutSlot.Layout;

        IReadOnlyList<LayoutMarker.Target> LayoutMarker.Target.Children
          => new ListProxy<CUIComponent, LayoutMarker.Target>(
            Self.Tree.Children,
            child => child.Access_CUIComponent.LayoutMarker
          );

        void LayoutMarker.Target.NotifyMainComponent()
          => Self.MainComponentTracker.MainComponent?.NotifyThatLayoutHasChanged();
      }
    }
  }
}