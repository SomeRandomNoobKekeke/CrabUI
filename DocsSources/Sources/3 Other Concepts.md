# Other Concepts {#OtherConcepts}

## CUIInfrastructure

Most CUI stuff is located in CUICore  
CUICore is a passive isolated state machine  
It doesn't know about the game and has to be run by CUIRunners via handles  

There could be various runners, e.g. Solo runner runs code in this mod, Master runner scans other mods, test runner can feed it with fake data and concurrent runner can run CUICore in a separate thread

#### Then why are you putting solo runner in CUI master package?

[Ah, eto... bleh.gif](https://tenor.com/ru/view/ah-eto-bleh-anime-bleh-gif-26784876)

Master runner isn't done yet, and there's not much to scan in other packages yet  
Current SoloRunner just finds all CUIComponents and ISerializable in assembly that called CUI.Start  
Should work from master package too

Also data sources are separated from runners

CUICore + CUIRunners + data sources makes CUISetup  
There's one static CUISetup at CUI.Setup  
It's initiated at CUI.Start() and automatically disposed at plugin.Dispose()

CUI.Setup is the singleton here, everything else is just pointing to it

CUI and CUICore classes have static members for accessing core and runners  
Why 2? CUI is supposed to be used by you, and CUICore is supposed to be called by stuff from inside CUICore  
Also stuff that calls CUICore assumes that it's already activated

<br><br>
## VisualComponents / VisualUnits
Unit of drawing / event handling in CUI is VisualUnits  

SimpleTexture, Borders, TextBlock are VisualUnits  

VisualComponents is aggregate for VisualUnits, it's a base class for CUIComponent and other stuff containing VisualUnits  
VisualComponents have VisualSplit method where they specify how they should be splitted

CUIMainComponent recursively splits VisualComponents into a chain of VisualUnits  
Then it draws them and dispatches events to them  
Host VisualComponent typically listens to events on its VisualUnits

Also VisualComponents can yield VisualBounds in VisualSplit, they are commands for ChainDrawer to change its state e.g. change scissor rect, sampler state or apply camera transformation before drawing

<br><br>
## Serialization (wip, mostly borked)
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

### path resolution
When loading textures or xml prefabs CUI is aware of mod that called it and prefab file being loaded

So it checks for textures to load at:
- Full path
- Relative path to loaded xml folder
- Relative path to mod folder
- Relative path to game folder

Same for loaded xml

For saved xml it tries to save to:
- Full path
- Relative path to mod folder
- Relative path to game folder

### Serialization modes
There's a prop 
~~~~~~~~~~~~~{cs}
public CUISerializationMode CUIComponent.SerializationMode { get; set; }
~~~~~~~~~~~~~
it's one of 
~~~~~~~~~~~~~{cs}
public enum CUISerializationMode { Replace, Merge, Ignore }
~~~~~~~~~~~~~
It affects how child will be deserialized if there is already a child with the same name:
- Replace - create new child and replace existing
- Merge - Props will be copied to existing child
- Ignore - Will keep existing child intact

You can prevent child from being serialized with 
~~~~~~~~~~~~~{cs}
public bool CUIComponent.Serializable { get; set; } = true;
~~~~~~~~~~~~~
You can override these in derived components:
~~~~~~~~~~~~~{cs}
protected virtual void CUIComponent.BeforeSerialization() { } // - Happens before deserialization of children
protected virtual void CUIComponent.AfterSerialization() { } // - Happens after deserialization of children
~~~~~~~~~~~~~

<br><br>
## Component States
you can save / restore component state with

~~~~~~~~~~~~~{cs}
public void SaveState(string name)
public void RestoreState(string name)
~~~~~~~~~~~~~

there's a dict of MemorizedStates and it uses serialization behind the scenes

<br><br>
## Focus (wip, mostly borked)
In vanilla there's 
~~~~~~~~~~~~~{cs}
public IKeyboardSubscriber Barotrauma.GUI.KeyboardDispatcher.Subscriber { get; set; }
~~~~~~~~~~~~~
new subscriber gains focus, old one loses it

In CUI there's
~~~~~~~~~~~~~{cs}
public IFocusable? CUICore.GlobalFocusTracker.Focused { get; set; }
~~~~~~~~~~~~~
with the  same idea

Focusable components are focused when clicked  
Focused components start recieving keyboard events  

But actually they only request focus   
it's resolved at the end of update cycle because there might be multiple clicked components at once

You can request focus and blur manually with:
~~~~~~~~~~~~~{cs}
public void CUIComponent.Focus();
public void CUIComponent.Blur();
~~~~~~~~~~~~~

CUI can steal focus from vanilla KeyboardDispatcher and simulate focus on some dummy IKeyboardSubscriber  
It's barely tested and i'm sure it's borked  
Also there's a lot of actions in vanilla that just ignores KeyboardDispatcher.Subscriber, idk how to block those 

<br><br>
## Sprites / Textures
CUICore can't load textures on its own, it requests them from CUIRunner  

CUIRunner loads them into CUITextureManager, it only loads them once and automatically disposes them later  
All textures have a name, typically a path but you can specify the name you like  
Then you can get them by that name  

~~~~~~~~~~~~~{cs}
public static CUITextureManager CUI.TextureManager { get; }
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public interface CUITextureManager
{
  CUITexture2D Add(CUITexture2D texture, string key);
  void Clear();
  void Dispose();
  void Forget(string key);
  bool Has(string key);
  CUITexture2D Get(string key); // - this also loads the texture if it's not loaded yet
  public CUITexture2D Reload(string key);
  public CUITexture2D LoadAs(string path, string key);
}
~~~~~~~~~~~~~
When loading textures you can use:
- Absolute path
- Relative path to loaded xml folder (when texture path is inside xml prefab)
- Relative path to mod folder
- Relative path to game folder

CUI doesn't use use monogame types directly, CUIRunner wraps them in abstractions, they are mostly similar  
~~~~~~~~~~~~~
  Texture2D -> CUITexture2D
  SpriteBatch -> CUISpriteBatch
  ...
~~~~~~~~~~~~~
Why? because now i can intercept draw calls, test them and create textures with no real data

But most stuff uses CUISprites  
CUISprites is another wrapper around CUITexture2D with additional info
~~~~~~~~~~~~~{cs}
public record CUISprite(CUITexture2D texture)
{
  public static CUISprite White => new CUISprite(CUITexture2D.White);
  public static CUISprite BaroDev => new CUISprite(CUICore.TextureManager.Get("BaroDev"));

  public static CUISprite Get(string key);
  public static CUISprite LoadAs(string path, string key);

  public CUITexture2D Texture { get; set; }
  public Rectangle? SourceRectangle { get; set; } = null;
  public Color Color { get; set; } = Color.White; // !!!
  public float Rotation { get; set; } = 0.0f;
  public Vector2 Origin { get; set; } = Vector2.Zero;
  public SpriteEffects Effects { get; set; } = SpriteEffects.None;
  public float LayerDepth { get; set; } = 0.0f;

  public Color[] Data => ShouldBufferData ? DataBuffer : Texture.Data;
}
~~~~~~~~~~~~~

<br><br>
## Styles and Palettes

- Styles are actions that can be applied to components to change their props
- Palettes are tables of values for styles

Styles and palettes are reactive so if you change them all affected components will reapply their styles

CUIComponent types can have:
~~~~~~~~~~~~~{cs}
public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIFrame>((c) =>{ ... });
protected override void InitStyle() {}
~~~~~~~~~~~~~

CUIComponents have:
~~~~~~~~~~~~~{cs}
public virtual Action<CUIComponent> CUIComponent.Style { set; } // - sets PersonalStyle to CUIActionStyle
public ICUIStyle CUIComponent.PersonalStyle { get; set; }
public CUIPalette CUIComponent.Palette { get; set; }
public CUIPalette CUIComponent.DeepPalette { get; set; } // - This sets palette recursively to all children
public bool UseReactiveStyles { get; set; } // - It defaults to CUICore.Styles.UseReactiveStyles
~~~~~~~~~~~~~

#### Where to get palettes:
There are 4 global palettes in CUICore
~~~~~~~~~~~~~{cs}
public CUIPalette CUICore.Palettes.Primary { get; set; }
public CUIPalette CUICore.Palettes.Secondary { get; set; }
public CUIPalette CUICore.Palettes.Tertiary { get; set; }
public CUIPalette CUICore.Palettes.Quaternary { get; set; }
~~~~~~~~~~~~~

Also you can create palettes from color
~~~~~~~~~~~~~{cs}
public static CUIPalette CUIPalette.FromColor(Color color)
~~~~~~~~~~~~~

CUIPalettes are IDictionary<string, Color> so you can change them manually in theory

#### About Palette colors:
Those are key reference colors that all components can use to construct colors they need to paint themselves

Basically if you're making a component and want to use a color that you can't construct from already existing you should add it to palette    
And then ask yourself what if i set all colors to random will it still look good? If not then there's some hidden interconnection you're missing

#### How styles are applied on object creation:
1. Properties are initialized before constructor
2. Props can be set in constructor
3. InitStyle() method is runned, once
4. All DefaultStyles from lineage of types are applied
5. Personal style applied
6. Props you set in object initializer, if you set personal style it will also be applied on set

If you change palette or styles later this happens:
4. All DefaultStyles from lineage of types are applied
5. Personal style applied

So props you set manually or in constructor will be overriden  

#### Experimental CUIContextStyle<T>
It's a style that is added to type styles pipeline on creation and removed on dispose

You can use it to apply same actions to a group of created components

~~~~~~~~~~~~~{cs}
using (new CUIContextStyle<CUIButton>(btn => btn.Background.Color = Color.Green))
{
  new CUIButton("Green");
  new CUIButton("Green");
}
new CUIButton("Not Green");
~~~~~~~~~~~~~

<br><br>
## Animations
All animations in CUI are based on objects AnimationCore

AnimationCore is passive state machine, it has tons of settings, exposes Lambda [0..1] and can be updated manually or by CUICore.AnimationPlayer

There's tons of props, read api when i make them

There's also ypedAnimation<T> that can animate typed values
~~~~~~~~~~~~~{cs}
public class TypedAnimation<T> : AnimationCore
{
  public T StartValue { get; set; }
  public T EndValue { get; set; }
  private Func<T, T, double, T> LerpFunc;
  public T Value => LerpFunc(StartValue, EndValue, Lambda);

  public event Action<T> Changed;
}
~~~~~~~~~~~~~

Currently all animations are external, so you have to animate some value and then manually update some prop on component  
There's no AnimatedProp yet

<br><br>
## RoutableCommands
All CUIComponents can send such RoutableCommands up and down component tree to communicate with each other
~~~~~~~~~~~~~{cs}
public record RoutableCommand(string name, object data);
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public partial class CUIComponent
  {
    public public_Commands_Part Commands { get; } = new();

    public class public_Commands_Part : Part, IModule
    {
      public void ListenFor(string name, Action<object> action);
      public void ListenFor<T>(string name, Action<T> action);
      public void SendDown(string name, object data = null);
      public void SendUp(string name, object data = null);
    }
  }
~~~~~~~~~~~~~

Benefits of RoutableCommands:
- You don't depend on layout, you can rearrange your components and they will still work
- You can develop smaller components independenty instead of hardcoding one to use another

For example CUICloseButton emits "close" event up to the nearest CUIFrame  
When CUIFrame recieves "close" event it closes  
It doesn't matter where the close button is and i don't have to pass close button to the frame so it could subscribe to its events


## Debug (almost non existent)

Most of the time i debug with just CUI.Logger, but if it's too complex or i know i'll need the same data later is add DebugNodes

~~~~~~~~~~~~~{cs}
public class DebugHub : DebugRelayBase
{
    public ClearableEvent<DebugEvent> Output;
    public IDictionary<string, DebugGate> Gates;
    public void Open(string type);
    public void Close(string type);
}
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public class DebugNode<T1> : DebugNodeBase
{
    public bool IsOpen;
    public Func<T1, string> MsgFactory;
    public void Send(T1 arg1);
    public DebugGate GlobalGate;
    public DebugHub Hub;
    private DebugEvent EventFactory(T1 arg1)
    {
      return new DebugEvent()
      {
        Type = Type,
        Args = new object[] { arg1 },
        Msg = MsgFactory.Invoke(arg1),
      };
    }
}
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
  public class DebugRelay : DebugRelayBase, IDebugRelayTarget
  {
    public void Map(DebugRelayBase next);
    public void Unmap(DebugRelayBase next);
  }
~~~~~~~~~~~~~

They're designed to be fast  
They are generic and can accept various objects without transformations   
Node wraps them into DebugEvents with the message generated by MsgFactory and the send that DebugEvent directly to CUICore.DebugHub.Output  
But only when the node is open and DebugHub.DebugGate is open

Nodes and DebugRelays can be routed, but they don't route the data, they only route open/close commands  
~~~~~~~~~~~~~
CUICore.DebugHub <- CUICore.Main <- deep Component attached to CUICore.Main <- modules inside that component  
~~~~~~~~~~~~~

you can open all gates on component with
~~~~~~~~~~~~~{cs}
  public bool Debug { get; set; }
  public bool DeepDebug { get; set; } // also open recursively on all children
~~~~~~~~~~~~~

Then debuggers can listen for DebugEvents on CUICore.DebugHub, there's one in Infrastructure  
//TODO i probably should move it to CUICore so you could use it too









