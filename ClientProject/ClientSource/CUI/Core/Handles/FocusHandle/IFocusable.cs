using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CrabUI
{
  public interface IFocusable
  {
    public event Action<CUIMouseDownEvent> MouseDown;
    public bool Focused { get; set; }
    public void RequestFocus();
  }
}