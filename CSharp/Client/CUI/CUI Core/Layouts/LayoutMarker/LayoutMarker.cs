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
  public class LayoutMarker
  {
    private TreeAdapter<ILayoutHost> Host;

    public void Mark(LayoutMarkPattern pattern)
    {
      if (pattern.Empty) return;
      pattern.MarkFunc.Invoke();
    }

    public LayoutMarker(ITreeNode host)
    {
      Host = new TreeAdapter<ILayoutHost>(host);
    }
  }
}