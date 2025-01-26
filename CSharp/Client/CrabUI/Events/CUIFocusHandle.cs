using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public class CUIFocusHandle : ICUIVitalizable
  {
    public void SetHost(CUIComponent host) => Host = host;
    public CUIComponent Host;
    public bool Focusable;
    public CUIMouseEvent Trigger = CUIMouseEvent.Down;

    public bool ShouldStart(CUIInput input)
    {
      return Focusable && (
        (Trigger == CUIMouseEvent.Down && input.MouseDown) ||
        (Trigger == CUIMouseEvent.DClick && input.DoubleClick)
      );
    }

    public CUIFocusHandle() { }
    public CUIFocusHandle(CUIComponent host) => Host = host;
  }
}