using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using CUICodeGenerator;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public partial class CUICore
  {
    public class GlobalFocusTracker_Part : Part
    {
      private IFocusable? _Focused; public IFocusable? Focused
      {
        get => _Focused;
        private set
        {
          if (_Focused == value) return;

          if (_Focused is not null)
          {
            _Focused.Focused = false;
            _Focused.OnFocusLost.Raise();

          }

          _Focused = value;

          if (_Focused is not null)
          {
            _Focused.Focused = true;
            _Focused.OnFocus.Raise();
          }
        }
      }

      public IFocusable WantsToBeFocused { get; set; }

      public void ResolveFocus(bool SomethingFocusedElsewhere)
      {
        if (SomethingFocusedElsewhere)
        {
          Focused = null;
          return;
        }

        IFocusable next = WantsToBeFocused;
        next ??= Self.Main.WantsToBeFocused;
        next ??= Self.TopMain.WantsToBeFocused;

        if (next is not null) Focused = next; //TODO and GrabFocus()?
        if (next is null && FocusShouldBeLost()) Focused = null;

        WantsToBeFocused = null;
      }

      private bool FocusShouldBeLost()
      {
        return Self._Input.Mouse.M1.Down;
      }
    }

    public GlobalFocusTracker_Part GlobalFocusTracker { get; } = new();
  }

}