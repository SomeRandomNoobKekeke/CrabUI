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
namespace CrabUI
{
  public partial class CUIVisualComponent
  {
    public class IFocusableAdapter_Part : Part, IFocusable, IKeyboardEventConsumer
    {
      public bool Focused { get; set; }
      public bool ManuallyFocused { get; set; }
      public ClearableEvent OnFocus { get; } = new();
      public ClearableEvent OnFocusLost { get; } = new();

      public ClearableEvent<CUIKeyPressedEvent> KeyPressed { get; } = new();
      public ClearableEvent<CUIKeyReleasedEvent> KeyReleased { get; } = new();
      public ClearableEvent<CUITextInputEvent> TextInput { get; } = new();
      public ClearableEvent<CUIKeyDownInputEvent> KeyDownInput { get; } = new();

      public override string ToString() => $"Focusable part of [{Self}]";
    }
    protected IFocusableAdapter_Part IFocusableAdapter { get; } = new();


    public bool Focused => IFocusableAdapter.Focused;
    public bool Focusable { get; set; }
    public bool ManuallyFocused
    {
      get => IFocusableAdapter.ManuallyFocused;
      set => IFocusableAdapter.ManuallyFocused = value;
    }

    public void HandleFocusProbe(CUIFocusRequestEvent e)
    {
      if (!Focusable) return;

      if (e.Input.Mouse.M1.Down)
      {
        e.Accept(IFocusableAdapter);
      }
    }

    public void Focus() => CUICore.RequestFocus(IFocusableAdapter);
    public void Blur() => CUICore.RequestBlur(IFocusableAdapter);

    //TODO should these take this CUIVisualComponent as first arg?
    public Action AddOnFocus { set { OnFocus += value; } }
    public event Action OnFocus
    {
      add => IFocusableAdapter.OnFocus.Add(value);
      remove => IFocusableAdapter.OnFocus.Remove(value);
    }

    public Action AddOnFocusLost { set { OnFocusLost += value; } }
    public event Action OnFocusLost
    {
      add => IFocusableAdapter.OnFocusLost.Add(value);
      remove => IFocusableAdapter.OnFocusLost.Remove(value);
    }


    public Action<CUIKeyPressedEvent> OnKeyPressed { set { KeyPressed += value; } }
    public event Action<CUIKeyPressedEvent> KeyPressed
    {
      add => IFocusableAdapter.KeyPressed.Add(value);
      remove => IFocusableAdapter.KeyPressed.Remove(value);
    }

    public Action<CUIKeyReleasedEvent> OnKeyReleased { set { KeyReleased += value; } }
    public event Action<CUIKeyReleasedEvent> KeyReleased
    {
      add => IFocusableAdapter.KeyReleased.Add(value);
      remove => IFocusableAdapter.KeyReleased.Remove(value);
    }

    public Action<CUITextInputEvent> OnTextInput { set { TextInput += value; } }
    public event Action<CUITextInputEvent> TextInput
    {
      add => IFocusableAdapter.TextInput.Add(value);
      remove => IFocusableAdapter.TextInput.Remove(value);
    }

    public Action<CUIKeyDownInputEvent> OnKeyDownInput { set { KeyDownInput += value; } }
    public event Action<CUIKeyDownInputEvent> KeyDownInput
    {
      add => IFocusableAdapter.KeyDownInput.Add(value);
      remove => IFocusableAdapter.KeyDownInput.Remove(value);
    }
  }
}