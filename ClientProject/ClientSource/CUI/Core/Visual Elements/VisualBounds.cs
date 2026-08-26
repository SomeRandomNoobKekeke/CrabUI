using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;

namespace CursedUI
{
  public class VisualBounds
  {
    public class LeftContextBound : VisualUnit
    {
      public LeftContextBound(VisualBounds bounds) => Bounds = bounds;
      public VisualBounds Bounds { get; }
    }


    public class RightContextBound : VisualUnit
    {
      public RightContextBound(VisualBounds bounds) => Bounds = bounds;
      public VisualBounds Bounds { get; }
    }

    public VisualBounds()
    {
      LeftBound = new LeftContextBound(this);
      RightBound = new RightContextBound(this);
    }

    public LeftContextBound LeftBound { get; }
    public RightContextBound RightBound { get; }

    public SamplerState? SamplerState { get; set; }
    public Rectangle? ScissorRect { get; set; }
  }


}