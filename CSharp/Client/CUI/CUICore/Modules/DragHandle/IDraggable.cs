using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentInjector;

namespace CrabUI
{
  public interface IDraggable
  {
    public event Action<CUIMouseDownEvent> MouseDown;
    public CUIRect Rect { get; }
    public CUIRect? ParentRect { get; }
    public void SetAbsolutePos(float x, float y);
  }
}