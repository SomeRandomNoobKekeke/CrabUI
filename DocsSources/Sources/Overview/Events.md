# Events {#OverviewEvents}

Note: most events accompanied by delegate props where you can set callbacks in object initializers 
~~~~~~~~~~~~~{cs}
CUIComponent component = new CUIComponent()
{
  OnMouseDown = (c, e) => CUI.Logger.Log(e.Pos),
};
//Same as
component.MouseDown += (c, e) => CUI.Logger.Log(e.Pos);
~~~~~~~~~~~~~

Also most events have "this" component as first arg  
Some of them don't and it's a consistency bug which i'll fix somewhere in the future

Also idk if it's a good idea to pass "this" as first arg, in fact i never use it, tell me your thoughts

ClearableEvents are wrappers around event that can be cleared, raised from outside and routed to one another

### Commonly used Events of CUIComponent:
~~~~~~~~~~~~~{cs}
public event Action<CUIComponent, CUIMouseDownEvent> MouseDown;
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIComponent, CUIMouseUpEvent> MouseUp
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIComponent, CUIMouseClickEvent> MouseClick
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIComponent, CUIMouseDoubleClickEvent> MouseDoubleClick
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIComponent, CUIMouseEnterEvent> MouseEnter
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIComponent, CUIMouseLeaveEvent> MouseLeave
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIComponent, CUIMouseOnEvent> MouseOn // It's called on components currently under the mouse
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIComponent, CUIMouseOffEvent> MouseOff // It's called on prev components under the mouse
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIComponent, CUIMouseScrollEvent> MouseScroll
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIComponent, Vector2> Dragged
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIComponent, CUIRect> RectSet
~~~~~~~~~~~~~

Component recieves Key events only when focused
~~~~~~~~~~~~~{cs}
public event Action OnFocus
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action OnFocusLost
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIKeyPressedEvent> KeyPressed
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIKeyReleasedEvent> KeyReleased
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUITextInputEvent> TextInput // localized + spammed when key is held
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public event Action<CUIKeyDownInputEvent> KeyDownInput // spammed when key is held
~~~~~~~~~~~~~

There's also global events on main components, e.g. in `CUI.Main.GlobalEvents`

~~~~~~~~~~~~~{cs}
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
  }
}
~~~~~~~~~~~~~


