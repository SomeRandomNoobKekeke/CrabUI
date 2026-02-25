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


    private partial class ComponentAccess : Layout.Access
    {
      CUIRect Layout.Access.Rect
      {
        get => Host.CUIProps.Rect.Value;
        set => Host.CUIProps.Rect.Value = value;
      }

      IList Layout.Access.Children
        => new ListProxy<CUIComponent, ComponentAccess>(Host.Children, c => c.Access);
    }

    private partial class ComponentAccess : PlainLayout.IPlainLayoutHost
    {

    }

    private partial class ComponentAccess : PlainLayout.IPlainLayoutElement
    {
      CUIRect PlainLayout.IPlainLayoutElement.Rect
      {
        get => Host.CUIProps.Rect.Value;
        set => Host.CUIProps.Rect.Value = value;
      }
      CUINullRect PlainLayout.IPlainLayoutElement.Absolute => Host.CUIProps.Absolute.Value;
      CUINullRect PlainLayout.IPlainLayoutElement.Relative => Host.CUIProps.Relative.Value;
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
        Host.Internal.MainComponentTracker.MainComponent?.LayoutChanged();
      }
    }

    private partial class ComponentAccess : CUILayoutProp.Target
    {
      void CUILayoutProp.Target.Mark(LayoutMarker.Pattern pattern) => Host.LayoutMarker.Mark(pattern);
    }

    private partial class ComponentAccess : MainComponentTracker.Target
    {
      MainComponentTracker MainComponentTracker.Target.Tracker => Host.Internal.MainComponentTracker;

      IReadOnlyList<MainComponentTracker.Target> MainComponentTracker.Target.Children
        => new ListProxy<CUIComponent, MainComponentTracker.Target>(Host.Children, c => c.Access);
    }

  }
}