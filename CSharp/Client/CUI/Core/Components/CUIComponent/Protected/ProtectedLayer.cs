using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected class ProtectedLayer
    {
      public MainComponentTracker MainComponentTracker { get; } = new();

      public void NotifyMainComponent()
      {
        MainComponentTracker.MainComponent?.LayoutChanged();
      }

      public CUIComponent Host { get; }
      public ProtectedLayer(CUIComponent host) => Host = host;
    }

    protected ProtectedLayer Protected { get; }
  }
}