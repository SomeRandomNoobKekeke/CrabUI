using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public abstract class CUIMouseButtonEvent : CUIMouseEvent
  {
    public CUIMouseButton Button { get; }

    public CUIMouseButtonEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(mouse)
       => Button = button;
  }
}