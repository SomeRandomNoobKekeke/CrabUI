using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    public FocusHandle FocusHandle { get; } = new();

    public bool Focused
    {
      get => FocusHandle.Focused;
      set => FocusHandle.Focused = value;
    }

    public bool Focusable
    {
      get => FocusHandle.Focusable;
      set => FocusHandle.Focusable = value;
    }

    //TODO should these take this CUIComponent as first arg?
    public Action AddOnFocus { set { OnFocus += value; } }
    public event Action OnFocus
    {
      add => FocusHandle.OnFocus.Add(value);
      remove => FocusHandle.OnFocus.Remove(value);
    }

    public Action AddOnFocusLost { set { OnFocusLost += value; } }
    public event Action OnFocusLost
    {
      add => FocusHandle.OnFocusLost.Add(value);
      remove => FocusHandle.OnFocusLost.Remove(value);
    }
  }
}