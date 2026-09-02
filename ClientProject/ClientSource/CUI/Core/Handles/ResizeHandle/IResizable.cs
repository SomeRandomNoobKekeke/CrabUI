using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{
  public interface IResizable
  {
    public event Action<CUIMouseDownEvent> MouseDown;
    public CUIRect Rect { get; }


    /// <param name="preventMovement">passed in case you don't want to touch 1 of the coordinates</param>
    public void ResizeToAbsoluteRect(CUIRect rect, CUIBool2 preventMovement);
    public event Action<CUIMouseUpEvent> HubMouseUp;
    public event Action<CUIMouseMovedEvent> HubMouseMoved;
    public bool TryGrab(object handle);
    public void Release(object handle);

    public Vector2 MinSize { get; }
    // public Vector2 MaxSize { get; }//TODO
  }
}