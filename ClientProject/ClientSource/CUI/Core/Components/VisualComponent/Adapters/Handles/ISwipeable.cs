using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUIVisualComponent
  {
    protected partial class Adapters_Part
    {
      public ISwipeable_Adapter ISwipeable { get; } = new();
      public partial class ISwipeable_Adapter : Part, IAdapterPart, ISwipeable
      {
        public void Init()
        {
          Self.MouseDown += (CUIMouseDownEvent e) => MouseDown?.Invoke(e);
        }
        public event Action<CUIMouseDownEvent> MouseDown;
        public CUIRect Rect => Self.OuterRect;
        public CUIRect? ParentRect => Self.Parent?.ChildrenRect;

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