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

    public event Action OnOpen;
    public void RaiseOnOpen() => OnOpen?.Invoke();
    public Action AddOnOpen { set { OnOpen += value; } }

    public event Action OnClose;
    public void RaiseOnClose() => OnClose?.Invoke();
    public Action AddOnClose { set { OnClose += value; } }
  }
}