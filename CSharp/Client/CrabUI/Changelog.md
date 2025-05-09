# Changelog

## 0.2.7.0

Removed CUIFrame outline from DrawFront, but not ResizeHandle yet, it's a bit harder  
Now Frame outlines won't be visible through other components

Added MoveToFront method to CUIComponent, made CUIFrames MoveToFront on click by default

Also CUIFrames now closes on "Close Frame" command by default so you don't need to set it, and i matches CloseButton command

CUIToggleButton.ToggleOnClick CUIMultiButton.CycleOnClick props

Added Repeat prop for CUIAnimation

Made CUIPalette a bit more resistant to missing files and misuse  
Added hardcoded backup palette so you could see something even if you could't load it for some reason

#### added AttachedItems
You can now attach CUIComponent to Barotrauma.Item and track when it's get selected, deselected and when its UpdateHUD is called  
Check docs

#### Serialization changes:

Separated serialization and parsing logic  
All parsing is now done in CUIParser, it can parse anything, you can also use it

Renamed:  
- Unserializable -> Serializable, should this component be serialized
- BreakSerialization -> SerializeChildren, should children of this component be serialized

Added:
- MergeSerialization - Children of deserialized component will be moved to existing component
- ReplaceSerialization - Deserialized component will replace existing component

#### Debug
Added
- CUIPropsInterface component
- CUIComponentSelector component
- cuidebugprops console command
- printcolors console command

Added checkers texture for debug

## 0.2.6.4

"Fixed" buttons still changing colors behind components with ConsumeMouseClicks

## 0.2.6.3

Added Piano test

## 0.2.6.2

Added Gap prop for CUIHorizontalList

## 0.2.6.1

Set UseCursedPatches to true by default because that way it's easier to make mod running  

Added Ghost Detector, it will notify you if something cursed happenes with the patches

Added CUIButton.PlaySound prop, you can prevent it from playing click sound 

Rearranged files to confuse you even more

## 0.2.6.0
Found one ugly solution to the curse (https://github.com/evilfactory/LuaCsForBarotrauma/issues/245), i think it's the only way to bypass it  
If you find a better way let me know  

So, the problem is harmony patches in assembly mods are bugged and generally don't work  

Solution: Add lua script that adds patches and calls hooks, and then add hooks from assembly

If you set CUI.UseCursedPatches to true it'll use harmony and be potentially bugged  
If you set CUI.UseCursedPatches to false it won't be bugged, but you'll also have to set CUI.HookIdentifier to some name or hooks from different CUIs will conflict and add AdditionalHooks.lua to Lua/Autorun

## 0.2.5.1
Experimenting with the way multiple CUIs resolve conflicts
Renamed CUI.UpdateHookIdentifier => CUI.HookIdentifier
now i'm using it in harmony patches to

added warning if it's not set

fixed crash in GUI_UpdateMouseOn_Postfix
Added null checks in GUI_UpdateMouseOn_Postfix

## 0.2.5.0
Added CUI.UpdateHookIdentifier it will be set as identifier to CUI think hook, it very important to set it or hooks from different CUIs will conflict

Added CUIAnimation
Added IgnoreTransparent prop, if true mouse events will work only on not transparent sprites
Added Transparency prop, it multiplies BackgroundColor.A and is propagated to Children

Made CUISpite an object... again
Added Rotation, Origin and Offset to CUISprite
Added option to load CUISprites with base folder, which allows deserialized components to load sprites from the same folder with relative paths

Added CUIMenu, check "Custom Menus" mod, CUIRadialMenu (the ugly brother of CUIMenu)

Added more docs

## 0.2.4.0

"Fixed" cursed bug that made MainComponents become in GameMain.Update patch after multiple lobbies in compiled version
But this "fix" seems to decrease update smoothness, so i might rethink later
Set CUI.UseCursedPatches to true if you're not affraid

Added more performance measurements, shortcutted dumb class scanning in CUILuaRegistrar that happened even if you didn't use lua

Buttons now update their color only on events and not in draw cycle, added AutoUpdateColor to prevent this color change in case you want to control it manually (why?)

Added confusing event InvokeOnMouseOff which is symmetrical to InvokeOnMouseOn but happens on previous MouseOn list, and it turned out to be essential to e.g. switch color when mouse leaves a button

You can now limit resize directions with CUIComponent.ResizeDirection

Fixed forsed size not reseting after removing a textblock

Added cuiprinttree command along with cuidraworder


## 0.2.3.0

Made CUITextInput, CUITickBox and CUISlider use commands and consume data

Added Gap to CUIVerticalList

Made OnAnyCommand,OnAnyData,OnConsume events instead of delegates

added ReflectCommands and RetranslateCommands props, so you could define in xml that a wrapper should sync state between its children

Setting a Palette prop now won't automatically set palette for all children because it doesn't work as intended on deserialized components, use DeepPalette instead

CUISlider now have Range and Precision

CUI.OnPauseMenuToggled will now trigger even when resume button in pause menu is pressed

You can no just set CUIPalette.DefaultPalette before CUI.Initialize instead of manually loading it

Palettes now remember their BaseColor so you could replicate them

Added more useless CUIPrefabs, i think they are too specific and need some redesign, perhaps i need to create some builder 

Added FloatRange alongside with IntRange

fixed crash in KeyboardDispatcher_set_Subscriber_Replace in compiled mods

## 0.2.2.1

Minor stuff: multibutton dispatches the command, CUIPages resizes page to [0,0,1,1], maincomponent flatten tree is a bit more thread safe

Added IRefreshable interface and CUIComponent.CascadeRefresh
CascadeRefresh will recursivelly call Refresh on every child that implements IRefreshable

## 0.2.2.0

Added to CUI.cs
```
using System.Runtime.CompilerServices;
[assembly: IgnoresAccessChecksTo("Barotrauma")]
[assembly: IgnoresAccessChecksTo("DedicatedServer")]
[assembly: IgnoresAccessChecksTo("BarotraumaCore")]
```
So it could be compiled

#### Temporary solution to pathing:

Now mod won't automatially find its folders

If you want to use lua you need to set CUI.ModDir to the mod folder path

Also you need to place Assets folder with CUI stuff somewhere in your mod and set CUI.AssetsPath to it  
You can rename it, just set the path

All this needs to be done before CUI.Initialize()

## 0.2.1.0

Dried tree building methods, added tests for them

Added insert method along with append and prepend, unlike List.Insert it won't throw on "index out of bounds"

## 0.2.0.1

.nojekyll moment

## 0.2.0.0

Reworked CUIPalette, and CUICommand, check docs

Reworked border, added separate borders for each side, border sprite, outline

Changed how zindex is calculated, now every next child will have higher zindex -> everything in one frame will be above or below everything in the other

optimized CUITextBlock measurements, added some validation to CUITextInput

Added CUIPresets with... presets. Which you can use to reduce boilerplate code 

Made more stuff parsable and serializble

And tons of other things i'm too lazy to write down, compare commits if you're curious


## 0.1.0.0

You can now use relative paths for sprite textures  
You can set CUI.PGNAssets to the folder with your assets, CUI will also search for textures there

Reworked MasterColorOpaque, it now just uses base color alpha

Synced vertical and horizontal lists, added ScrollSpeed

OnUpdate event now invoked before layout calcs, Also added OnLayoutUpdated event, it's invoked before recalc of children layouts

"Fixed" imprecise rects that e.g. caused sprite overlap and gaps

Added CrossRelative prop, it's like Relative but values are relative to the oposite value, e.g. CrossRelative.Width = Host.Real.Height, convinient for making square things

Reworked slider component

DragHandle can now DragRelative to the parent 

#### Serialization 

Added BreakSerialization prop, if true serrialization will stop on this component

Added LoadSelfFromFile, SaveToTheSamePath methods

Added Hydrate method, it will run right after deserizlization, allowing you to access children with Get<> and e.g. save them to local vars

Added SaveAfterLoad prop, it's mostly for serialization debugging

Added more Custom ToString and parsed methods to CUIExtensions, Added native enum serialization, Vector2 and Rectangle is now serialized into [ x, y ], be carefull

Sprite is now fully serializable

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