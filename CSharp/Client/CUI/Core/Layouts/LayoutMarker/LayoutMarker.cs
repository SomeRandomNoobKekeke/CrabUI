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
    public interface Target
    {
      public Target Parent { get; }
      public Layout Layout { get; }
      public IReadOnlyList<Target> Children { get; }
      public void NotifyMainComponent();// BRUH temporary
    }







    public Target Host { get; set; }

    public void Mark(Pattern pattern)
    {
      if (pattern.Empty) return;
      pattern.MarkFunc(Host);
      Host.NotifyMainComponent();
    }
  }
}