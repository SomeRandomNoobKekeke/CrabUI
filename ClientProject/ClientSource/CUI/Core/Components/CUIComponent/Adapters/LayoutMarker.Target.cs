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
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected partial class Adapters_Part : Part
    {
      public LayoutMarker_Adapter LayoutMarker { get; } = new();
      public class LayoutMarker_Adapter : Part, IAdapterPart, LayoutMarker.Target
      {
        public void Init()
        {
          //TODO init Children here
        }

        LayoutMarker.Target LayoutMarker.Target.Parent => Self.Tree.Parent?.Adapters.LayoutMarker;
        Layout LayoutMarker.Target.Layout => Self.Layout;

        IReadOnlyList<LayoutMarker.Target> LayoutMarker.Target.Children
          => new ListProxy<CUIComponent, LayoutMarker.Target>(
            Self.Tree.Children,
            child => child.Adapters.LayoutMarker
          );

        void LayoutMarker.Target.NotifyLayoutUpdated() => Self.LayoutUpdateNotifier.Notify();
      }
    }
  }
}