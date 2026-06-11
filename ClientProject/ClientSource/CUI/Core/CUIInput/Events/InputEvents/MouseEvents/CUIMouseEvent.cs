using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public abstract class CUIMouseEvent : InputEvent
  {
    public Vector2 Pos => Mouse.Pos;
    public CUIInput.MouseInput Mouse { get; }

    public CUIMouseEvent(CUIInput.MouseInput mouse) => Mouse = mouse;
  }
}