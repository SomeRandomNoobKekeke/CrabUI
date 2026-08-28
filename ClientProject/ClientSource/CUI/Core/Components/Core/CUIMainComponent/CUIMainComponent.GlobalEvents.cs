using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class CUIMainComponent
  {
    public GlobalEvents_Part GlobalEvents { get; } = new();
    public class GlobalEvents_Part : Part, IMouseEventConsumer, IKeyboardEventConsumer
    {

      public ClearableEvent BeforeUpdate { get; } = new();
      public ClearableEvent AfterUpdate { get; } = new();

      public bool MouseOver { get; set; } // BRUH
      public bool MousePressed { get; set; }

      public bool ConsumeMouseEvents { get; set; } // BRUH

      public ClearableEvent<CUIMouseDownEvent> MouseDown { get; } = new();
      public ClearableEvent<CUIMouseUpEvent> MouseUp { get; } = new();
      public ClearableEvent<CUIMouseClickEvent> MouseClick { get; } = new();
      public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
      public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; } = new();
      public ClearableEvent<CUIMouseEnterEvent> MouseEnter { get; } = new(); // BRUH
      public ClearableEvent<CUIMouseLeaveEvent> MouseLeave { get; } = new(); // BRUH
      public ClearableEvent<CUIMouseOnEvent> MouseOn { get; } = new(); // BRUH
      public ClearableEvent<CUIMouseOffEvent> MouseOff { get; } = new(); // BRUH
      public ClearableEvent<CUIMouseScrollEvent> MouseScroll { get; } = new();

      public ClearableEvent<CUIKeyPressedEvent> KeyPressed { get; } = new();
      public ClearableEvent<CUIKeyReleasedEvent> KeyReleased { get; } = new();
      public ClearableEvent<CUITextInputEvent> TextInput { get; } = new();
      public ClearableEvent<CUIKeyDownInputEvent> KeyDownInput { get; } = new();

      public bool IsPointOnTransparentPixel(Vector2 point) => false;// BRUH
    }
  }
}