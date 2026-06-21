using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public abstract class VisualElementBase : IMouseEventConsumer, IVisualElement
  {
    public VisualElementBase()
    {
      VisualWrapper = new(this);
    }

    public object HostComponent { get; set; }
    public string HostPropName { get; set; }

    public bool Visible { get; set; } = true;
    public abstract CUIRect Rect { get; set; }
    public abstract void Draw(CUISpriteBatch spriteBatch);

    public VisualUnit.PrimitiveVisualElement VisualWrapper { get; }

    public bool MouseOver { get; set; }
    public bool MousePressed { get; set; }

    public bool ConsumeMouseClicks { get; set; }

    public ClearableEvent<CUIMouseDownEvent> MouseDown { get; } = new();
    public ClearableEvent<CUIMouseUpEvent> MouseUp { get; } = new();
    public ClearableEvent<CUIMouseClickEvent> MouseClick { get; } = new();
    public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
    public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; } = new();
    public ClearableEvent<CUIMouseEnterEvent> MouseEnter { get; } = new();
    public ClearableEvent<CUIMouseLeaveEvent> MouseLeave { get; } = new();
    public ClearableEvent<CUIMouseOnEvent> MouseOn { get; } = new();
    public ClearableEvent<CUIMouseOffEvent> MouseOff { get; } = new();
    public ClearableEvent<CUIMouseScrollEvent> MouseScroll { get; } = new();

  }
}