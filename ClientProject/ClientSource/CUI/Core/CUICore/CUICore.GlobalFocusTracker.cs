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
      public bool FocusShouldBeLost { get; set; }
      public IFocusable ShouldBeFocused { get; set; }


      private IFocusable? _FocusedComponent;
      public IFocusable? FocusedComponent
      {
        get => _FocusedComponent;
        private set
        {
          if (_FocusedComponent == value) return;

          if (_FocusedComponent != null) _FocusedComponent.Focused = false;
          _FocusedComponent = value;
          if (_FocusedComponent != null) _FocusedComponent.Focused = true;
        }
      }

      public void ResolveFocus(bool SomethingFocusedElsewhere)
      {
        if (SomethingFocusedElsewhere)
        {
          FocusedComponent = null;
          return;
        }

        if (
          Self.Main.FocusTracker.FocusShouldBeLost &&
          Self.TopMain.FocusTracker.FocusShouldBeLost
        )
        {
          FocusedComponent = null;
          return;
        }

        if (
          Self.Main.FocusTracker.ShouldBeFocused is null &&
          Self.TopMain.FocusTracker.ShouldBeFocused is null
        )
        {
          return;
        }

        IFocusable? next = null;
        next ??= Self.Main.FocusTracker.ShouldBeFocused;
        next ??= Self.TopMain.FocusTracker.ShouldBeFocused;

        FocusedComponent = next;
      }
    }


    public GlobalFocusTracker_Part GlobalFocusTracker { get; } = new();
  }

}