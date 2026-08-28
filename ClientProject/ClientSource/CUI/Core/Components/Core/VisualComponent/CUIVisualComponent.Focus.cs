using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
namespace CursedUI
{
  public partial class CUIVisualComponent
  {
    public class FocusHandle_Part : Part, IFocusable, IKeyboardEventConsumer
    {
      public void Init()
      {
        Self.MouseDown += (e) =>
        {
          if (Self.Focusable && e.Mouse.M1.Down)
          {
            Focus();
          }
        };
      }

      public bool Focused
      {
        get => Self.Focused;
        set => Self.Focused = value;
      }

      public void Blur() => Self.MainComponent?.FocusHandle.RequestBlur(this);
      public void Focus() => Self.MainComponent?.FocusHandle.RequestFocus(this);

      public ClearableEvent<CUIKeyPressedEvent> KeyPressed { get; } = new();
      public ClearableEvent<CUIKeyReleasedEvent> KeyReleased { get; } = new();
      public ClearableEvent<CUITextInputEvent> TextInput { get; } = new();
      public ClearableEvent<CUIKeyDownInputEvent> KeyDownInput { get; } = new();

      public override string ToString() => $"{Self} Focus Handle";
    }

    protected FocusHandle_Part FocusHandle { get; } = new();

    public bool Focusable { get; set; }
    private bool _Focused; public bool Focused
    {
      get => _Focused;
      set
      {
        bool wasFocused = _Focused;
        _Focused = value;

        if (!wasFocused && _Focused) OnFocus?.Invoke();
        if (wasFocused && !_Focused) OnBlur?.Invoke();
      }
    }

    public event Action OnFocus;
    public event Action OnBlur;

    public void Blur() => FocusHandle.Blur();
    public void Focus() => FocusHandle.Focus();



    public Action<CUIKeyPressedEvent> OnKeyPressed { set { KeyPressed += value; } }
    public event Action<CUIKeyPressedEvent> KeyPressed
    {
      add => FocusHandle.KeyPressed.Add(value);
      remove => FocusHandle.KeyPressed.Remove(value);
    }

    public Action<CUIKeyReleasedEvent> OnKeyReleased { set { KeyReleased += value; } }
    public event Action<CUIKeyReleasedEvent> KeyReleased
    {
      add => FocusHandle.KeyReleased.Add(value);
      remove => FocusHandle.KeyReleased.Remove(value);
    }

    public Action<CUITextInputEvent> OnTextInput { set { TextInput += value; } }
    public event Action<CUITextInputEvent> TextInput
    {
      add => FocusHandle.TextInput.Add(value);
      remove => FocusHandle.TextInput.Remove(value);
    }

    public Action<CUIKeyDownInputEvent> OnKeyDownInput { set { KeyDownInput += value; } }
    public event Action<CUIKeyDownInputEvent> KeyDownInput
    {
      add => FocusHandle.KeyDownInput.Add(value);
      remove => FocusHandle.KeyDownInput.Remove(value);
    }
  }
}