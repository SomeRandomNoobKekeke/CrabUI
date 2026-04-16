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
using ComponentInjector;

namespace CrabUI
{
  public partial class CUIComponent
  {
    public partial class Access_Part : Part
    {
      public IDragHandleHub_Access IDragHandleHub { get; } = new();
      public partial class IDragHandleHub_Access : Part, IAccess, IDragHandleHub
      {
        public event Action<CUIMouseUpEvent> MouseUp
        {
          add => Self.MainComponentTracker.MainComponent?.GlobalEvents.MouseUp.Add(value);
          remove => Self.MainComponentTracker.MainComponent?.GlobalEvents.MouseUp.Remove(value);
        }
        public event Action<CUIMouseMovedEvent> MouseMoved
        {
          add => Self.MainComponentTracker.MainComponent?.GlobalEvents.MouseMoved.Add(value);
          remove => Self.MainComponentTracker.MainComponent?.GlobalEvents.MouseMoved.Remove(value);
        }
      }


      public IDraggable_Access IDraggable { get; } = new();
      public partial class IDraggable_Access : Part, IAccess, IDraggable
      {
        public event Action<CUIMouseDownEvent> MouseDown
        {
          add => Self.Events.MouseDown.Add(value);
          remove => Self.Events.MouseDown.Remove(value);
        }
        public CUIRect Rect => Self.FunnyProps.Rect.Value;
        public CUIRect? ParentRect => Self.Parent?.FunnyProps.Rect.Value;
        public void SetAbsolutePos(float x, float y)
        {
          Self.LayoutProps.Absolute.Value = Self.LayoutProps.Absolute.Value with
          {
            Left = x,
            Top = y,
          };
        }
      }

    }
  }
}