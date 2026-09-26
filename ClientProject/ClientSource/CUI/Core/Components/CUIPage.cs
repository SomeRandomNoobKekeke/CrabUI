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
  public partial class CUIPage : CUIComponent, IComponent
  {
    protected virtual void HandleOpen() { }
    protected virtual void HandleClose() { }

    public event Action OnOpen;
    public event Action OnClose;

    public Action AddOnOpen { set { OnOpen += value; } }
    public Action AddOnClose { set { OnClose += value; } }

    public void RaiseOnOpen()
    {
      HandleOpen();
      OnOpen?.Invoke();
    }
    public void RaiseOnClose()
    {
      HandleClose();
      OnClose?.Invoke();
    }

  }
}