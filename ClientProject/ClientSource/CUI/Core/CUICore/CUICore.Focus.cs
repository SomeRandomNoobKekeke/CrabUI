using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;
using Microsoft.Xna.Framework;
namespace CursedUI
{

  public partial class CUICore
  {
    public class FocusHandle_Part : Part
    {
      public void Init()
      {
        Debug_Focus = new(DebugCategory.Focus, Self._DebugHub)
        {
          IsOpen = true,
          MsgFactory = (s) => s,
        };
      }
      public DebugNode<string> Debug_Focus { get; private set; }

      private IFocusable _Focused; public IFocusable Focused
      {
        get => _Focused;
        private set
        {
          if (_Focused == value) return;

          if (_Focused != null) _Focused.Focused = false;
          Debug_Focus.Send($"Focus changed [{_Focused}] -> [{value}]");
          _Focused = value;
          if (_Focused != null) _Focused.Focused = true;
        }
      }

      public List<IFocusable> RequestedFocus { get; } = new();
      public HashSet<IFocusable> RequestedBlur { get; } = new();

      public void RequestFocus(IFocusable focusable)
      {
        RequestedFocus.Add(focusable);
        Debug_Focus.Send($"[{focusable}] requested focus");
      }
      public void RequestBlur(IFocusable focusable)
      {
        RequestedBlur.Add(focusable);
        Debug_Focus.Send($"[{focusable}] requested blur");
      }

      public void Reset()
      {
        if (RequestedFocus.Count > 0) Debug_Focus.Send($"RequestedFocus reset");
        if (RequestedBlur.Count > 0) Debug_Focus.Send($"RequestedBlur reset");

        RequestedFocus.Clear();
        RequestedBlur.Clear();
      }

      public void ResolveFocus()
      {
        IFocusable newFocused = RequestedFocus.LastOrDefault();

        if (newFocused != null)
        {
          Focused = newFocused;
          Self.Handles.GrabFocus(Focused);
          return;
        }

        if (RequestedBlur.Contains(Focused))
        {
          Focused = null;
          Self.Handles.GrabFocus(Focused);
          return;
        }

        if (Self._Input.Mouse.M1.Down)
        {
          Focused = null;
          Self.Handles.GrabFocus(Focused);
        }
      }

      public void ClearFocus()
      {
        Focused = null;
        Debug_Focus.Send($"Focus Cleared");
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