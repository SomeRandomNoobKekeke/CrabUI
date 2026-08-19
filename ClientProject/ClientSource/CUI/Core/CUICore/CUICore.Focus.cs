using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;
using Microsoft.Xna.Framework;
namespace CrabUI
{

  public partial class CUICore
  {
    public class FocusHandle_Part : Part
    {
      private IFocusable _Focused; public IFocusable Focused
      {
        get => _Focused;
        private set
        {
          if (_Focused == value) return;

          if (_Focused != null) _Focused.Focused = false;
          _Focused = value;
          if (_Focused != null) _Focused.Focused = true;
        }
      }

      public List<IFocusable> RequestedFocus { get; } = new();
      public HashSet<IFocusable> RequestedBlur { get; } = new();

      public void RequestFocus(IFocusable focusable)
      {
        RequestedFocus.Add(focusable);
      }
      public void RequestBlur(IFocusable focusable)
      {
        RequestedBlur.Add(focusable);
      }

      public void Reset()
      {
        RequestedFocus.Clear();
        RequestedBlur.Clear();
      }

      public void ResolveFocus()
      {
        IFocusable newFocused = RequestedFocus.LastOrDefault();

        if (newFocused != null)
        {
          Focused = newFocused;
          return;
        }

        if (RequestedBlur.Contains(Focused))
        {
          Focused = null;
          return;
        }

        if (Self._Input.Mouse.M1.Down)
        {
          Focused = null;
        }
      }

      public void ClearFocus()
      {
        Focused = null;
      }

      private EventDispatcher EventDispatcher { get; } = new();
      public void DispatchKeyboadEvents()
      {
        if (Focused is null) return;
        // if (CUICore.InputBlockingMenuOpen) return;
        EventDispatcher.Dispatch(Focused, Self._EventConstructor.KeyboardEvents);
      }
    }

    private FocusHandle_Part FocusHandle { get; } = new();
  }
}