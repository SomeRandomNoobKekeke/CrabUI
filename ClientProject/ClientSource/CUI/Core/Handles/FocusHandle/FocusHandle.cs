using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using BaroJunk;

namespace CrabUI
{
  public class FocusHandle : IModule
  {
    private IFocusable host;
    [In]
    public IFocusable Host
    {
      get => host;
      set
      {
        if (host is not null) DisconnectFromHost(host);
        host = value;
        if (host is not null) ConnectToHost(host);
      }
    }

    public bool Focusable { get; set; }

    private bool _Focused; public bool Focused
    {
      get => _Focused;
      set
      {
        if (_Focused == value) return;
        _Focused = value;
        if (value) OnFocus.Raise(); else OnFocusLost.Raise();
      }
    }

    public ClearableEvent OnFocus { get; } = new();
    public ClearableEvent OnFocusLost { get; } = new();


    //TODO could focus be triggered by other actions?
    private void ConnectToHost(IFocusable host)
    {
      host.MouseDown += FocusHandler;
    }

    private void DisconnectFromHost(IFocusable host)
    {
      host.MouseDown -= FocusHandler;
    }

    private void FocusHandler(CUIMouseDownEvent e)
    {
      Focus();
    }

    public void Focus()
    {
      if (!Focusable) return;
      //Note: i can only request focus, Focused is set by CUICore after processing all requests
      Host.RequestFocus();
    }


  }
}