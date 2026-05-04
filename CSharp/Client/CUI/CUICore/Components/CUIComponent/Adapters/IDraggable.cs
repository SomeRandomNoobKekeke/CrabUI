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