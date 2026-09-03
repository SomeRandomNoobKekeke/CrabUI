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
  public class VisualBounds : IAware
  {
    public class LeftContextBound : VisualUnit, IAware
    {
      public object HostComponent { get; set; }
      public string HostPropName { get; set; }

      public LeftContextBound(VisualBounds bounds) => Bounds = bounds;
      public VisualBounds Bounds { get; }

      public override string ToString() => $"{HostComponent} vvv Left Visual Bound";
    }


    public class RightContextBound : VisualUnit, IAware
    {
      public object HostComponent { get; set; }
      public string HostPropName { get; set; }

      public RightContextBound(VisualBounds bounds) => Bounds = bounds;
      public VisualBounds Bounds { get; }
      public override string ToString() => $"{HostComponent} ^^^ Right Visual Bound";
    }

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

    public VisualBounds()
    {
      LeftBound = new LeftContextBound(this);
      RightBound = new RightContextBound(this);
    }

    public LeftContextBound LeftBound { get; }
    public RightContextBound RightBound { get; }

    public SamplerState? SamplerState { get; set; }
    public Rectangle? ScissorRect { get; set; }


    public override string ToString() => $"{HostComponent} Visual Bounds";
  }


}