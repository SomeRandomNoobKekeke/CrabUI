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
  public partial class CUIVisualComponent : IFocusable
  {
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

    public void Blur()
    {
      MainComponent?.FocusHandle.RequestBlur(this);
    }

    public void Focus()
    {
      MainComponent?.FocusHandle.RequestFocus(this);
    }
  }
}