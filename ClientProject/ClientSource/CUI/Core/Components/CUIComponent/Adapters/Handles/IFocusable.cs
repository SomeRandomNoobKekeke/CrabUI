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
      public IFocusable_Adapter IFocusable { get; } = new();
      public partial class IFocusable_Adapter : Part, IAdapterPart, IFocusable
      {
        public bool Focused
        {
          get => Self.FocusHandle.Focused;
          set => Self.FocusHandle.Focused = value;
        }

        public void Init()
        {
          Self.MouseDown += (CUIComponent c, CUIMouseDownEvent e) => MouseDown?.Invoke(e);
        }

        public event Action<CUIMouseDownEvent> MouseDown;

        public void RequestFocus()
        {
          Self.MainComponentTracker.MainComponent?.FocusTracker.RequestFocus(this);
        }
      }
    }
  }
}