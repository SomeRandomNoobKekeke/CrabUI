using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected partial class Adapters_Part
    {
      public IResizable_Adapter IResizable { get; } = new();
      public partial class IResizable_Adapter : Part, IAdapterPart, IResizable
      {
        public void Init()
        {
          Self.MouseDown += (CUIComponent c, CUIMouseDownEvent e) => MouseDown?.Invoke(e);
        }

        public CUIRect Rect => Self.Rect;

        public void SetSize(Vector2 size)
        {
          Self.LayoutProps.Absolute.Value = Self.LayoutProps.Absolute.Value with
          {
            Size = size,
          };
        }

        public event Action<CUIMouseDownEvent> MouseDown;

        public event Action<CUIMouseUpEvent> HubMouseUp
        {
          add => Self.MainComponentTracker.MainComponent?.GlobalEvents.MouseUp.Add(value);
          remove => Self.MainComponentTracker.MainComponent?.GlobalEvents.MouseUp.Remove(value);
        }
        public event Action<CUIMouseMovedEvent> HubMouseMoved
        {
          add => Self.MainComponentTracker.MainComponent?.GlobalEvents.MouseMoved.Add(value);
          remove => Self.MainComponentTracker.MainComponent?.GlobalEvents.MouseMoved.Remove(value);
        }

        public bool TryGrab(object handle)
         => Self.MainComponentTracker.MainComponent.GrabbedHandleTracker.TryGrab(handle);
        public void Release(object handle)
          => Self.MainComponentTracker.MainComponent.GrabbedHandleTracker.Release(handle);
      }
    }
  }
}