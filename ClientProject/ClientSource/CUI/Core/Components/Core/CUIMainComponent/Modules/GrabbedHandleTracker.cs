using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CursedUI
{
  public class GrabbedHandleTracker : IModule
  {
    public DebugRelay DebugRelay { get; } = new();

    public object GrabbedHandle { get; private set; }
    public bool SomethingGrabbed => GrabbedHandle is not null;


    public bool TryGrab(object handle)
    {
      if (GrabbedHandle is not null)
      {
        return false;
      }
      else
      {
        GrabbedHandle = handle;
        return true;
      }
    }

    public void Release(object handle)
    {
      GrabbedHandle = null;
    }
  }
}