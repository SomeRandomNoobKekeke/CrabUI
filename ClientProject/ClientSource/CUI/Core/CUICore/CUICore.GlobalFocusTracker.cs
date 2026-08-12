using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public partial class CUICore
  {
    public GlobalFocusTracker_Part GlobalFocusTracker { get; } = new();
    public class GlobalFocusTracker_Part : Part
    {
      private EventDispatcher EventDispatcher { get; } = new();

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

      private IFocusable _WantsToBeFocused; public IFocusable WantsToBeFocused
      {
        get => _WantsToBeFocused;
        set
        {
          _WantsToBeFocused = value;
        }
      }
      public HashSet<IFocusable> WantsToBeBlured { get; } = new();

      public void AddToWantsToBeBlured(IFocusable focusable)
      {
        WantsToBeBlured.Add(focusable);
      }

      public void ResolveFocus(bool SomethingFocusedElsewhere)
      {
        if (SomethingFocusedElsewhere)
        {
          Focused = null;
          return;
        }

        IFocusable next = WantsToBeFocused;
        next ??= Self.TopMain.WantsToBeFocused;
        next ??= Self.Main.WantsToBeFocused;


        if (next is not null)
        {
          Focused = next;
          Self.Handles.GrabFocus();
        }


        if (WantsToBeBlured.Contains(Focused))
        {
          Focused = null;
          Self.Handles.ClearFocus();
        }

        if (next is null && Self._Input.Mouse.M1.Down)
        {
          Focused = null;
          Self.Handles.ClearFocus();
        }

        _WantsToBeFocused = null;
        Self.Main.WantsToBeFocused = null;
        Self.TopMain.WantsToBeFocused = null;
        WantsToBeBlured.Clear();
      }


      public void DispatchKeyboadEvents()
      {
        if (Focused is null) return;
        if (CUICore.InputBlockingMenuOpen) return;

        EventDispatcher.Dispatch(Focused, Self.EventConstructor.KeyboardEvents);
      }
    }


  }

}