using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public interface IResizable
  {
    public event Action<CUIMouseDownEvent> MouseDown;
    public CUIRect Rect { get; }
    public void ResizeFrom2Points(Vector2 point1, Vector2 anchor1, Vector2 point2, Vector2 anchor2);
    public event Action<CUIMouseUpEvent> HubMouseUp;
    public event Action<CUIMouseMovedEvent> HubMouseMoved;
    public bool TryGrab(object handle);
    public void Release(object handle);
  }
}