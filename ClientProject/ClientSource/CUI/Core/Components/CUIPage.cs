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
  public partial class CUIPage : CUIComponent, IComponent
  {
    public ClearableEvent OnOpen { get; } = new();
    public Action AddOnOpen { set { OnOpen.Add(value); } }

    public ClearableEvent OnClose { get; } = new();
    public Action AddOnClose { set { OnClose.Add(value); } }
  }
}