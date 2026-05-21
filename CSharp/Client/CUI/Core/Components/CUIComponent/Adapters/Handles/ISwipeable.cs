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
      public ISwipeable_Adapter ISwipeable { get; } = new();
      public partial class ISwipeable_Adapter : Part, IAdapterPart, ISwipeable
      {

        public event Action<CUIMouseDownEvent> MouseDown
        {
          add => Self.Events.MouseDown.Add(value);
          remove => Self.Events.MouseDown.Remove(value);
        }
        public CUIRect Rect => Self.Rect;
        public CUIRect? ParentRect => Self.Parent?.Rect;

        public void MoveChildrenOffset(Vector2 offset)
        {
          Self.LayoutProps.ChildrenOffset.Value += offset;
        }


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