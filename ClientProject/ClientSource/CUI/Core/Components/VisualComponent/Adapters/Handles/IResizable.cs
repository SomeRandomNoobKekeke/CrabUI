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
      public IResizable_Adapter IResizable { get; } = new();
      public partial class IResizable_Adapter : Part, IAdapterPart, IResizable
      {
        public void Init()
        {
          Self.MouseDown += (CUIVisualComponent c, CUIMouseDownEvent e) => MouseDown?.Invoke(e);
        }

        public CUIRect Rect => Self.OuterRect;

        public Vector2 MinSize
        {
          get
          {
            float w = 0;
            if (Self.RelativeMin.Width.HasValue) w = Math.Max(w, Self.RelativeMin.Width.Value * Self.Parent.ChildrenRect.Width);
            if (Self.AbsoluteMin.Width.HasValue) w = Math.Max(w, Self.AbsoluteMin.Width.Value);

            float h = 0;
            if (Self.RelativeMin.Height.HasValue) h = Math.Max(h, Self.RelativeMin.Height.Value * Self.Parent.ChildrenRect.Height);
            if (Self.AbsoluteMin.Height.HasValue) h = Math.Max(h, Self.AbsoluteMin.Height.Value);

            return new Vector2(w, h);
          }
        }
        // Vector2 IResizable.MaxSize //TODO



        public void ResizeToAbsoluteRect(CUIRect rect)
        {
          //TODO It's better but still kinda funny, you can smash frame out of bounds
          if (Self.Parent.ChildrenBounds != null)
          {
            rect = Self.Parent.ChildrenBounds(Self.Parent.ChildrenRect).FitGracefuly(Rect, rect);
          }

          Self.Absolute = new CUINullRect(
            CUIAnchor.AbsoluteRectToAchored(
              rect, Self.Parent.ChildrenRect, Self.Anchor
            )
          );
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