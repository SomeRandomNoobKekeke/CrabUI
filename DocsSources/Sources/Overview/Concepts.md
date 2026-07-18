# Other Concepts {#OverviewOtherConcepts}

### CUIInfrastructure
Most CUI stuff is located in CUICore  
CUICore is passive isolated state machine  
It doesn't know about the game and has to be runned by CUIRunners via handles 

There could be various runners, e.g. they can run code in this mod, or scan other mods, or feed it with test data, or run it in a separate process

Also data sources are separated from runners

CUICore + CUIRunners + data sources makes CUISetup  
There's one static CUISetup at CUI.Setup  
It's initiated at CUI.Start() and automatically disposed at plugin.Dispose()

CUI.Setup is the singleton here, everything else is just pointing to it

CUI and CUICore classes have static members for accessing core and runners  
Why 2? CUI is supposed to be used by you, and CUICore is supposed to be called by stuff from inside CUICore  
Also stuff that calls CUICore assumes that it's already activated

### VisualComponents / VisualUnits
Unit of drawing / event handling in CUI is VisualUnits  

SimpleTexture, Borders, TextBlock are VisualUnits  

VisualComponents is aggregate for VisualUnits, it's a base class for CUIComponent and other stuff containing VisualUnits  
VisualComponents have VisualSplit method where they specify how they should be splitted

CUIMainComponent recursively splits VisualComponents into a chain of VisualUnits  
Then it draws them and dispatches events to them  
Host VisualComponent typically listens to events on its VisualUnits

### Serialization (wip, mostly borked)
All components are serializable to / from xml with
~~~~~~~~~~~~~{cs}
public static CUIComponent CUIComponent.Deserialize(XElement element)
public XElement Serialize()
~~~~~~~~~~~~~

Also CUIComponents can be accessed as
~~~~~~~~~~~~~{cs}
public IDictionary<string, string> As_StringDictionary { get; }
public IDictionary<string, object> As_Dictionary { get; }
~~~~~~~~~~~~~

### Component States
you can save / restore component state with

~~~~~~~~~~~~~{cs}
public void SaveState(string name)
public void RestoreState(string name)
~~~~~~~~~~~~~

there's a dict of MemorizedStates behind the scenes and it uses serialization

### Focus (wip, mostly borked)
In vanilla there's 
~~~~~~~~~~~~~{cs}
public IKeyboardSubscriber Barotrauma.GUI.KeyboardDispatcher.Subscriber { get; set; }
~~~~~~~~~~~~~
new subscriber gains focus, old one loses it

In CUI there's
~~~~~~~~~~~~~{cs}
public CUICore.GlobalFocusTracker.Focused { get; set; }
~~~~~~~~~~~~~
with the  same idea

Focusable components are focused when clicked  
But actually they only request focus   
it's resolved at the end of update cycle because there might be multiple clicked components at once

CUI can steal focus from vanilla KeyboardDispatcher and simulate focus on some dummy IKeyboardSubscriber  
It's barely tested and i'm sure it's borked  
Also there's a lot of actions in vanilla that just ignores KeyboardDispatcher.Subscriber, idk how to block those 

### Styles and palettes (wip, mostly borked)
ICUIStyle is something that can be applied to component and change its state  
It can be a function, a dict of props or xml prefab  

Many CUIComponent types have attached DefaultStyles  
All CUIComponent scanned on CUI.Start() and default styles from lineage of CUIComponents are combined in CUIStylePipelines  
Which then applied on component creation

There's also CUIPalettes, they are dicts of typically used colors, sizes etc  
There's global Primary, Secondary palettes in CUICore.Styles and components can have personal palettes

Styles typically use values from component palette  

CUIStylePipelines and palettes are reactive, so changin styles or palette will reaplly CUIStylePipelines

So if you need some props to be resistant to palette changes put them in personal styles



