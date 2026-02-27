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
    protected ProtectedLayerAccess ProtectedAccess { get; }
    protected partial class ProtectedLayerAccess
    {
      private CUIComponent Host;
      public ProtectedLayerAccess(CUIComponent host) => Host = host;
      public override string ToString() => Host.ToString();
    }



    protected partial class ProtectedLayerAccess : Layout.Target
    {
      CUIRect Layout.Target.Rect
      {
        get => Host.CUIProps.Rect.Value;
        set => Host.CUIProps.Rect.Value = value;
      }

      IList Layout.Target.Children
        => new ListProxy<CUIComponent, ProtectedLayerAccess>(Host.Tree.Children, c => c.ProtectedAccess);
    }

    protected partial class ProtectedLayerAccess : PlainLayout.Target
    {
      CUINullRect PlainLayout.Target.Absolute => Host.CUIProps.Absolute.Value;
      CUINullRect PlainLayout.Target.Relative => Host.CUIProps.Relative.Value;
      IReadOnlyList<PlainLayout.Target> PlainLayout.Target.Children
        => new ListProxy<CUIComponent, PlainLayout.Target>(Host.Tree.Children, c => c.ProtectedAccess);
    }


    protected partial class ProtectedLayerAccess : LayoutMarker.Target
    {
      LayoutMarker.Target LayoutMarker.Target.Parent => Host.Parent?.ProtectedAccess;
      Layout LayoutMarker.Target.Layout => Host.Layout;

      IReadOnlyList<LayoutMarker.Target> LayoutMarker.Target.Children
        => new ListProxy<CUIComponent, LayoutMarker.Target>(Host.Tree.Children, child => child.ProtectedAccess);

      void LayoutMarker.Target.NotifyMainComponent() => Host.NotifyMainComponent();
    }

    protected partial class ProtectedLayerAccess : CUILayoutProp.Target
    {
      void CUILayoutProp.Target.Mark(LayoutMarker.Pattern pattern) => Host.LayoutMarker.Mark(pattern);
    }

    protected partial class ProtectedLayerAccess : MainComponentTracker.Target
    {
      MainComponentTracker MainComponentTracker.Target.Tracker => Host.MainComponentTracker;

      IReadOnlyList<MainComponentTracker.Target> MainComponentTracker.Target.Children
        => new ListProxy<CUIComponent, MainComponentTracker.Target>(Host.Tree.Children, c => c.ProtectedAccess);
    }

  }
}