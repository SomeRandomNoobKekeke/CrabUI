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

namespace CursedUI
{
  public partial class CUIVisualComponent
  {
    protected partial class Adapters_Part
    {
      public IDraggable_Adapter IDraggable { get; } = new();
      public partial class IDraggable_Adapter : Part, IAdapterPart, IDraggable
      {
        public void Init()
        {
          Self.MouseDown += (CUIMouseDownEvent e) => MouseDown?.Invoke(e);
        }

        public event Action<CUIMouseDownEvent> MouseDown;
        public CUIRect Rect => Self.OuterRect;
        public CUIRect? ParentRect => Self.Parent?.ChildrenRect;

        // Note: DragHandle doesn't know and doesn't care about anchors
        public void SetLeftTopPos(float x, float y)
        {
          CUIRect parentRect = ParentRect ?? CUIRect.Zero;


          CUIRect rect = new CUIRect(x, y, Rect.Width, Rect.Height);

          if (Self.Parent.ChildrenBounds != null)
          {
            rect = Self.Parent.ChildrenBounds(Self.Parent.ChildrenRect).FitMovingRect(Rect, rect);
          }

          Vector2 offset = CUIAnchor.GetOffset(
            parentRect,
            Self.ParentAnchor ?? Self.Anchor,
            rect,
            Self.Anchor
          );

          //TODO add Relative drag
          if (Self.DragRelative)
          {
            Self.Relative = Self.Relative with
            {
              Left = offset.X / parentRect.Width,
              Top = offset.Y / parentRect.Height,
            };

            Self.Events.Dragged.Raise(Self.Relative.Position);
          }
          else
          {
            Self.Absolute = Self.Absolute with
            {
              Left = offset.X,
              Top = offset.Y,
            };

            Self.Events.Dragged.Raise(Self.Absolute.Position);
          }


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
        {
          bool result = Self.MainComponentTracker.MainComponent?.GrabbedHandleTracker.TryGrab(handle) ?? false;
          Self.Events.DragStarted.Raise(Self.DragRelative ? Self.Relative.Position : Self.Absolute.Position);
          return result;
        }

        public void Release(object handle)
        {
          //TODO this feels awkward, drag handle doesn't pass coordinates here, mb i should make separate events
          Self.MainComponentTracker.MainComponent?.GrabbedHandleTracker.Release(handle);
          Self.Events.DragEnded.Raise(Self.DragRelative ? Self.Relative.Position : Self.Absolute.Position);
        }
      }
    }
  }
}