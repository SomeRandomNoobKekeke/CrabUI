# Overview {#Overview}

## Basics 
All GUI elements: frames, buttons, list, textblocks are CUIComponents

They are C# objects, they all have parameterless constructor so you can just create them

They have props that control how they will be drawn and positioned
~~~~~~~~~~~~~{cs}
CUIFrame frame = new CUIFrame()
{
  Absolute = new CUINullRect(w: 400, h: 600),
  Background = { Color = Color.Blue, } // you can set deep props like this
};
~~~~~~~~~~~~~

CUIComponents can be connected to each other forming a tree, they have:
~~~~~~~~~~~~~{cs}
public IList<CUIComponent> Children { get; }
public CUIComponent Parent { get; set; }
~~~~~~~~~~~~~

you can connect them like this
~~~~~~~~~~~~~{cs}
CUI.Main.Children.Add(frame); 
// or
frame.Parent = CUI.Main;
~~~~~~~~~~~~~

Another fancy way to connect components is indexing  
It adds component as a child and memorizes it, so you can later access it by name
~~~~~~~~~~~~~{cs}
frame["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1), };
frame["layout"]["handle"] = new CUIHorizontalList()
{
  FitContent = new CUIBool2(false, true),
};
frame["layout"]["handle"]["caption"] = Caption = new CUITextBlock("bruh")
{
  Flex = 1,
};
~~~~~~~~~~~~~

To make your components visible add them to CUI.Main or CUI.TopMain  
CUI.Main is drawn below vanilla GUI and TopMain is drawn above
~~~~~~~~~~~~~{cs}
frame.Open(); // same as CUI.Main.Children.Add(frame);
~~~~~~~~~~~~~

CUIComponent can handle various events, e.g.
~~~~~~~~~~~~~{cs}
//mmm so consistent
frame.MouseDown += (c, e) => CUI.Logger.Log($"Mouse down at {e.Pos}");
frame.OnFocus += () => frame.Background.Color = Color.Cyan;
frame.OnFocusLost += () => frame.Background.Color = Color.Blue;
frame.KeyPressed += (e) => CUI.Logger.Log(e.Key);
~~~~~~~~~~~~~
<br><br>
## Components
- CUIComponent - It's just a square with background and borders
- CUIMainComponent -Special component that Draws and calculates layouts of its children, there's 2 CUI.Main and CUI.TopMain, don't create
- CUIButton - A button
- CUICanvas - A canvas, you can draw on its texture
- CUICheckBox - A checkbox
- CUICloseButton - Button with a cross that emits "close" event that forces nearest frame to close
- CUIDropDown - Drop down select (borked)
- CUIFrame - Draggable and resizable container for other components, can be Opened and Closed
- CUIVerticalList - Wrapper with CUIVerticalListLayout
- CUIGrid - Wrapper with Grid layour (half-assed)
- CUIPages - Can have only 1 child - opened page, resizes opened page to its size
- CUIPage - When opened in CUIPages recieves OnOpen / OnClose events
- CUIToggleButton - Button that can be toggled On and Off
- CUIRadioButton - Button that can be added to a group where only 1 button can be On at a time
- CUIScrollBar - Box with a slider that can be dragged, exposes Lambda (half-assed)
- CUITextBlock - Block of text with background, can resist shrinking
- CUITextLine - Line of text
- CUITextInput - Text input (half-assed)

#### CUIDefault.
- CUIDefault.Frame - prebuilt frame with handle, caption and close button, add children to frame["layout"]
- CUIDefault.Panels - Styled and a bit dimmed panels
- InputField - Base class for InputField, wrappers with label and text input, intended to be used in some settings ui (half-assed)

<br><br>
## Layouts
- CUIPlainLayout - simple two-dimensional plane
- CUIVerticalListLayout - places children in a stack
- CUIHorizontalListLayout - Same as CUIVerticalListLayout but horizontal
- CUIGridLayout - places children in cells specified by GridRow and GridColumn (half-assed)

<br><br>
## Types
#### Commonly used "primitive" types:
~~~~~~~~~~~~~{cs}
public enum CUIDirection { Straight, Reverse, }
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public struct CUIRect {
  public float Left;
  public float Top;
  public float Width;
  public float Height;
}
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public struct CUINullRect {
  public float? Left;
  public float? Top;
  public float? Width;
  public float? Height;
}
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public struct CUIBool2 {
  public bool X;
  public bool Y;
}
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public struct CUISizes {
  public float Top;
  public float Right;
  public float Bottom;
  public float Left;
}
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public static class CUIAnchor {
  public static Vector2 LeftTop = new Vector2(0.0f, 0.0f);
  public static Vector2 CenterTop = new Vector2(0.5f, 0.0f);
  public static Vector2 RightTop = new Vector2(1.0f, 0.0f);
  ...
}
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public struct CUIBoundaries {
  public float? MinX;
  public float? MaxX;
  public float? MinY;
  public float? MaxY;
}
~~~~~~~~~~~~~

<br><br>
## Props
#### Commonly used Props of CUIComponent:
#### Layout Props:
Note that props used by some layouts might be ignored in others

~~~~~~~~~~~~~{cs}
public CUINullRect Absolute { get; set; } // - Absolute position and size in pixels
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUINullRect AbsoluteMin { get; set; } // - Min Absolute position and size in pixels
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUINullRect AbsoluteMax { get; set; } // - Max Absolute position and size in pixels
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUINullRect Relative { get; set; } // - Position and size relative to parent size 
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUINullRect RelativeMin { get; set; } // - Min Position and size relative to parent size 
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUINullRect RelativeMax { get; set; } // - Max Position and size relative to parent size 
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUINullRect CrossRelative { get; set; } // - Relative position and size but to the opposite dimension, useful for creating square things
~~~~~~~~~~~~~

Child position is calculated from Anchor position  
Child is attached with its Anchor point to ParentAnchor of parent rect  
if ParentAnchor is null Anchor is used for both
~~~~~~~~~~~~~{cs}
public Vector2 Anchor { get; set; } // - ([0..1],[0..1]) Vector2
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public Vector2? ParentAnchor { get; set; } // - ([0..1],[0..1]) Vector2?
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIDirection Direction { get; set; } // - used in lists to specify direction of child positioning
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public float? Flex { get; set; } // - if not null then child will fill empty space in lists 
                                 // - if there's multiple flex components space will be splitted among them proportionally to their flex
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public Vector2 ChildrenOffset { get; set; } // - All children will be shifted on this vector, used for scroll
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIBoundaries ChildrenOffsetBounds { get; set; } // - ChildrenOffset will be bounded with these, used to limit scroll
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public Func<CUIRect, CUIBoundaries> { get; set; } // - Used to keep children in some bounds, there's static funcs in CUIBoundaries
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIBool2 FitContent { get; set; } // - Parent will resize to fit all children inside of its rect 
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public int GridRow { get; set; } // - Grid row to put this component into
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public int GridColumn { get; set; } // - Grid column to put this component into
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public (int, int) Grid { get; set; } // - Set both at once
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public CUISizes Margin { get; set; } { get; set; } // - Empty space around component
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUISizes Border { get; set; } // - Size of borders
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUISizes Padding { get; set; } // - Inner empty space between border and children
~~~~~~~~~~~~~

Note: these rects are not readonly, but you don't need to set them, they're supposed to be set by parent layout
~~~~~~~~~~~~~{cs}
public CUIRect { get; set; } OuterRect
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIRect Rect { get; set; } // OuterRect - Margin
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIRect InnerRect { get; set; } // Rect - Border
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIRect ChildrenRect { get; set; } // InnerRect - Padding
~~~~~~~~~~~~~

#### Graphics Props:
~~~~~~~~~~~~~{cs}
public SimpleTexture Background { get; }

public class SimpleTexture {
  public CUISprite Sprite { get; set; }
  public bool Visible { get; set; }

  // Note that these props are forwarded from Sprite, so if you change sprite you throw away these as well 
  public CUITexture2D Texture { get; set; }
  public Rectangle? SourceRectangle { get; set; }
  public Color Color { get; set; }
  public float Rotation { get; set; }
  public Vector2 Origin { get; set; }
  public SpriteEffects Effects { get; set; }
  public float LayerDepth { get; set; }
}
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public Borders Borders { get; }

public class Borders {
  public CUISizes Sizes { get; set; }
  public bool Visible { get; set; }
  public Color Color { get; set; }
}
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool Displayed { get; set; } // - If false component won't even be added to the list of visible components
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool CullChildren { get; set; } // - Should children be culled and cropped outside of parent rect
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool IgnoretransparentPixels { get; set; } // - Whether clicks should register on transparent pixels, it buffers texture data to test pixel, it's heavy, use wisely
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool UseReactiveStyles { get; set; } // - Defaults to CUICore.Styles.UseReactiveStyles
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIPaletteRank PaletteRank { get; set; } // - uses palette from CUICore.Palettes of some rank (Primary, Secondary)
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIPalette Palette { get; set; } // - Changes palette, it'll retrigger styles
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIPalette DeepPalette { get; set; } // - Recursively sets palette for children 
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public Action<CUIComponent> Style { get; set; } // - Arbitrary func that sets some props on component, saved in PersonalStyle
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public ICUIStyle PersonalStyle { get; set; } // - Extra style that will be applied after type specific styles
~~~~~~~~~~~~~

#### Other Props:
~~~~~~~~~~~~~{cs}
public bool Focusable { get; set; } // - Can it be focused
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool Draggable { get; set; } // - Can it be dragged
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool Swipeable { get; set; } // - Can it be swiped
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool Resizable { get; set; } // - Can it be resized
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool ConsumeFocus { get; set; } // - Should it consume focus events
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool ConsumeMouseEvents { get; set; } // - Should it consume mouse events
~~~~~~~~~~~~~

<br><br>
## Events
Note: most events accompanied by delegate props where you can set callbacks in object initializers 
~~~~~~~~~~~~~{cs}
CUIComponent component = new CUIComponent()
{
  OnMouseDown = (c, e) => CUI.Logger.Log(e.Pos),
};
//Same as
component.MouseDown += (c, e) => CUI.Logger.Log(e.Pos);
~~~~~~~~~~~~~

You can trigger some events manually: 
~~~~~~~~~~~~~{cs}
public void CUIComponent.Click(); // - triggers MouseDown
public void CUIComponent.PressKey(Keys key); // - Triggers KeyPressed
~~~~~~~~~~~~~

Also most events have "this" component as first arg  
Some of them don't and it's a consistency bug which i'll fix somewhere in the future

Also idk if it's a good idea to pass "this" as first arg, in fact i never use it, tell me your thoughts

ClearableEvents are wrappers around event that can be cleared, raised from outside and routed to one another

#### Commonly used Events of CUIComponent:
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


