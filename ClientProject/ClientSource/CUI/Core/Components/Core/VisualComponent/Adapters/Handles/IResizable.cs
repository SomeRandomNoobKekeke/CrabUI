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
      public IResizable_Adapter IResizable { get; } = new();
      public partial class IResizable_Adapter : Part, IAdapterPart, IResizable
      {
        public void Init()
        {
          Self.MouseDown += (CUIMouseDownEvent e) => MouseDown?.Invoke(e);
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



        public void ResizeToAbsoluteRect(CUIRect rect, CUIBool2 preventMovement)
        {
          if (Self.Parent.ChildrenBounds != null)
          {
            rect = Self.Parent.ChildrenBounds(Self.Parent.ChildrenRect).FitResizingRect(Rect, rect);
          }

          CUIRect anchoredRect = CUIAnchor.AbsoluteRectToAchored(
            rect, Self.Parent.ChildrenRect, Self.Anchor
          );

          if (preventMovement.X)
          {
            if (preventMovement.Y)
            {
              return;
            }
            else
            {
              if (Self.ResizeRelative)
              {
                Self.Relative = Self.Relative with
                {
                  Top = anchoredRect.Top / Self.Parent.ChildrenRect.Height,
                  Height = anchoredRect.Height / Self.Parent.ChildrenRect.Height,
                };
              }
              else
              {
                Self.Absolute = Self.Absolute with
                {
                  Top = anchoredRect.Top,
                  Height = anchoredRect.Height,
                };
              }
            }
          }
          else
          {
            if (preventMovement.Y)
            {
              if (Self.ResizeRelative)
              {
                Self.Relative = Self.Relative with
                {
                  Left = anchoredRect.Left / Self.Parent.ChildrenRect.Width,
                  Width = anchoredRect.Width / Self.Parent.ChildrenRect.Width,
                };
              }
              else
              {
                Self.Absolute = Self.Absolute with
                {
                  Left = anchoredRect.Left,
                  Width = anchoredRect.Width,
                };
              }
            }
            else
            {
              if (Self.ResizeRelative)
              {
                Self.Relative = Self.Relative with
                {
                  Left = anchoredRect.Left / Self.Parent.ChildrenRect.Width,
                  Top = anchoredRect.Top / Self.Parent.ChildrenRect.Height,
                  Width = anchoredRect.Width / Self.Parent.ChildrenRect.Width,
                  Height = anchoredRect.Height / Self.Parent.ChildrenRect.Height,
                };
              }
              else
              {
                Self.Absolute = Self.Absolute with
                {
                  Left = anchoredRect.Left,
                  Top = anchoredRect.Top,
                  Width = anchoredRect.Width,
                  Height = anchoredRect.Height,
                };
              }
            }
          }

          Self.Events.Resized.Raise(anchoredRect);
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