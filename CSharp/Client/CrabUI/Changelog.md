## 0.0.5.1

Added "[CUISerializable] public string Command { get; set; }"" to CUIButton so you could define command that is called on click in xml

Splitted MasterColor into MasterColor and MasterColorOpaque

CUITextBlock RealTextSize is now rounded because 2.199999 is prone to floating-point errors 

Added MasterColor to CUIToggleButton

Buttons now will folow a pattern: if state is changed by user input then it'll invoke StateChanged event  
If state is changed from code then it won't invoke the event

When you change state from code you already know about that so you don't need an event  
And on the other side if event is always fired it will create un-untangleable loops when you try to sync state between two buttons

Fixed CUIComponent.Get< T > method, i forgot to add it to docs, it gives you memorized component by its name, so it's same as frame["header"], but it can also give you nested components like that `Header = Frame.Get<CUIHorizontalList>("layout.content.header");`

Exposed ResizeHandles, you can hide them separately

Fixed crash when serializing a Vector2, added more try-catches and warnings there

Fixed Sprites in CUI.png being 33x33, i just created a wrong first rectangle and then copy-pasted it, guh

Added sprites to resize handles, and gradient sprite that's not used yet

Added `public SpriteEffects Effects;` to CUISprite, it controls texture flipping

More params in CUITextureManager.GetSprite

## 0.0.5.0

Added Styles

Every component has a Style and every Type has a DefaultStyle

Styles are observable dicts with pairs { "prop name", "prop value" } and can be used to assign any parsable string to any prop or reference some value from CUIPalette

## 0.0.4.0
Added CUICanvas.Render(Action< SpriteBatch > renderFunc) method that allows you to render anything you want onto the canvas texture

## 0.0.3.0
Added Changelog.md :BaroDev:

Added CUI.TopMain, it's the second CUIMainComponent which exists above vanilla GUI

Added printsprites command, it prints styles from GUIStyle.ComponentStyles

More fabric methods for CUISprite