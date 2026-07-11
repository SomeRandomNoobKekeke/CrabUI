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
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected partial class Adapters_Part
    {
      public IDraggable_Adapter IDraggable { get; } = new();
      public partial class IDraggable_Adapter : Part, IAdapterPart, IDraggable
      {
        public void Init()
        {
          Self.MouseDown += (CUIComponent c, CUIMouseDownEvent e) => MouseDown?.Invoke(e);
        }

        public event Action<CUIMouseDownEvent> MouseDown;
        public CUIRect Rect => Self.Rect;
        public CUIRect? ParentRect => Self.Parent?.Rect;

        // Note: DragHandle doesn't know and doesn't care about anchors
        public void SetLeftTopPos(float x, float y)
        {
          CUIRect parentRect = ParentRect ?? CUIRect.Zero;

          Vector2 offset = CUIAnchor.GetOffset(
            parentRect,
            Self.LayoutProps.ParentAnchor.Value ?? Self.LayoutProps.Anchor.Value,
            new CUIRect(x, y, Rect.Width, Rect.Height),
            Self.LayoutProps.Anchor.Value
          );

          //TODO add Relative drag
          Self.LayoutProps.Absolute.Value = Self.LayoutProps.Absolute.Value with
          {
            Left = parentRect.Left + offset.X,
            Top = parentRect.Top + offset.Y,
          };

          Self.Events.Dragged.Raise(Self, new Vector2(x, y));
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
          => Self.MainComponentTracker.MainComponent?.GrabbedHandleTracker.TryGrab(handle) ?? false;

        //TODO How to release grab handle when component is detached?
        public void Release(object handle)
          => Self.MainComponentTracker.MainComponent?.GrabbedHandleTracker.Release(handle);
      }
    }
  }
}