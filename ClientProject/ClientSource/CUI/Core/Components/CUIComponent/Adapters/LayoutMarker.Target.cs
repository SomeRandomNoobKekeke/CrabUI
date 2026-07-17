using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
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

        LayoutMarker.Target LayoutMarker.Target.Parent => Self.Parent?.Adapters.LayoutMarker;
        Layout LayoutMarker.Target.Layout => Self.Layout;

        IReadOnlyList<LayoutMarker.Target> LayoutMarker.Target.Children
          => Self.Children.ReadOnlyAs<CUIComponent, LayoutMarker.Target>(child => child.Adapters.LayoutMarker);

        void LayoutMarker.Target.NotifyLayoutUpdated() => Self.LayoutUpdateNotifier.Notify();
      }
    }
  }
}