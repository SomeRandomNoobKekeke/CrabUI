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
      public IDraggable_Adapter IDraggable { get; } = new();
      public partial class IDraggable_Adapter : Part, IAdapterPart, IDraggable
      {
        public event Action<CUIMouseDownEvent> MouseDown
        {
          add => Self.Events.MouseDown.Add(value);
          remove => Self.Events.MouseDown.Remove(value);
        }
        public CUIRect Rect => Self.FunnyProps.Rect.Value;
        public CUIRect? ParentRect => Self.Parent?.FunnyProps.Rect.Value;

        // Note: DragHandle doesn't know and doesn't care about anchors
        public void SetLeftTopPos(float x, float y)
        {
          Vector2 offset = CUIAnchor.GetOffset(
            ParentRect ?? CUIRect.Zero,
            Self.LayoutProps.ParentAnchor.Value ?? Self.LayoutProps.Anchor.Value,
            new CUIRect(x, y, Rect.Width, Rect.Height),
            Self.LayoutProps.Anchor.Value
          );

          //TODO add Relative drag
          Self.LayoutProps.Absolute.Value = Self.LayoutProps.Absolute.Value with
          {
            Left = offset.X,
            Top = offset.Y,
          };
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
      }
    }
  }
}