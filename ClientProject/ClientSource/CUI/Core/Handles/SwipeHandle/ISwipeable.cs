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
  public interface ISwipeable
  {
    public event Action<CUIMouseDownEvent> MouseDown;
    public CUIRect Rect { get; }
    public CUIRect? ParentRect { get; }
    public void MoveChildrenOffset(Vector2 offset);

    public event Action<CUIMouseUpEvent> HubMouseUp;
    public event Action<CUIMouseMovedEvent> HubMouseMoved;

    public bool TryGrab(object handle);
    public void Release(object handle);
  }
}