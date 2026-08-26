using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;
using System.Text.Json;

namespace CursedUI
{
  public class DummyVisualElement : IMouseEventConsumer
  {
    public bool MouseOver { get; set; }
    public bool MousePressed { get; set; }
    public bool ConsumeMouseEvents { get; set; }
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

    public bool IsPointOnTransparentPixel(Vector2 point) => false;
  }
}