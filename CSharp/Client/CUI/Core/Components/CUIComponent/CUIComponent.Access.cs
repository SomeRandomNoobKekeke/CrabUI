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

namespace CrabUI
{
  public partial class CUIComponent
  {
    private ComponentAccess Access { get; }
    private partial class ComponentAccess
    {
      private CUIComponent Host;
      public ComponentAccess(CUIComponent host) => Host = host;
      public override string ToString() => Host.ToString();
    }

    private partial class ComponentAccess : IRectElement
    {
      CUIRect IRectElement.Rect
      {
        get => Host.CUIProps.Rect.Value;
        set => Host.CUIProps.Rect.Value = value;
      }
    }

    private partial class ComponentAccess : Layout.ILayoutHost
    {
      IList Layout.ILayoutHost.Children => new ListProxy<CUIComponent, ComponentAccess>(Host.Children, c => c.Access);
    }

    private partial class ComponentAccess : PlainLayout.IPlainLayoutHost { }

    private partial class ComponentAccess : PlainLayout.IPlainLayoutElement
    {
      CUINullRect PlainLayout.IPlainLayoutElement.Absolute => Host.CUIProps.Absolute.Value;
      CUINullRect PlainLayout.IPlainLayoutElement.Relative => Host.CUIProps.Relative.Value;
    }

    private partial class ComponentAccess : ILayoutContainer
    {
      Layout ILayoutContainer.Layout => Host.Layout;
    }

    private partial class ComponentAccess : ILayoutContainerAccess
    {
      Layout ILayoutContainerAccess.GetLayout(CUIComponent host) => host.Layout;
    }

    private partial class ComponentAccess : LayoutMarker.IMarkableLayoutContainer
    {
      LayoutMarker.IMarkableLayoutContainer LayoutMarker.IMarkableLayoutContainer.Parent
        => Host.Parent?.Access;
      Layout LayoutMarker.IMarkableLayoutContainer.Layout => Host.Layout;

      IReadOnlyList<LayoutMarker.IMarkableLayoutContainer> LayoutMarker.IMarkableLayoutContainer.Children => new ListProxy<CUIComponent, LayoutMarker.IMarkableLayoutContainer>(Host.Children, child => child.Access);

      void LayoutMarker.IMarkableLayoutContainer.NotifyMainComponent()
      {
        Host.MainComponentTracker.MainComponent?.LayoutChanged();
      }
    }

    private partial class ComponentAccess : LayoutMarker.ILayoutMarkable
    {
      void LayoutMarker.ILayoutMarkable.Mark(LayoutMarker.Pattern pattern) => Host.LayoutMarker.Mark(pattern);
    }

    private partial class ComponentAccess : MainComponentTracker.IMainComponentTrackerContainer, MainComponentTracker.IMainComponentTrackersParent
    {
      MainComponentTracker MainComponentTracker.IMainComponentTrackerContainer.Tracker => Host.MainComponentTracker;

      IReadOnlyList<MainComponentTracker.IMainComponentTrackerContainer> MainComponentTracker.IMainComponentTrackersParent.Children => new ListProxy<CUIComponent, MainComponentTracker.IMainComponentTrackerContainer>(Host.Children, c => c.Access);


    }

  }
}