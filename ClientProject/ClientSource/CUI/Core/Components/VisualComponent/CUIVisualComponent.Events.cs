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
    public abstract bool MouseOver { get; }
    public abstract bool MousePressed { get; }


    protected Events_Part Events { get; } = new();
    public class Events_Part : Part, IModule, IMouseEventConsumingComponent
    {
      public CUIVisualComponent Component => Self;


      public ClearableEvent<CUIVisualComponent, CUIMouseDownEvent> MouseDown { get; } = new();
      public ClearableEvent<CUIVisualComponent, CUIMouseUpEvent> MouseUp { get; } = new();
      public ClearableEvent<CUIVisualComponent, CUIMouseClickEvent> MouseClick { get; } = new();
      public ClearableEvent<CUIVisualComponent, CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
      public ClearableEvent<CUIVisualComponent, CUIMouseMovedEvent> MouseMoved { get; } = new();
      public ClearableEvent<CUIVisualComponent, CUIMouseEnterEvent> MouseEnter { get; } = new();
      public ClearableEvent<CUIVisualComponent, CUIMouseLeaveEvent> MouseLeave { get; } = new();
      public ClearableEvent<CUIVisualComponent, CUIMouseOnEvent> MouseOn { get; } = new();
      public ClearableEvent<CUIVisualComponent, CUIMouseOffEvent> MouseOff { get; } = new();
      public ClearableEvent<CUIVisualComponent, CUIMouseScrollEvent> MouseScroll { get; } = new();

      public ClearableEvent<CUIVisualComponent, Vector2> DragStarted { get; } = new();
      public ClearableEvent<CUIVisualComponent, Vector2> Dragged { get; } = new();
      public ClearableEvent<CUIVisualComponent, Vector2> DragEnded { get; } = new();

      public ClearableEvent<CUIVisualComponent, CUIRect> Resized { get; } = new();
      public ClearableEvent<CUIVisualComponent, CUIRect> RectSet { get; } = new();


    }
  }
}