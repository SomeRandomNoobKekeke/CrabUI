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
          Self.Debug_FocusedChanged.Send(_Focused);

          if (_Focused is not null)
          {
            _Focused.Focused = true;
            _Focused.OnFocus.Raise();
          }
        }
      }

      public IFocusable WantsToBeFocused { get; set; }

      //TODO should this be delayed and resolved at the end of the update?
      public void Blur()
      {
        Focused = null;
        Self.Handles.ClearFocus();
      }
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

        if (next is not null)
        {
          Focused = next;

          //TODO there are ton of hardcoded actions in barotrauma that are performed without focus
          // idk, i need a map of them to see how focus can possibly be resolved 
          Self.Handles.GrabFocus();
        }
        if (next is null && FocusShouldBeLost())
        {
          Blur();
        }

        WantsToBeFocused = null;
      }

      private bool FocusShouldBeLost()
      {
        if (Focused?.ManuallyFocused == true) return false;
        return Self._Input.Mouse.M1.Down;
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