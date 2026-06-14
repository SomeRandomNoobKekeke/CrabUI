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
  public partial class CUIComponent : IFocusableComponent
  {
    public bool Focused { get; set; }
    public bool Focusable { get; set; }
    public void Focus() => Focused = true;
    public ClearableEvent FocusLost { get; }
  }
}