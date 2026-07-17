using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;

namespace CrabUI
{
  public class VisualBounds : IAware
  {
    public object HostComponent
    {
      get => LeftBound.HostComponent;
      set
      {
        LeftBound.HostComponent = value;
        RightBound.HostComponent = value;
      }
    }
    public string HostPropName
    {
      get => LeftBound.HostPropName;
      set
      {
        LeftBound.HostPropName = value;
        RightBound.HostPropName = value;
      }
    }

    public class LeftContextBound : VisualUnit
    {
      public override object HostComponent { get; set; }
      public override string HostPropName { get; set; }

      public LeftContextBound(VisualBounds bounds) => Bounds = bounds;
      public VisualBounds Bounds { get; }
    }


    public class RightContextBound : VisualUnit
    {
      public override object HostComponent { get; set; }
      public override string HostPropName { get; set; }
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