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
    protected class Internals
    {
      public MainComponentTracker MainComponentTracker { get; } = new();
    }

    private Internals Internal { get; } = new();
  }
}