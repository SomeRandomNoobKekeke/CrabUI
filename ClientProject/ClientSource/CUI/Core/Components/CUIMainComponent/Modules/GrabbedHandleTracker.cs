using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CrabUI
{
  public class GrabbedHandleTracker : IModule
  {
    public DebugRelay DebugRelay { get; } = new();

    public DebugNode<object> Debug_HandleGrabbed { get; } = new(
      DebugCategory.HandleGrab, CUI.DebugHub,
      (handle) => $"{handle.GetType().Name} on {(handle as IAware).HostComponent} Grabbed"
    );

    public DebugNode<object> Debug_HandleReleased { get; } = new(
      DebugCategory.HandleGrab, CUI.DebugHub,
      (handle) => $"{handle.GetType().Name} on {(handle as IAware).HostComponent} Released"
    );

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
        Debug_HandleGrabbed.Send(handle);
        return true;
      }
    }

    public void Release(object handle)
    {
      GrabbedHandle = null;
      Debug_HandleReleased.Send(handle);
    }

    public GrabbedHandleTracker()
    {
      DebugRelay.Route(Debug_HandleGrabbed);
      DebugRelay.Route(Debug_HandleReleased);
    }
  }
}