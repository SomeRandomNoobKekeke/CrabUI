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
    public void SetSize(Vector2 size);
    public event Action<CUIMouseUpEvent> HubMouseUp;
    public event Action<CUIMouseMovedEvent> HubMouseMoved;
  }
}