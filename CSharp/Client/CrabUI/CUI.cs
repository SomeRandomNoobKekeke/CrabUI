using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;

using System.Runtime.CompilerServices;
[assembly: IgnoresAccessChecksTo("Barotrauma")]
[assembly: IgnoresAccessChecksTo("DedicatedServer")]
[assembly: IgnoresAccessChecksTo("BarotraumaCore")]

namespace CrabUI
{
  /// <summary>
  /// In fact a static class managing static things
  /// </summary>
  public partial class CUI
  {
    public static Vector2 GameScreenSize => new Vector2(GameMain.GraphicsWidth, GameMain.GraphicsHeight);
    public static Rectangle GameScreenRect => new Rectangle(0, 0, GameMain.GraphicsWidth, GameMain.GraphicsHeight);


    private static string modDir;
    /// <summary>
    /// Root folder of the mod, used to deduce lua folder location
    /// </summary>
    public static string ModDir
    {
      get => modDir;
      set
      {
        modDir = value;
        LuaFolder = Path.Combine(value, @"Lua");
      }
    }

    /// <summary>
    /// If false CUI won't waste time on registering lua types and creating types file
    /// </summary>
    public static bool UseLua { get; set; } = true;
    /// <summary>
    /// Should point to lua folder, if you want lua types file
    /// </summary>
    public static string LuaFolder { get; set; }

    private static string assetsPath;
    /// <summary>
    /// Path to assets folder with all CUI stuff
    /// </summary>
    public static string AssetsPath
    {
      get => assetsPath;
      set
      {
        assetsPath = value;
        PalettesPath = Path.Combine(value, @"Palettes");
      }
    }
    public static string CUITexturePath = "CUI.png";
    public static string PalettesPath { get; set; }



    /// <summary>
    /// If set CUI will also check this folder when loading textures
    /// </summary>
    public static string PGNAssets { get; set; }

    /// <summary>
    /// A singleton
    /// </summary>
    public static CUI Instance;
    /// <summary>
    /// Orchestrates Drawing and updates, there could be only one
    /// CUI.Main is located under vanilla GUI
    /// </summary>
    public static CUIMainComponent Main => Instance?.main;
    /// <summary>
    /// Orchestrates Drawing and updates, there could be only one
    /// CUI.TopMain is located above vanilla GUI
    /// </summary>
    public static CUIMainComponent TopMain => Instance?.topMain;
    /// <summary>
    /// Snapshot of mouse and keyboard state
    /// </summary>
    public static CUIInput Input => Instance?.input;
    /// <summary>
    /// Safe texture manager
    /// </summary
    public static CUITextureManager TextureManager => Instance?.textureManager;
    /// <summary>
    /// Adapter to vanilla focus system, don't use
    /// </summary>
    public static CUIFocusResolver FocusResolver => Instance?.focusResolver;
    public static CUILuaRegistrar LuaRegistrar => Instance?.luaRegistrar;

    public static CUIComponent FocusedComponent
    {
      get => FocusResolver.FocusedCUIComponent;
      set => FocusResolver.FocusedCUIComponent = value;
    }

    /// <summary>
    /// This affects logging
    /// </summary>
    public static bool Debug;
    /// <summary>
    /// If true other compiled mods that use CUI will break 
    /// But if false then you'll have to setup hooks with lua or separate compiled plugin
    /// </summary>
    public static bool UseCursedPatches { get; set; } = true;
    /// <summary>
    /// It's used as harmony id and lua hooks id, it's deduced automatically from calling assembly name  
    /// It's should be unique per mod or there will be conflicts  
    /// You can set it to any string if you want
    /// </summary>
    public static string HookIdentifier
    {
      get => hookIdentifier;
      set
      {
        hookIdentifier = value?.Replace(' ', '_');
      }
    }
    private static string hookIdentifier;
    public static string CUIHookID => $"CrabUI.{HookIdentifier}";
    public static Harmony harmony;
    public static Random Random = new Random();

    /// <summary>
    /// Called on first Initialize
    /// </summary>
    public static event Action OnInit;
    /// <summary>
    /// Called on last Dispose
    /// </summary>
    public static event Action OnDispose;
    public static bool Disposed { get; set; } = true;
    public static event Action<TextInputEventArgs> OnWindowTextInput;
    public static event Action<TextInputEventArgs> OnWindowKeyDown;
    //public static event Action<TextInputEventArgs> OnWindowKeyUp;

    //TODO Still doesn't trigger when someone ends round in multiplayer
    public static event Action OnPauseMenuToggled;
    public static void InvokeOnPauseMenuToggled() => OnPauseMenuToggled?.Invoke();

    public static bool InputBlockingMenuOpen
    {
      get
      {
        if (IsBlockingPredicates == null) return false;
        return IsBlockingPredicates.Any(p => p());
      }
    }
    public static List<Func<bool>> IsBlockingPredicates => Instance?.isBlockingPredicates;
    private List<Func<bool>> isBlockingPredicates = new List<Func<bool>>();
    /// <summary>
    /// In theory multiple mods could use same CUI instance, 
    /// i clean it up when UserCount drops to 0
    /// </summary>
    public static int UserCount = 0;

    /// <summary>
    /// An object that contains current mouse and keyboard states
    /// It scans states at the start on Main.Update
    /// </summary>
    private CUIInput input = new CUIInput();
    private CUIMainComponent main;
    private CUIMainComponent topMain;
    private CUITextureManager textureManager = new CUITextureManager();
    private CUIFocusResolver focusResolver = new CUIFocusResolver();
    private CUILuaRegistrar luaRegistrar = new CUILuaRegistrar();

    public static void ReEmitWindowTextInput(object sender, TextInputEventArgs e) => OnWindowTextInput?.Invoke(e);
    public static void ReEmitWindowKeyDown(object sender, TextInputEventArgs e) => OnWindowKeyDown?.Invoke(e);
    //public static void ReEmitWindowKeyUp(object sender, TextInputEventArgs e) => OnWindowKeyUp?.Invoke(e);


    private void CreateMains()
    {
      main = new CUIMainComponent() { AKA = "Main Component" };
      topMain = new CUIMainComponent() { AKA = "Top Main Component" };
    }

    /// <summary>
    /// Should be called in IAssemblyPlugin.Initialize 
    /// \todo make it CUI instance member when plugin system settle
    /// </summary>
    public static void Initialize()
    {
      CUIDebug.Log($"CUI.Initialize {HookIdentifier} Instance:[{Instance?.GetHashCode()}] UserCount:{UserCount}", Color.Lime);
      if (Instance == null)
      {
        GhostDetector.Activate();
        Disposed = false;
        HookIdentifier ??= Assembly.GetCallingAssembly().GetName().Name ?? "";

        Instance = new CUI();

        Stopwatch sw = Stopwatch.StartNew();
        if (HookIdentifier == null || HookIdentifier == "") CUI.Warning($"Warning: CUI.HookIdentifier is not set, this mod may conflict with other mods that use CUI");

        InitStatic();
        // this should init only static stuff that doesn't depend on instance
        OnInit?.Invoke();

        Instance.CreateMains();

        GameMain.Instance.Window.TextInput += ReEmitWindowTextInput;
        GameMain.Instance.Window.KeyDown += ReEmitWindowKeyDown;
        //GameMain.Instance.Window.KeyUp += ReEmitWindowKeyUp;
        CUIDebug.Log($"CUI.OnInit?.Invoke took {sw.ElapsedMilliseconds}ms");

        sw.Restart();

        harmony = new Harmony(CUIHookID);
        PatchAll();
        CUIDebug.Log($"CUI.PatchAll took {sw.ElapsedMilliseconds}ms");

        AddCommands();
        AddDebugCommands();

        sw.Restart();
        LuaRegistrar.Register();
        CUIDebug.Log($"CUI.LuaRegistrar.Register took {sw.ElapsedMilliseconds}ms");
      }

      UserCount++;

      CUIDebug.Log($"CUI.Initialized {HookIdentifier} Instance:[{Instance?.GetHashCode()}] UserCount:{UserCount}", Color.Lime);
    }

    public static void OnLoadCompleted()
    {
      //Idk doesn't work
      //CUIMultiModResolver.FindOtherInputs();
    }


    /// <summary>
    /// Should be called in IAssemblyPlugin.Dispose
    /// </summary>
    public static void Dispose()
    {
      GhostDetector.Deactivate();
      CUIDebug.Log($"CUI.Dispose {HookIdentifier} Instance:[{Instance?.GetHashCode()}] UserCount:{UserCount}", Color.Lime);

      UserCount--;

      if (UserCount <= 0)
      {
        RemoveCommands();
        // harmony.UnpatchAll(harmony.Id);
        harmony.UnpatchAll();
        TextureManager.Dispose();
        CUIDebugEventComponent.CapturedIDs.Clear();
        OnDispose?.Invoke();
        Disposed = true;

        Instance.isBlockingPredicates.Clear();
        Errors.Clear();

        LuaRegistrar.Deregister();

        Instance = null;
        UserCount = 0;

        CUIDebug.Log($"CUI.Disposed {HookIdentifier} Instance:[{Instance?.GetHashCode()}] UserCount:{UserCount}", Color.Lime);
      }

      GameMain.Instance.Window.TextInput -= ReEmitWindowTextInput;
      GameMain.Instance.Window.KeyDown -= ReEmitWindowKeyDown;
      //GameMain.Instance.Window.KeyUp -= ReEmitWindowKeyUp;
    }

    //HACK Why it's set to run in static constructor?
    // it runs perfectly fine in CUI.Initialize
    //TODO component inits doesn't depend on the order
    // why am i responsible for initing them here?
    internal static void InitStatic()
    {
      CUIInterpolate.InitStatic();
      CUIAnimation.InitStatic();
      CUIReflection.InitStatic();
      CUIMultiModResolver.InitStatic();
      CUIPalette.InitStatic();
      CUIMap.CUIMapLink.InitStatic();
      CUIMenu.InitStatic();
      CUIComponent.InitStatic();
      CUITypeMetaData.InitStatic();
      CUIStyleLoader.InitStatic();
    }
  }
}