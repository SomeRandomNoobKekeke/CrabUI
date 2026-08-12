using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
namespace CrabUI
{
  public partial class CUIVisualComponent
  {
    public virtual bool MouseOver { get; }
    public virtual bool MousePressed { get; }
    public virtual bool ConsumeMouseEvents { get; set; }
    public virtual bool IsPointOnTransparentPixel(Vector2 point) => false;

    protected Events_Part Events { get; } = new();
    public class Events_Part : Part, IModule, IMouseEventConsumingComponent
    {
      public CUIVisualComponent Component => Self;

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

      public ClearableEvent<Vector2> DragStarted { get; } = new();
      public ClearableEvent<Vector2> Dragged { get; } = new();
      public ClearableEvent<Vector2> DragEnded { get; } = new();

      public ClearableEvent<CUIRect> Resized { get; } = new();
      public ClearableEvent<CUIRect> RectSet { get; } = new();

    }
  }
}