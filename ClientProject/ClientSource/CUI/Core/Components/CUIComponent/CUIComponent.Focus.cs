using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    public class IFocusableAdapter_Part : Part, IFocusable
    {
      public bool Focused { get; set; }
      public ClearableEvent OnFocus { get; } = new();
      public ClearableEvent OnFocusLost { get; } = new();
    }
    protected IFocusableAdapter_Part IFocusableAdapter { get; } = new();

    [InitMethod]
    public void InitFocusStuff()
    {
      Background.FocusProbed.Add(HandleFocusProbe);
    }

    public bool ConsumeFocus
    {
      get => Background.ConsumeFocus;
      set => Background.ConsumeFocus = value;
    }
    public bool Focused => IFocusableAdapter.Focused;
    public bool Focusable { get; set; }

    public void HandleFocusProbe(CUIFocusRequestEvent e)
    {
      if (!Focusable) return;

      if (e.Input.Mouse.M1.Down)
      {
        e.Accept(IFocusableAdapter);
      }
    }

    public void Focus() => CUICore.RequestFocus(IFocusableAdapter);

    //TODO should these take this CUIComponent as first arg?
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
  }
}