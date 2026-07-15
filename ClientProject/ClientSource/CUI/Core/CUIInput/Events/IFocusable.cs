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
  public interface IFocusable : IEventConsumer
  {
    public bool Focused { get; set; }
    public ClearableEvent OnFocus { get; }
    public ClearableEvent OnFocusLost { get; }
  }
}