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
  public partial class LayoutMarker
  {
    public interface IMarkableLayoutContainer
    {
      public IMarkableLayoutContainer Parent { get; }
      public Layout Layout { get; }
      public IReadOnlyList<IMarkableLayoutContainer> Children { get; }
      public void NotifyMainComponent();// BRUH temporary
    }

    public interface ILayoutMarkable
    {
      public void Mark(Pattern pattern);
    }



    public IMarkableLayoutContainer Host { get; set; }

    public void Mark(Pattern pattern)
    {
      if (pattern.Empty) return;
      pattern.MarkFunc(Host);
      Host.NotifyMainComponent();
    }
  }
}